using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Program

    {
        [STAThread]
        static void Main(string[] args)
        {
            string False = "False";
            string S1 = "https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId=";
            string S2 = "&key=AIzaSyAGPYIA5Zz3m1WavVFmw35Fw5mvLUkUyeY";
            string URLFormat = @"https://www.youtube.com/playlist?list=";
            string insert = "www.youtubeinmp3.com/fetch/?video=https://www.youtube.com/watch?v=";
            int width = 200;
            int height = 60;
            Console.SetWindowSize(width, height);

            Console.Title = "YouTube Playlist URL parser by Frazzlee";
            Console.WriteLine("Removing previous files...");
            File.Delete("lines.txt");
            File.Delete("Dump.txt");
            File.Delete("LinesNEW.txt");
            File.Delete("RAWid.txt");
            File.Delete("PreOpen.txt");
            Thread.Sleep(100);
            bool isInternetConnected = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            Console.WriteLine("Is Internet Connected / Are connections available :     {0}", isInternetConnected);

            if (String.ReferenceEquals(isInternetConnected, False))
            {
                Console.WriteLine("Cannot connect to internet, please check connections");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            
            Console.WriteLine(@"Only works up to 50 videos, since the API only gives me the videos that are loaded as standard on the browser (load more button) , will fix soon");           
            Console.WriteLine("Enter full playlist Link:");
            string fullURLuser = Console.ReadLine();
            System.IO.File.WriteAllText("Playlist.txt", fullURLuser);

            string isYouTube = fullURLuser.Remove(fullURLuser.Length - 34);
            if (!isYouTube.Equals(URLFormat))
            {
                Console.WriteLine("Wrong URL Format (most likely isn't a YouTube link), please check documentation for more details");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            else
                Console.WriteLine("URL seems to be ok...");

            string playlistID = fullURLuser.Substring(38);
            string fullURL = S1 + playlistID + S2;
            Console.WriteLine("REMOVE LATER: Full URL: {0}",fullURL);
            Console.WriteLine("Parsing HTML Code");
            Thread.Sleep(1000);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;            
                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                string data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                Console.WriteLine(data);               

                Console.Clear();
                Thread.Sleep(800);
                Console.WriteLine(@"Dumping API result to ""dump.txt""");
                System.IO.File.WriteAllText("dump.txt", data);
                Thread.Sleep(500);
                Console.WriteLine("Dump finished");

                string[] dumpFile = System.IO.File.ReadAllLines("dump.txt");
                string[] selected = dumpFile.Where(line => line.StartsWith(@"     ""videoId"":")).ToArray();               
                System.IO.File.AppendAllLines("linesNEW.txt", selected);

                Console.WriteLine("VideoID Positions succesfully found and extracted");
                string[] videoID = System.IO.File.ReadAllLines("LinesNEW.txt");

                for (int i = 0; i < videoID.Length; i++)
                {
                    videoID[i].Substring(17);
                }
                Console.Clear();

                for (int i = 0; i < videoID.Length; i++)
                    videoID[i] = videoID[i].Replace(@"     ""videoId"": """, "INSERT");
                for (int i = 0; i < videoID.Length; i++)
                    videoID[i] = videoID[i].Replace(@"""", string.Empty);
                for (int i = 0; i < videoID.Length; i++)
                    videoID[i] = videoID[i].Replace(@"INSERT", insert);
                System.IO.File.AppendAllLines("PreOpen.txt", videoID);
                WebClient wb = new WebClient();
                for (int i = 0; i < videoID.Length; i++)
                    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", videoID[i].ToString());
                    Thread.Sleep(5000);

                System.IO.File.AppendAllLines("RAWid.txt", videoID);
                Console.WriteLine("All done! : ) don't forget to star");
            }
            else
            {
                Console.WriteLine("Connection could not be established , please check internet connection or firewall settings");
            }              
                Console.ReadLine();
            
        }
    }
}
