using DSharpPlus.CommandsNext;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System.Text.RegularExpressions;
using DSharpPlus;



namespace akz_commands
{
    public class kurs
    {
        string kod { get; set; }
        string miejsca { get; set; }
        string prowadzacy { get; set; }
        string termin { get; set; }

        public kurs(string kod, string miejsca, string prowadzacy, string termin)
        {
            this.kod = kod;
            this.miejsca = miejsca;
            this.prowadzacy = prowadzacy;
            this.termin = termin;
        }

        public string wypisz()
        {
           return String.Format("\t {0} {1} {2} Miejsca={3}", kod, prowadzacy, termin, miejsca);
        }

    }
    public class AKZ : BaseCommandModule
    {
        [Command("akz")]
        [Description("Dostępne kursy w katalogu akz")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task akz(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync().ConfigureAwait(false);
            var katalog = GetHtmlAsync();
                katalog.Wait();
                var akz = new DiscordEmbedBuilder();
                akz.Title = "Dostepne kursy w AKZ";
                akz.WithColor(DiscordColor.IndianRed);
            foreach (KeyValuePair<string, List<kurs>> grupak in katalog.Result.OrderBy(i => i.Key))
                {
                    string wiadomosc = "";
                foreach (var kurs in grupak.Value)
                {
                    wiadomosc += "\n" + kurs.wypisz();
                }
                    akz.AddField(grupak.Key, wiadomosc);
        
                }

            await ctx.Channel.SendMessageAsync(embed: akz).ConfigureAwait(false);
           
            Console.ReadKey();

            static async Task<Dictionary<string, List<kurs>>> GetHtmlAsync()
            {
                var url = "http://akz.pwr.edu.pl/katalog_zap.html";

                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var kursy = htmlDocument.DocumentNode.Descendants("td").ToList();
                int i = 0;

                Regex nowy = new Regex(@"\(\w*\ *\w*\)\ \d*:\d*-\d*:\d*");

                Dictionary<string, List<kurs>> katalogkurs = new Dictionary<string, List<kurs>>();

                for (i = 0; i < (kursy.Count + 1) / 10; i++)
                {
                    if (kursy[i * 10 + 8].InnerText != "II")
                    {
                        if (kursy[i * 10 + 5].InnerText != "0")
                        {
                            MatchCollection mc = nowy.Matches(kursy[i * 10 + 3].InnerText);
                            string dat = " ";
                            foreach(var elements in mc)
                            {
                               dat += elements.ToString() + ", ";
                            }
                            var nowykurs = new kurs(kursy[i * 10 + 1].InnerText, kursy[i * 10 + 5].InnerText,
                                kursy[i * 10 + 4].InnerText, dat);


                            if (katalogkurs.ContainsKey(kursy[i * 10 + 2].InnerText))
                            {
                                katalogkurs[kursy[i * 10 + 2].InnerText].Add(nowykurs);
                            }
                            else
                            {
                                var katalog = new List<kurs>();
                                katalog.Add(nowykurs);
                                katalogkurs.Add(kursy[i * 10 + 2].InnerText, katalog);
                            }
                        }
                    }
                }

                return katalogkurs;
            }
        }
    }
}