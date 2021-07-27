using System;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using eka.comms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using main_akz;
using eka;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext.Exceptions;
using parameter_akz;


namespace EKA.bot
{

    class Program
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public readonly EventId EKAEventId = new EventId(4, "EKA W4");

        static void Main(string[] args)
        {

            var bot = new Program();
            bot.RunEKAAsync().GetAwaiter().GetResult();
           
        }

        public async Task RunEKAAsync()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<configjson>(json);
            var config = new DiscordConfiguration
            {
                Token = configJson.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,

            };

            Client = new DiscordClient(config);

            Client.Ready += ClientReady;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true,

            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<commands>();
            Commands.RegisterCommands<AKZMain>();
            Commands.RegisterCommands<AKZParametr>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }
        private Task ClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
        private Task ClientError(DiscordClient sender, ClientErrorEventArgs e)
        {
            sender.Logger.LogError(EKAEventId , e.Exception, "Blad");
            return Task.CompletedTask;
        }

        
    }
}
