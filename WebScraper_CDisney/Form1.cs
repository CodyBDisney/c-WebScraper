using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using static System.Diagnostics.Trace;
using System.Net;

namespace WebScraper_CDisney
{
    public partial class Form1 : Form
    {
        //members
        List<Task> _activeDownloads = new List<Task>(); //List of tasks dowloading images

        public Form1()
        {
            InitializeComponent();

            //Labels
            UI_Label_URLBox.Text = "URL:";
            UI_Label_GridView.Text = "Loaded images:";
            UI_Label_Listbox.Text = "Notifications:";

            //Load Button
            UI_Button_Load.Text = "Load";
            UI_Button_Load.Click += UI_Button_Load_Click;

            //Save Button
            UI_Button_Save.Text = "Save";
            UI_Button_Save.Click += UI_Button_Save_Click;

            //Cancel Button
            UI_Button_Cancel.Text = "Cancel";
            UI_Button_Cancel.Click += UI_Button_Cancel_Click;
        }

        //------------------Form Methods------------------
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //------------------Cancel Loading------------------
        private void UI_Button_Cancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //------------------Save Images------------------
        private void UI_Button_Save_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            List<string> links = GetLinks(rdr.ReadToEnd()); //gets all links

            List<string> imageLinks = GetImageLinks(links); //pulls out only image links

            //display information

            //open file dialog for download location

            //start downloading files

            //done
            return messageString;
            
        }

        private List<string> GetLinks(string file)
        {
            WriteLine("----------Getting links----------");
            //Regex reg = new Regex(@"http[s]?:\/\/.*(?'extension'\..*?(/|\\| )*?)*");
            Regex reg = new Regex("\"" + @"http[s]?(.*?)*" + "\"");

            MatchCollection matches = reg.Matches(file);

            List<string> images = new List<string>();

            foreach(Match match in matches)
            {
                WriteLine(match);
                images.Add(match.ToString().Replace('\"',' ').Trim()); //replace and trim remove quotations from html
            }

            return images;
        }

        private List<string> GetImageLinks(List<string> links)
        {
            WriteLine("----------Getting image links----------");
            string[] goodExtensions = new string[] { "jpg", "png", "jpeg", "ico" };

            var imageLinks = from n in links
                             where goodExtensions.Contains(n.Split('.').Last())
                             select n;


            foreach(string n in imageLinks)
            {
                WriteLine(n);
            }

            return imageLinks.ToList();
        }
    }
}
