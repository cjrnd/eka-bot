using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace eka.comms
{
    public class commands : BaseCommandModule
    {

        [Command("air")]
        [Description("no wiadomo co i kogo")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task air(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("J***C AIR").ConfigureAwait(false);
        }

       /* [Command("rola")]

        [Description("rola byku")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task join(CommandContext ctx)
        {
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Byku chcesz role?",
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

            if (reactionResult.Result.Emoji == thumbsDownEmoji)
            {
                var role = ctx.Guild.GetRole(idroli);
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == thumbsUpEmoji)
            {
                var role = ctx.Guild.GetRole(idroli);
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else
            {
                //oj nie byqu
            }
            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }*/


        [Command("orzel")]
        [Description("orzel ")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task orzel(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("orzel mam dla ciebie cebularz").ConfigureAwait(false);
        }


        [Command("dodaj")]
        [Description("dodawanie mordo")]
        //[RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
        public async Task dodaj(CommandContext ctx, [Description("pierwsza liczba mordo")] int x, [Description("druga liczba mordo")] int y)
        {
            await ctx.Channel.SendMessageAsync((x + y).ToString()).ConfigureAwait(false);
        }

        [Command("navyseal"), Aliases("gorillawarfare"), Description("What the fuck did you just say to me?")]
        public async Task NavySeal(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.");
        }


        [Command("ankieta")]
        [Description("Tworzenie ankiety")]
        // [RequireRoles(RoleCheckMode.Any, "Edytor", "Admin", "Starostwo")]
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
