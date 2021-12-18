using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

#pragma warning disable CA1416


namespace DiscoBot.Utils
{
    public class ImageUtils
    {
        private static Font GetRegular()
        {
            var collection = new PrivateFontCollection();
            collection.AddFontFile(@"resources\LibreBaskerville-Regular.ttf");
            return new Font(new FontFamily("Libre Baskerville", collection), 36, FontStyle.Bold);
        }

        private static Font GetItalic()
        {
            var collection = new PrivateFontCollection();
            collection.AddFontFile(@"resources\LibreBaskerville-Italic.ttf");
            return new Font(new FontFamily("Libre Baskerville", collection), 36, FontStyle.Bold);
        }

        private static Font GetBold()
        {
            var collection = new PrivateFontCollection();
            collection.AddFontFile(@"resources\LibreBaskerville-Bold.ttf");
            return new Font(new FontFamily("Libre Baskerville", collection), 36, FontStyle.Bold);
        }

        public static Image DrawSimpleDiscoWithFlavor(string title, string text, string flavor)
        {
            var img = new Bitmap(1, 1);
            var drawing = Graphics.FromImage(img);

            var size = drawing.MeasureString(title + text + flavor, GetItalic());
            var titleSize = drawing.MeasureString(title, GetBold());
            var messageSize = drawing.MeasureString(text, GetRegular());
            var flavorSize = drawing.MeasureString(flavor, GetItalic());

            img.Dispose();
            drawing.Dispose();

            var width = titleSize.Width + messageSize.Width + flavorSize.Width;

            img = new Bitmap((int)width, (int)size.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(Color.Gray);

            var textBrush = new SolidBrush(Color.White);
            drawing.DrawString(title, GetBold(), textBrush, 0, 0);
            drawing.DrawString(text, GetRegular(), textBrush, titleSize.Width, 0);
            drawing.DrawString(flavor, GetItalic(), textBrush, titleSize.Width + messageSize.Width, 0);
            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        public static Image DrawSimpleDisco(string author, string text)
        {
            Enum.TryParse(typeof(Skill), author, true, out var enumSkill);
            if (enumSkill is not Skill skill)
                return null;
            author = SkillsUtil.GetSkillName(skill);

            var img = new Bitmap(1, 1);
            var drawing = Graphics.FromImage(img);

            author += " - ";
            var size = drawing.MeasureString(author + text, GetItalic());
            var titleSize = drawing.MeasureString(author, GetBold());
            var messageSize = drawing.MeasureString(text, GetRegular());

            img.Dispose();
            drawing.Dispose();

            var width = titleSize.Width + messageSize.Width;

            img = new Bitmap((int)width, (int)size.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(Color.DarkSlateGray);

            var textBrush = new SolidBrush(SkillsUtil.GetSkillColour(skill));
            drawing.DrawString(author, GetBold(), textBrush, 0, 0);
            textBrush.Color = Color.White;
            drawing.DrawString(text, GetRegular(), textBrush, titleSize.Width, 0);
            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        public static Image DrawSimpleDisco(string text)
        {
            var img = new Bitmap(1, 1);
            var drawing = Graphics.FromImage(img);
            var title = "MESSAGE - ";
            var flavor = "egg";

            var fullTesx = title + text + flavor;
            var size = drawing.MeasureString(fullTesx, GetItalic());
            var titleSize = drawing.MeasureString(title, GetBold());
            var messageSize = drawing.MeasureString(text, GetRegular());
            var flavorSize = drawing.MeasureString(flavor, GetItalic());

            img.Dispose();
            drawing.Dispose();

            var width = titleSize.Width + messageSize.Width + flavorSize.Width;

            img = new Bitmap((int)width, (int)size.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(Color.Gray);

            var textBrush = new SolidBrush(Color.White);
            drawing.DrawString(title, GetBold(), textBrush, 0, 0);
            drawing.DrawString(text, GetRegular(), textBrush, titleSize.Width, 0);
            drawing.DrawString(flavor, GetItalic(), textBrush, titleSize.Width + messageSize.Width, 0);
            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}

#pragma warning restore CA1416
