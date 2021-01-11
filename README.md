# **Prove of Concept** Understanding Website structre and extracting Media files

Task: Getting the URLs for Media Files (MP3/ MP4) by a scraping program, pass them via json file to the Media Player Program which can mirror it to any chromecast devices

# First Problem/ Understanding 

The html structure of a website is mostly unique -> for every website there has to be an individual html path
'''csharp
            
            Console.WriteLine("Bitte geben sie eine Webseiten URL ein die druchsucht werden soll:");

            string webseite = null;
            webseite = Console.ReadLine();


            // donwload source coode of site
            WebClient wc = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            wc.DownloadFile(webseite, @"sourceFile.html");
'''

