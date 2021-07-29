This is a simple web scraping bot made for Discord using C# and DsharpPlus library. It's main purpose is to get informations from web page about courses avaiable in Wrocław University of Science and Technology and send them back to users.<br/>
The web scraping part was done with help of HtmlAgilityPack library
```
var url = "http://akz.pwr.edu.pl/katalog_zap.html";

                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var courses = htmlDocument.DocumentNode.Descendants("td").ToList();
```

![how its working](dc.gif)

