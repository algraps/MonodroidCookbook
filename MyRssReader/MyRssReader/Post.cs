using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyRssReader.WorpressReader;

namespace MyRssReader
{
    public class Post
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
    }

    public class PostManager
    {
        public static IList GetBlogPosts(string postUrl, int postCount)
        {

            string xml;

            using (WebClient downloader = new WebClient())
            {
                using (TextReader reader =
                   new StreamReader(downloader.OpenRead(postUrl)))
                {
                    xml = reader.ReadToEnd();
                }
            }
            // Sanitize the XML
            xml = XmlCommon.SanitizeXmlString(xml);
            XmlDocument xmlDoc = new XmlDocument();
            //Load feed via a feedUrl.
            TextReader tr = new StringReader(xml);
            var doc = XDocument.Load(tr);

            XNamespace dc = "http://purl.org/dc/elements/1.1/";

            //Get all the "items" in the feed.
            var feeds = doc.Descendants("item").Select(x =>
            new Post
            {
                //Get title, pubished date, and link elements.
                Title = x.Element("title").Value, //3
                PublishedDate = DateTime.Parse(x.Element("pubDate").Value),
                Author = x.Element(dc + "creator").Value,
                Url = x.Element("link").Value
            } //  Put them into an object (Post)
            )
                // Order them by the pubDate (Post.PublishedDate).
            .OrderByDescending(x => x.PublishedDate)
                //Only get the amount specified, the top (1, 2, 3, etc.) via postCount.
            .Take(postCount);

            //Convert the feeds to a List and return them.
            return feeds.ToList();
        }
    }
}