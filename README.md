# **Prove of Concept** Understanding Website structre and extracting Media files

Task: Getting the URLs for Media Files (MP3/ MP4) by a scraping program, pass them via json file to the Media Player Program which can mirror it to any chromecast devices

# First Problem/ Understanding 

The html structure of a website is mostly unique -> for every website there has to be an individual html path
```csharp
            
            Console.WriteLine("Bitte geben sie eine Webseiten URL ein die druchsucht werden soll:");

            string webseite = null;
            webseite = Console.ReadLine();


            // donwload source coode of site
            WebClient wc = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            wc.DownloadFile(webseite, @"sourceFile.html");
``` 

# Workaround use Parsehub

Parsehub is a GUI scraping program to scrape websites and converting scraped data into a json file.
-> **Interface** for my Mediaplayer program
Source: https://www.parsehub.com

# Designing Pattern 

The program is a Xamarin Project. To correctly follow the MVVM-Pattern I used the prism framework.  
Source:https://github.com/PrismLibrary/Prism  
The prism framework uses a navigationservice which works with *Dependency Injection* to call views only from the dependent viewmodel
Source: https://prismlibrary.com/docs/xamarin-forms/navigation/navigation-basics.html

# Choosing Player Plugin

**Xam.Forms.VideoPlayer** 
Is a very performant & powerful Plugin.   
*Advantage:*  
Playing Videos from 3. Party Streaming Services  
*Disadvantage:*  
It doesnt work with the prism framework  
Source:https://github.com/pro777s/Xam.Forms.VideoPlayer  

**Alternativ: LibVLCSharp.Forms**
*Advantage:*  
- Can be integrated with the MVVM-Pattern
- Build-in Chromecast feature 
*Disadvantage:* 
- Has performace issuse while being used in the android simulator 
- needs a url which only points to a media file  
Bsp:  
Cant Play: https://www.ardmediathek.de/ard/sendung/charite/staffel-1/Y3JpZDovL2Rhc2Vyc3RlLmRlL2NoYXJpdGU/1/  
Can Play: https://nrodlzdf-a.akamaihd.net/none/zdf/20/12/201219_sendung_f21/2/201219_sendung_f21_2128k_p18v15.webm  
Source: https://github.com/videolan/libvlcsharp/blob/3.x/src/LibVLCSharp.Forms/README.md







