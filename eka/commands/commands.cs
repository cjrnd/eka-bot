using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DSharpPlus;


namespace eka.comms
{
    public class commands : BaseCommandModule
    {

        [Command("test")]
        [Description("test")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task test(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("test").ConfigureAwait(false);
        }
        [Command("cal")]
        public async Task cal(CommandContext ctx)
        {
            var calendar = new DiscordEmbedBuilder();
            calendar.WithColor(DiscordColor.IndianRed);
            calendar.WithTitle("Kalendarz AKZ");
            calendar.AddField("GODZINA","tutaj tego", true);
            calendar.AddField("PONIEDZIAŁEK","zajecia jeden", true);
            calendar.AddField("WTOREK","zajecia dwa", true);
            calendar.AddField("ŚRODA","zajecia trzy", true);
            calendar.AddField("CZWARTEK","cztery ", true);
            calendar.AddField("PIĄTEK","no i pięć", true);
            




            var calmes = await ctx.Channel.SendMessageAsync(embed: calendar).ConfigureAwait(false);
        }


    [Command("rola")]
        [Description("Przydzielenie roli")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task join(CommandContext ctx)
        {
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Chcesz role?",
                ImageUrl = ctx.Client.CurrentUser.AvatarUrl,
                Color = DiscordColor.Green
            };
            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var reactionResult = await interactivity.WaitForReactionAsync(x => x.Message == joinMessage && x.User == ctx.User && (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji)).ConfigureAwait(false);

            if (reactionResult.Result.Emoji == thumbsUpEmoji)
            {
                var role = ctx.Guild.GetRole(698475931739881524);
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == thumbsDownEmoji)
            {
                var role = ctx.Guild.GetRole(698475931739881524);
                await ctx.Member.RevokeRoleAsync(role).ConfigureAwait(false);
            }
            else
            {
                //oj nie byqu
            }
            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }


        [Command("orzel")]
        [Description("orzel ")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task orzel(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("orzel mam dla ciebie cebularz").ConfigureAwait(false);
        }


        [Command("anno"), Aliases("og"), Description("do oglaszania wiadomosci")]
        public async Task Anno(CommandContext ctx, string MessageType )
        {
           
            await ctx.TriggerTypingAsync().ConfigureAwait(false);
            var AnnoEmbed = new DiscordEmbedBuilder();
            AnnoEmbed.WithTitle("Ogłoszenie:");
            AnnoEmbed.WithDescription(MessageType);
            var AnnoMessage = await ctx.Channel.SendMessageAsync(embed: AnnoEmbed).ConfigureAwait(false);
        
        }


        [Command("ankieta")]
        [Description("Tworzenie ankiety")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task ankieta(CommandContext ctx, string tytul,params DiscordEmoji[] emojiOption)
        {
           
            var interactivity = ctx.Client.GetInteractivity();

            var opcje = emojiOption.Select(x => x.ToString());

            var xde = "᲼᲼᲼᲼᲼᲼"; 

            var ankietaembed = new DiscordEmbedBuilder();
            ankietaembed.WithTitle("Ankieta: ");
            string.Join("", opcje);
            foreach (var opcja in emojiOption)
            {
               ankietaembed.AddField(String.Format("{0} {1} {0}",opcja.ToString(), tytul), xde);
            }
           
            ankietaembed.WithImageUrl("https://i.imgur.com/1yG28tM.jpg");

            var ankietaMessage = await ctx.Channel.SendMessageAsync(embed: ankietaembed).ConfigureAwait(false);

            foreach(var opcja in emojiOption)
            {
                await ankietaMessage.CreateReactionAsync(opcja).ConfigureAwait(false);
            }

        }
         



    }
}
