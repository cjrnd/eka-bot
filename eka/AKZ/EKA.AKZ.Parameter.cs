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

namespace AKZ_Parameter
{
    public class Course
    {
        public string CourseCode { get; set; }
        public string CoursePlaces { get; set; }
        public string CourseLeader { get; set; }
        public string CourseDate { get; set; }

        public Course(string CourseCode, string CoursePlaces, string CourseLeader, string CourseDate)
        {
            this.CourseCode = CourseCode;
            this.CoursePlaces = CoursePlaces;
            this.CourseLeader = CourseLeader;
            this.CourseDate = CourseDate;
        }

        public string print()
        {
            return String.Format("\t {0} {1} {2} Miejsca={3}", CourseCode, CourseLeader, CourseDate, CoursePlaces);
        }

    }


    class AKZParametr : BaseCommandModule
    {
        [Command("akzp")]
        [Description("Dostępne kursy w katalogu akz w zależności od wybranego kryterium wyszukiwania")]
        public async Task cal(CommandContext ctx, string Parameter)
        {
            await ctx.TriggerTypingAsync().ConfigureAwait(false);
            var CourseScrape = GetHtmlAsync();
            CourseScrape.Wait();
            var akz = new DiscordEmbedBuilder();
            akz.Title = "Dostepne kursy w AKZ";
            akz.WithColor(DiscordColor.Blue);
            foreach (KeyValuePair<string, List<Course>> Course in CourseScrape.Result.OrderBy(i => i.Key))
            {
                string z = "";
                if (Course.Key.ToLower().Contains(Parameter.ToLower()))
                {
                    foreach (var kurs in Course.Value)
                    {
                        z += "\n" + kurs.print();
                    }

                    akz.AddField(Course.Key, z);
                }
            }


            await ctx.Channel.SendMessageAsync(embed: akz).ConfigureAwait(false);


            static async Task<Dictionary<string, List<Course>>> GetHtmlAsync()
            {
                var url = "http://akz.pwr.edu.pl/katalog_zap.html";

                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var courses = htmlDocument.DocumentNode.Descendants("td").ToList();
                int i = 0;

                Regex nowy = new Regex(@"\(\w*\ *\w*\)\ \d*:\d*-\d*:\d*");

                Dictionary<string, List<Course>> CatalogOfCourses = new Dictionary<string, List<Course>>();

                for (i = 0; i < (courses.Count + 1) / 10; i++)
                {
                    if (courses[i * 10 + 8].InnerText != "II")
                    {
                        if (courses[i * 10 + 5].InnerText != "0")
                        {
                            MatchCollection mc = nowy.Matches(courses[i * 10 + 3].InnerText);
                            string x = " ";
                            foreach (var elements in mc)
                            {
                                x += elements.ToString() + ", ";
                            }
                            var NewCourse = new Course(courses[i * 10 + 1].InnerText, courses[i * 10 + 5].InnerText,
                                courses[i * 10 + 4].InnerText, x);


                            if (CatalogOfCourses.ContainsKey(courses[i * 10 + 2].InnerText))
                            {
                                CatalogOfCourses[courses[i * 10 + 2].InnerText].Add(NewCourse);
                            }
                            else
                            {
                                var Catalog = new List<Course>();
                                Catalog.Add(NewCourse);
                                CatalogOfCourses.Add(courses[i * 10 + 2].InnerText, Catalog);
                            }
                        }
                    }
                }

                return CatalogOfCourses;
            }
        }

    }

}
