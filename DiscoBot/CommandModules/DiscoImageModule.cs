using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiscoBot.Utils;
using Discord.Commands;

#pragma warning disable CA1416

namespace DiscoBot.CommandModules
{
    public class DiscoImageModule : ModuleBase
    {
        [Command("test")]
        public async Task TestDrawCommand([Remainder] string args = null)
        {
            await Context.Channel.TriggerTypingAsync();

            Image image;
            if (args == null)
                image = ImageUtils.DrawSimpleDisco("TEST");
            else
                image = ImageUtils.DrawSimpleDisco(args);

            var filename = Guid.NewGuid() + ".jpg";
            image.Save(filename);

            await Context.Channel.SendFileAsync(filename);

            File.Delete(filename);
        }
    }
}

#pragma warning restore CA1416
