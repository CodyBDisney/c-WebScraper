using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using static System.Diagnostics.Trace;
using System.Net;

namespace WebScraper_CDisney
{
    public partial class Form1 : Form
    {
        //members
        private List<Task> _activeDownloads = new List<Task>(); //List of tasks dowloading images
        private List<CustomImage> _images = new List<CustomImage>(); //list of images from a website
        private string _downloadLocation; //path to download folder
        BindingSource bSource = new BindingSource();


        public Form1()
        {
            InitializeComponent();

            //Labels
            UI_Label_URLBox.Text = "URL:";
            UI_Label_GridView.Text = "Loaded images:";
            UI_Label_Listbox.Text = "Notifications:";
            UI_Label_DownloadLocation.Text = "Download Location:";

            //Text input
            UI_URLBox.TextChanged += UI_URLBox_TextChanged;

            //Load Button
            UI_Button_Load.Text = "Load";
            UI_Button_Load.Click += UI_Button_Load_Click;

            //Download Location
            UI_Button_ChangeDownload.Text = "Change";
            UI_Button_ChangeDownload.Click += UI_Button_ChangeDownload_Click;

            UI_TextBox_DownloadLocation.Text = "No path selected";
            UI_TextBox_DownloadLocation.ReadOnly = true;

            //gridView
            UI_Gridview.DataSource = bSource;

        }

        //----------Change Download Location----------
        private void UI_Button_ChangeDownload_Click(object sender, EventArgs e)
        {
            ChangeDownloadLocation();
        }

        private bool ChangeDownloadLocation()
        {
            bool result = false;

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                _downloadLocation = dlg.SelectedPath;
                result = true;
            }
            UI_TextBox_DownloadLocation.Text = _downloadLocation;

            return result;
        }

        private void UI_URLBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        //------------------Form Methods------------------
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //------------------Load URL------------------
        /// <summary>
        /// Button click handler for the load website button, starts the proccess to retrieve images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void UI_Button_Load_Click(object sender, EventArgs e)
        {
            //reset and prepare again
            UI_Button_Load.Enabled = false;
            //should parse out links, and retreives the website name and extension
            Regex reg = new Regex(@"http[s]?:\/\/(www\.)?(?'part'(.*)?\/)*(?'name'.*)(?'extension'\..*)"); 

            string testUrl = UI_URLBox.Text;
            
            if (!reg.IsMatch(testUrl))
            {
                UpdateListView("Invalid url loaded"); 
            }

            MatchCollection matches = reg.Matches(testUrl);

            string url = matches[0].ToString();

            await GetWebsite(url);

            //-----display information-----
            UpdateListView($"{_images.Count()} links found.");

            //find number of duplicate image links

            int duplicateImages = _images.Count() - _images.Distinct().Count();

            UpdateListView($"{duplicateImages} duplicate links found");

            //remove duplicate images
            _images = _images.Distinct().ToList();

            //find number of different image types
            int extensionCount = (from n in _images select n.Extension).Distinct().Count();

            UpdateListView($"{extensionCount} different image types proccessed.");

            //-----Download byte arrays-----
            Dictionary<Task, CustomImage> downloadTasks = new Dictionary<Task, CustomImage>();

            foreach (CustomImage image in _images)
            {
                using (WebClient downloadClient = new WebClient())
                {
                    downloadTasks.Add(downloadClient.DownloadDataTaskAsync(new Uri(url)),image);
                }
            }

            //Wait for downloads to finish
            int totalTasks = downloadTasks.Count();

            while (downloadTasks.Count() > 0)
            {
                Task<byte[]>finished = (Task<byte[]>)(await Task.WhenAny(downloadTasks.Keys));

                downloadTasks[finished].Bytes = finished.Result;

                downloadTasks.Remove(finished);

                UpdateListView($"Finished retrieving image {totalTasks - downloadTasks.Count()} / {totalTasks}");

            }

            //Download to a location
            if (_downloadLocation == null)
            {
                while (!ChangeDownloadLocation())
                {
                    UpdateListView("Please select a location to download to.");
                }
            }

            string folderName = $"{url.Split('/')[2]}_{DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")}";

            //find duplicate images

            //store images
            System.IO.Directory.CreateDirectory($"{_downloadLocation}\\{folderName}"); //creates new folder

            List<Task> saveTasks = new List<Task>();
            foreach(CustomImage img in _images) //prepares tasks to download images
            {
                using (FileStream stream = new FileStream($"{_downloadLocation}\\{folderName}\\{img.FileName}", FileMode.CreateNew))
                {
                    saveTasks.Add(stream.WriteAsync(img.Bytes,0,img.Bytes.Length));
                }
            }


            int totalImages = saveTasks.Count();
            while(saveTasks.Count() > 0) //downlaods images and reports when completed
            {
                Task task = await Task.WhenAny(saveTasks);

                UpdateListView($"Finished saving image {totalImages - saveTasks.Count()} / {totalImages}");

                saveTasks.Remove(task);
            }

            //Display
            var selectedData = from x in _images
                               select new
                               {
                                   ImageName = x.FileName,
                                   ImageType = x.Extension,
                                   URL = x.Url
                               };

            bSource.DataSource = selectedData;

            //re enable button
            UI_Button_Load.Enabled = true;
        }

        ////------------------Helpers------------------

        /// <summary>
        /// async method, currently does everything, but will eventually only retrieve website html
        /// </summary>
        /// <param name="url">url of website being searched</param>
        /// <returns></returns>
        private async Task GetWebsite(string url)
        {
            //grab html from website url
            WriteLine("Started printing");
            WebClient client = new WebClient();

            var x = await client.OpenReadTaskAsync(url);
            WriteLine($"Done! {x}");

            //open a streamreader to read html
            StreamReader rdr = new StreamReader(x);

            //get links and image links from html
            //WriteLine(rdr.ReadToEnd());
            _images = GetLinks(rdr.ReadToEnd()); //gets all links
        }

        /// <summary>
        /// Function to retrieve all http or https links from an html file
        /// </summary>
        /// <param name="file">html of a website</param>
        /// <returns>List containing all http or https links</returns>
        private List<CustomImage> GetLinks(string file)
        {
            WriteLine("----------Getting links----------");
            //Regex reg = new Regex(@"http[s]?:\/\/.*(?'extension'\..*?(/|\\| )*?)*");
            Regex reg = new Regex("<img.*src( )*=( )*[\"\'](?'link'http[s]?://.*?..*?)[\"\']"); //grabs all image tags from html

            MatchCollection matches = reg.Matches(file);

            List<CustomImage> images = new List<CustomImage>();

            if(matches.Count < 1)
            {
                WriteLine("No links found");
            }
            foreach (Match match in matches)
            {
                //WriteLine(match);
                //linq grabs http or https link from link
                var link = from l in match.ToString().Split(new char[] {'\"','\'' }) //split on double or single quotes
                           where l.StartsWith("http")
                           select l;
                
                foreach(string l in link)
                {
                    images.Add(new CustomImage(l));
                }
            }

            return images;
        }

        private void UpdateListView(string message)
        {
            UI_ListBox.Items.Add(message);
            UI_ListBox.TopIndex = UI_ListBox.Items.Count - 1;
        }



        
    }
}
