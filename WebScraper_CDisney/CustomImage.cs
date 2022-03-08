﻿using System;
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
        public byte[] Bytes;    //byte array containing file data

        /// <summary>
        /// Cosntructor, takes url and parses necessary information
        /// </summary>
        /// <param name="url">url of image being processed</param>
        public CustomImage(string url)
        {
            Url = url;
            FileName = url.Split('/').Last();
            Extension = FileName.Split('.').Last();
        }

        /// <summary>
        /// Override of equals to compare images by url
        /// </summary>
        /// <param name="obj">Object being comapred to</param>
        /// <returns>Bool of equality based on url</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CustomImage other)) throw new ArgumentException("Cannot compare different types");
            return Url.Equals(other.Url);
        }

        /// <summary>
        /// Override of gethashcode to prevent hashing for comparison
        /// </summary>
        /// <returns>0</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        public bool BytesMatch(byte[] other)
        {
            return Bytes.SequenceEqual(other);
        }
    }
}
