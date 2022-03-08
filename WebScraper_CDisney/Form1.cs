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
        private string _downloadLocation;


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
            throw new NotImplementedException();
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
            //should parse out links, and retreives the website name and extension
            Regex reg = new Regex(@"http[s]?:\/\/(www\.)?(?'part'(.*)?\/)*(?'name'.*)(?'extension'\..*)"); 

            string testUrl = UI_URLBox.Text;
            
            if (reg.IsMatch(testUrl))
            {
                MatchCollection matches = reg.Matches(testUrl);

                foreach(Match match in matches)
                {
                    WriteLine($"{match}, {match.Groups["name"]}, {match.Groups["extension"]}");

                    string message = await GetWebsite(match.ToString());
                }
            }
        }

        ////------------------Helpers------------------

        /// <summary>
        /// async method, currently does everything, but will eventually only retrieve website html
        /// </summary>
        /// <param name="url">url of website being searched</param>
        /// <returns></returns>
        private async Task<string> GetWebsite(string url)
        {
            string messageString = "An error has occurred"; //string to display message after proccess stops

            //grab html from website url
            WriteLine("Started printing");
            WebClient client = new WebClient();

            var x = await client.OpenReadTaskAsync(url);
            WriteLine($"Done! {x}");

            //open a streamreader to read html
            StreamReader rdr = new StreamReader(x);

            //get links and image links from html
            //WriteLine(rdr.ReadToEnd());
            List<CustomImage> links = GetLinks(rdr.ReadToEnd()); //gets all links
                       
            foreach(CustomImage img in links)
            {
                WriteLine(img.Url);
            }

            //display information
            UI_ListBox.Items.Add($"{links.Count()} links found.");
           
            //find number of different image types
            int extensionCount = (from n in links select n.Extension).Distinct().Count();

            UI_ListBox.Items.Add($"{extensionCount} different image types proccessed.");

            //find number of duplicate image links

            int duplicateImages = links.Count() - links.Distinct().Count();

            UI_ListBox.Items.Add($"{duplicateImages} duplicate images found");


            links = links.Distinct().ToList();
            //open file dialog for download location

            if(_downloadLocation == null)
            {
                while (!ChangeDownloadLocation())
                {
                    UI_ListBox.Items.Add("Please select a location to download to");
                }
            }


            //start downloading files
            List<Task> downloadTasks = new List<Task>();
            
            foreach(CustomImage image in links)
            {
                using(WebClient downloadClient = new WebClient())
                {
                    downloadTasks.Add(downloadClient.DownloadFileTaskAsync(new Uri(image.Url), _downloadLocation + "\\" + image.FileName));
                }
                    
                
            }

            int totalTasks = downloadTasks.Count();

            while(downloadTasks.Count() > 0)
            {
                Task finished = await Task.WhenAny(downloadTasks);

                downloadTasks.Remove(finished);

                UI_ListBox.Items.Add($"Finished download {totalTasks - downloadTasks.Count()} / {totalTasks}");
            }

            UI_ListBox.Items.Add("Finished all downloads");
            

            //done
            return messageString;
            
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



        
    }
}
