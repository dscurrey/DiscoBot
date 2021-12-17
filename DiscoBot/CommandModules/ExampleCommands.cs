using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscoBot.CommandModules
{
    public class ExampleCommands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            var user = Context.User;
            var msgText = $"You are: {user}\nEGG!";

            await ReplyAsync(msgText);
        }

        [Command("necktie")]
        [Alias("ask")]
        public async Task AskNecktie([Remainder] string args = null)
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var replyList = new List<string>()
            {
                "yes",
                "no",
                "maybe",
                "*Bratan*, now is the time!",
                "No. You're going to be mowed down by gunfire from the two remaining mercs."
            };

            embed.WithColor(255, 0, 255);
            embed.Title = "Horrific Necktie";
            embed.ImageUrl =
                "https://static.wikia.nocookie.net/discoelysium_gamepedia_en/images/4/43/Neck_tie.png/revision/latest/scale-to-width-down/512?cb=20191024110230";

            sb.AppendLine($"{Context.User},\n");

            if (args == null)
                sb.AppendLine("*Bratan*, you **must** ask me something.");
            else
            {
                var answer = replyList[new Random().Next(replyList.Count - 1)];
                sb.AppendLine($"*Bratan*! you asked: \"{args}\"");
                sb.AppendLine($"Your answer is {answer}");

                switch (answer)
                {
                    case "yes":
                    case "no":
                    case "maybe":
                        embed.WithColor(50, 205, 50);
                        break;
                    case "*Bratan*, now is the time!":
                    case "No. You're going to be mowed down by gunfire from the two remaining mercs.":
                        embed.WithColor(255, 255, 0);
                        break;
                }
            }

            embed.Description = sb.ToString();
            await ReplyAsync(null, false, embed.Build());
        }
    }
}
