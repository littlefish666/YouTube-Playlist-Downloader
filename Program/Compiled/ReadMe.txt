======================YouTube Playlist Downloader by Frazzlee=====================

Thanks for downloading this, hope you find it as useful as me.
Due to some unknown reason (will look at this later, it doesn't provide direct link to each one directly, but rather some just go to a page where you have to click download, I will work on this when I have more time)
If you want to look at the source but don't quite understand everything, look into the "EXPLAINED" folder, where I included the .cs file but with some comments in a hope to explain what important lines do
This took me quite some time so please star this if you have a Github account (:
p.s. I have to look more into the API to get it to load more than 50 videos at a times, I know it's possible, since I've seen Google saying about it, just not sure how atm... sorry

some playlists to test it with:
https://www.youtube.com/playlist?list=PLesuPwd8h_-DHkpkbcxJzpSAfP7MD9cs2
https://www.youtube.com/playlist?list=PLesuPwd8h_-A-t2V6Grnd-9OSCyzneeYx

Read me before running the program:
You'll notice there's 2 versions of this, lite version and developer version, both the same size and do basically the same thing but with a few changes
If you just want to download a playlist, you can stop reading here and just go ahead and use the lite version
The developer version dumps some files into the directory from which it's being run from just so you can debug or inspect stuff along the way
This includes, the source that I'm given through the YouTube API, the number of the lines which contain the video ID's , and the actual Video ID's

Documentation:
URL that you paste in MUST be in the following format: https://www.youtube.com/playlist?list=PLesuPwd8h_-DHkpkbcxJzpSAfP7MD9cs2 with the https included, otherwise it won't subtract the correct number of characters and you won't get anywhere
If your browser doesn't use HTTPS just amend your URL to fit the format of the one above, I originally had it so you just had to enter the platlist ID but amended it so I could give some error messages if format was wrong... might make it so that you can enter both

How this actually works:
If you don't understand the code, then here's a simple explanation
I use the YouTube API to get the important stuff from the playlist URL - the result I get from the YouTube API can be viewed in the "dump.txt" if you choose to run the developer edition
YouTube API Format:
https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId= --> PLAYLISTIDHERE <-- &key=AIzaSyAGPYIA5Zz3m1WavVFmw35Fw5mvLUkUyeY

From that result, I find the lines which begin with "VideoID", as these contain the unique indentifier for the YouTube URL (the part at the end) https://www.youtube.com/ --> watch?v=9m5DFlfVcOw <--
With this I now have each indentifier for each video, now all that's left to do is download the mp3, or get a direct link to an mp3 file
To get this direct link I use the youtubeinmp3 API which allows me to parse a direct download link to the mp3, more complicated than the YouTube API but nevertheless quite easy to work with
YouTubeInMP3 API Format:
www.youtubeinmp3.com/fetch/?video= --> YOUTUBE VIDEO LINK HERE <--
By opening that link from the app, it takes you to the direct location of the mp3, and downloads it via browser to your default download location, as always id3 tags are included in this download and the mp3 is highest quality available

Notes:

Source is included so you don't have to decompile it yourself :)
Written in C#
This is my first time using the YouTube API and I don't know how the key privacy works, this release publicly includes the key that I had generated for me specifically for this application
If you're going to amend this in any way, you can use however much of this code as you wish, however I'd request that you generate your own YouTube V3 API key from here : https://console.developers.google.com
Here's the link to the YouTubeInMP3 API - https://www.youtubeinmp3.com/api/ - no key required for this, just the correct format, API that I use is the "Direct Link" one, easiest and best to use in my opinion
Should work on all version of Windows though I've only tested on Win10 with latest framework installed (targets 4.5.2)
Any questions or if you want to contact me looka here - https://frazzlee.github.io
If you compare the "lines.txt" to the "dump.txt" file you'll see that the number in the "lines.txt" file is actually 1 less, because in fact line 1 = 0 and 2 = 1 etc
P.S. sorry for some untidy code, I'm not fussed since this isn't code on a big scale, I'll try to clean it up in future updates

======================Thanks======================
thanks to CNuts for helping me work with the indexing of lines - http://stackoverflow.com/users/7503064/cnuts
thank you degant for helping me work with indexing of lines also- http://stackoverflow.com/users/1186936/degant
much love also to Jawal69 for helping me to work with arrays also *does A453 once* ;)
Props to the people down at youtubeinmp3, this wouldn't be possible without you