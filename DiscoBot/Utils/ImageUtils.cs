using System.Drawing;
using System.Drawing.Text;

#pragma warning disable CA1416


namespace DiscoBot.Utils
{
    public class ImageUtils
    {
        private static PrivateFontCollection LoadFonts()
        {
            var collection = new PrivateFontCollection();
            collection.AddFontFile(@"resources\LibreBaskerville-Regular.ttf");
            collection.AddFontFile(@"resources\LibreBaskerville-Bold.ttf");
            collection.AddFontFile(@"resources\LibreBaskerville-Italic.ttf");
            return collection;
        }

        public static Image DrawSimpleDisco(string text)
        {
            var fontCollection = LoadFonts();
            var img = new Bitmap(1, 1);
            var drawing = Graphics.FromImage(img);

            var font = new Font(new FontFamily("Libre Baskerville", fontCollection), 36);
            var textSize = drawing.MeasureString(text, font);

            img.Dispose();
            drawing.Dispose();

            img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(Color.Gray);

            var textBrush = new SolidBrush(Color.White);
            drawing.DrawString(text, font, textBrush, 0, 0);
            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}

#pragma warning restore CA1416
