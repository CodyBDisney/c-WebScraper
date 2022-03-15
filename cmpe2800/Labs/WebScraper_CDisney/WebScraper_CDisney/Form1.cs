/////////////////////////////
///
/// Project: Lab02 - Web Scraper
/// 
/// Author: Cody Disney
/// 
/// Submission Code : CMPE2800_1212_L02
/// 
/// Revision History: See SVN
/// 
/////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace WebScraper_CDisney
{
    public partial class Form1 : Form
    {

        //members
        private List<CustomImage> _images = new List<CustomImage>();//list of images from a website
        private string _downloadLocation;                           //path to download folder
        BindingSource bSource = new BindingSource();                //Binding source for datagridview


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
            UI_Gridview.RowHeadersVisible = false;

        }

        //----------Change Download Location----------
        /// <summary>
        /// changes download location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_Button_ChangeDownload_Click(object sender, EventArgs e)
        {
            ChangeDownloadLocation();
        }

        /// <summary>
        /// Changes folder path to download images to
        /// </summary>
        /// <returns>bool if change ws successful</returns>
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

        /// <summary>
        /// Called on text change in url box, checks if url is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_URLBox_TextChanged(object sender, EventArgs e)
        {
            //will put regex here to check if valid url entered
            //check if any text in url box
            if (UI_URLBox.Text.Length == 0)
            {
                UI_URLBox.ForeColor = Color.DarkGray;
                UI_Button_Load.Enabled = false;
            }

            Regex reg = new Regex(@"http[s]?:\/\/(www\.)?(?'part'(.*)?\/)*(?'name'.*)(?'extension'\..*)");

            //see if url entered is valid, enable loading accordingly
            if (reg.IsMatch(UI_URLBox.Text))
            {
                UI_URLBox.ForeColor = Color.Green;
                UI_Button_Load.Enabled = true;
            }
            else
            {
                UI_URLBox.ForeColor = Color.Red;
                UI_Button_Load.Enabled = false;
            }
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
            UI_ListBox.Items.Clear();
            bSource.DataSource = null;

            /******************************************
             *   Parse website link from textbox
             ******************************************/
            Regex reg = new Regex(@"http[s]?:\/\/(www\.)?(?'part'(.*)?\/)*(?'name'.*)(?'extension'\..*)"); 

            
            if (!reg.IsMatch(UI_URLBox.Text))
            {
                UpdateListView("Invalid url loaded");
                return;
            }

            MatchCollection matches = reg.Matches(UI_URLBox.Text);

            string url = matches[0].ToString();

            if(!await GetWebsite(url))
            {
                UI_Button_Load.Enabled = true;
                return;
            }


            /******************************************
             *           Display Info
             ******************************************/
            UpdateListView("\n-----\n");
            UpdateListView($"{_images.Count()} links found.");

            //find number of duplicate image links
            
            int duplicateImages = _images.Count() - _images.Distinct().Count();

            UpdateListView($"{duplicateImages} duplicate links found");



            /******************************************
             *       Remove images with same url
             ******************************************/
            _images = _images.Distinct().ToList();

            //find number of different image types
            int extensionCount = (from n in _images select n.Extension).Distinct().Count();

            UpdateListView($"{extensionCount} different image types proccessed.");

            /******************************************
             *        Get Byte arrays from url   
             ******************************************/
            Dictionary<Task, CustomImage> downloadTasks = new Dictionary<Task, CustomImage>();

            foreach (CustomImage image in _images)
            {
                using (WebClient downloadClient = new WebClient())
                {
                    downloadTasks.Add(downloadClient.DownloadDataTaskAsync(new Uri(image.Url)),image);
                }
            }

            //Wait for downloads to finish
            int totalTasks = downloadTasks.Count();

            UpdateListView("\n-----\n");

            while (downloadTasks.Count() > 0)
            {
                Task<byte[]> finished = (Task<byte[]>)(await Task.WhenAny(downloadTasks.Keys));
                


                if (finished.Status != TaskStatus.RanToCompletion)
                {
                    UpdateListView($"Failed to download image: {downloadTasks[finished].FileName}");
                    _images.Remove(downloadTasks[finished]);
                    downloadTasks.Remove(finished);
                    continue;
                }
                downloadTasks[finished].Bytes = finished.Result;

                downloadTasks.Remove(finished);

                UpdateListView($"Finished retrieving image {totalTasks - downloadTasks.Count()} / {totalTasks}");

            }

            /******************************************
             *       Find duplicate image files    
             ******************************************/
            //Get images with the same name
            var duplicateGroups = from img in _images
                                  group img by img.FileName into q
                                  select q;


            List<CustomImage> imagesToRemove = new List<CustomImage>();
            foreach(var x in duplicateGroups)
            {
                var sameName = x.ToArray();

                foreach(CustomImage img in sameName) //gets any images that have duplicate bytes of the first ones
                {
                    var imgs = from i in sameName
                               where i.Bytes.SequenceEqual(img.Bytes) && i != img && !imagesToRemove.Contains(img)
                               select i;

                    imagesToRemove.AddRange(imgs);
                }
            }

            //remove duplicate images (same name and image)
            foreach (CustomImage image in imagesToRemove)
            {
                _images.Remove(image);
                UpdateListView($"Removed duplicate image: {image.FileName}");
            }


            /******************************************
             *          Set Download Location 
             ******************************************/
            if (_downloadLocation == null) //set location if it hasnt already
            {
                if (!ChangeDownloadLocation())
                {
                    UpdateListView("Download cancelled");
                    return;
                }
            }

            string folderName = $"{url.Split('/')[2]}_{DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")}"; //agreed formatting

            /******************************************
             *      Rename Duplicate image names
             ******************************************/
            foreach (var group in duplicateGroups)
            {
                var groupList = group.ToList();
                if(groupList.Count() > 1)
                {
                    for (int i = 1; i < groupList.Count(); i++)
                    {
                        string namePart = groupList[i].FileName.Split('.')[0];
                        groupList[i].FileName = $"{namePart}_({i}).{groupList[i].Extension}"; //adds number to images with dup names but diff bytes
                    }
                }
                
            }

            foreach(CustomImage img in _images.Where(x => x.FileName.ToUpper().Contains("SVG")))
            {
                string[] parts = img.FileName.Split('?');
                string newFilename = "";
                for(int i = 0; i < parts.Length-1; i++)
                {
                    newFilename += parts[i];
                }
                newFilename += ".svg";
                img.FileName = newFilename;
                img.Extension = "svg";
            }

            /******************************************
             *        Save images to location
             ******************************************/
            System.IO.Directory.CreateDirectory($"{_downloadLocation}\\{folderName}"); //creates new folder

            List<Task> saveTasks = new List<Task>();
            foreach(CustomImage img in _images) //prepares tasks to download images
            {
                saveTasks.Add(SaveImageAsync($"{_downloadLocation}\\{folderName}\\{img.FileName}", img));
            }

           
            
            
            int totalImages = saveTasks.Count(); //saves total amount of images being saved for output
            UpdateListView("\n-----\n");

            while (saveTasks.Count() > 0) //saves images and reports when completed
            {
                Task task = await Task.WhenAny(saveTasks);
                UpdateListView($"Finished saving image {totalImages - saveTasks.Count() + 1} / {totalImages}");

                saveTasks.Remove(task);
            }

            /******************************************
             *     Display image data to gridview
             ******************************************/
            var selectedData = from x in _images
                               select new
                               {
                                   ImageName = x.FileName,
                                   ImageType = x.Extension,
                                   URL = x.Url,
                                   Bytes = x.Bytes.Count()
                               };

            bSource.DataSource = selectedData;

            //re-enable load button
            UI_Button_Load.Enabled = true;
        }

        ////------------------Helpers------------------

        /// <summary>
        /// async method, currently does everything, but will eventually only retrieve website html
        /// </summary>
        /// <param name="url">url of website being searched</param>
        /// <returns></returns>
        private async Task<bool> GetWebsite(string url)
        {
            //grab html from website url
            WebClient client = new WebClient();
            Stream x = null;

            try
            {
                x = await client.OpenReadTaskAsync(url);
            }
            catch(Exception ex)
            {
                UpdateListView("Website did not respond");
                return false;
            }
            
            //open a streamreader to read html
            StreamReader rdr = new StreamReader(x);

            //get links and image links from html
            _images = GetLinks(rdr.ReadToEnd()); //gets all links

            return true;
        }

        /// <summary>
        /// Function to retrieve all http or https links from an html file
        /// </summary>
        /// <param name="file">html of a website</param>
        /// <returns>List containing all http or https links</returns>
        private List<CustomImage> GetLinks(string file)
        {
            //Regex reg = new Regex(@"http[s]?:\/\/.*(?'extension'\..*?(/|\\| )*?)*");
            Regex reg = new Regex("<img.*src( )*=( )*[\"\'](?'link'http[s]?://.*?..*?)[\"\']"); //grabs all image tags from html

            MatchCollection matches = reg.Matches(file);

            List<CustomImage> images = new List<CustomImage>();
            foreach (Match match in matches)
            {
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

        /// <summary>
        /// Updates text in listview, and scrolls to keep up
        /// </summary>
        /// <param name="message">message to put into listview</param>
        private void UpdateListView(string message)
        {
            UI_ListBox.Items.Add(message);
            UI_ListBox.TopIndex = UI_ListBox.Items.Count - 1;
        }

        /// <summary>
        /// Asynchronous method to save image to specified file path
        /// </summary>
        /// <param name="path">filepath to save image to</param>
        /// <param name="img">image being saved</param>
        /// <returns>Task to save image</returns>
        private async Task SaveImageAsync(string path, CustomImage img)
        {
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await stream.WriteAsync(img.Bytes, 0, img.Bytes.Length);
            }
        }

        
    }
}
