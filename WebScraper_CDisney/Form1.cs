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
        async private void UI_Button_Load_Click(object sender, EventArgs e)
        {
            //should parse out links, and retreives the website name and extension
            Regex reg = new Regex(@"http[s]?:\/\/(www\.)?(?'part'(.*)?\/)*(?'name'.*)(?'extension'\..*)"); 

            string testUrl = UI_URLBox.Text;
            WriteLine(testUrl);
            if (reg.IsMatch(testUrl))
            {
                MatchCollection matches = reg.Matches(testUrl);

                foreach(Match match in matches)
                {
                    WriteLine($"{match}, {match.Groups["name"]}, {match.Groups["extension"]}");

                    await Task.Run(() => PrintHTML(match.ToString()));
                }
            }
        }

        ////------------------Helpers------------------

        private async Task PrintHTML(string url)
        {
            WriteLine("Started printing");
            WebClient client = new WebClient();
            var x = await client.OpenReadTaskAsync(@"http:\\www.arstechnica.com");
            WriteLine($"Done! {x}");
            StreamReader rdr = new StreamReader(x);
            //Console.WriteLine(rdr.ReadToEnd());
            GetLinks(rdr.ReadToEnd());
        }

        private List<string> GetLinks(string file)
        {
            WriteLine("Getting links");
            //Regex reg = new Regex(@"http[s]?:\/\/.*(?'extension'\..*?(/|\\| )*?)*");
            Regex reg = new Regex("\"" + @"http[s]?(.*?)*" + "\"");

            MatchCollection matches = reg.Matches(file);

            List<string> images = new List<string>();

            foreach(Match match in matches)
            {
                WriteLine(match);
                images.Add(match.ToString());
            }

            return images;
        }
    }
}
