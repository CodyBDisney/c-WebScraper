using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper_CDisney
{
    class CustomImage
    {
        public string Url;      //Full url of image
        public string Extension;//Extension of image
        public string FileName; //Name of image

        public CustomImage(string url)
        {
            Url = url;
            FileName = url.Split('/').Last();
            Extension = FileName.Split('.').Last();
        }
    }
}
