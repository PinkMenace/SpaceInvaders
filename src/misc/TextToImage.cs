using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using Stream = System.IO.MemoryStream;

namespace SpaceInvaders.src.misc
{
    /// <summary>
    /// This class takes the a string of charaters (A-Z, 0-9, and and few speacial charaters (*, ?, =, -) into 
	/// a single image texture that can be used by xna.
    /// </summary>
    class TextToImage
    {
        private readonly int DEFAULT_IMAGE_WIDTH = 250;
        private readonly int DEFAULT_IMAGE_HEIGHT = 10;

        private readonly Color NULL_COLOR = Color.FromArgb(244, 244, 244);
        private readonly Color DEFAULT_FILL_COLOR = Color.Transparent;

        public Texture2D Image { get; set; }

        public TextToImage(string Text, Color? FillColor)
        {
            Text = Text.ToUpper();

            Bitmap MainImage = new Bitmap(DEFAULT_IMAGE_WIDTH, DEFAULT_IMAGE_HEIGHT);

            for (int x = 0; x < DEFAULT_IMAGE_WIDTH; x++)
            {
                for (int y = 0; y < DEFAULT_IMAGE_HEIGHT; y++)
                {
                    MainImage.SetPixel(x, y, NULL_COLOR);
                }
            }

            int OffSetX = 0;
            int OffSetY = 0;

            char[] charArray = Text.ToCharArray();

            Stream stream = new Stream();
            for (int x = 0; x < charArray.Length; x++)
            {
                ResetStream(stream);
                char currentChar = charArray[x];

                Texture2D currentCharImage = getChar(currentChar);
                currentCharImage.SaveAsPng(stream, currentCharImage.Width, currentCharImage.Height);

                ResetStream(stream);
                Bitmap bitmapCharImage = new Bitmap(stream);

                MainImage = MergeImages(ref MainImage, (Bitmap)bitmapCharImage, ref OffSetX, ref OffSetY, FillColor);
            }

            MainImage = TrimImage(MainImage);

            ResetStream(stream);

            MainImage.Save(stream, ImageFormat.Png);

            Image = Texture2D.FromStream(SpriteLoader.GraphicsManager.GraphicsDevice, stream);
        }

        /// <summary>
        /// We need to reset the stream each time we read/write to it.
        /// </summary>
        /// <param name="stream"></param>
        private void ResetStream(Stream stream)
        {
            stream.Seek(0, System.IO.SeekOrigin.Begin);
        }

        private Texture2D getChar(char c)
        {
            switch (c)
            {
                case 'A':
                    return ResourceCache.FontTextures[0];
                case 'B':
                    return ResourceCache.FontTextures[1];
                case 'C':
                    return ResourceCache.FontTextures[2];
                case 'D':
                    return ResourceCache.FontTextures[3];
                case 'E':
                    return ResourceCache.FontTextures[4];
                case 'F':
                    return ResourceCache.FontTextures[5];
                case 'G':
                    return ResourceCache.FontTextures[6];
                case 'H':
                    return ResourceCache.FontTextures[7];
                case 'I':
                    return ResourceCache.FontTextures[8];
                case 'J':
                    return ResourceCache.FontTextures[9];
                case 'K':
                    return ResourceCache.FontTextures[10];
                case 'L':
                    return ResourceCache.FontTextures[11];
                case 'M':
                    return ResourceCache.FontTextures[12];
                case 'N':
                    return ResourceCache.FontTextures[13];
                case 'O':
                    return ResourceCache.FontTextures[14];
                case 'P':
                    return ResourceCache.FontTextures[15];
                case 'Q':
                    return ResourceCache.FontTextures[16];
                case 'R':
                    return ResourceCache.FontTextures[17];
                case 'S':
                    return ResourceCache.FontTextures[18];
                case 'T':
                    return ResourceCache.FontTextures[19];
                case 'U':
                    return ResourceCache.FontTextures[20];
                case 'V':
                    return ResourceCache.FontTextures[21];
                case 'W':
                    return ResourceCache.FontTextures[22];
                case 'X':
                    return ResourceCache.FontTextures[23];
                case 'Y':
                    return ResourceCache.FontTextures[24];
                case 'Z':
                    return ResourceCache.FontTextures[25];

                // Numberals
                case '0':
                    return ResourceCache.FontTextures[26];
                case '1':
                    return ResourceCache.FontTextures[27];
                case '2':
                    return ResourceCache.FontTextures[28];
                case '3':
                    return ResourceCache.FontTextures[29];
                case '4':
                    return ResourceCache.FontTextures[30];
                case '5':
                    return ResourceCache.FontTextures[31];
                case '6':
                    return ResourceCache.FontTextures[32];
                case '7':
                    return ResourceCache.FontTextures[33];
                case '8':
                    return ResourceCache.FontTextures[34];
                case '9':
                    return ResourceCache.FontTextures[35];

                // Misc charaters
                case '-':
                    return ResourceCache.FontTextures[36];
                case '*':
                    return ResourceCache.FontTextures[37];
                case '=':
                    return ResourceCache.FontTextures[38];
                case '?':
                    return ResourceCache.FontTextures[39];
                default:
                    return ResourceCache.FontTextures[40];
            }
        }

        private Bitmap MergeImages(ref Bitmap dest, Bitmap src, ref int XOffSet, ref int YOffset, System.Drawing.Color? FillColor = null)
        {
            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    Color c = src.GetPixel(x, y);

                    if (FillColor.HasValue)
                    {
                        if (c == Color.FromArgb(0, 0, 0, 0))
                            c = FillColor.Value;
                    }

                    dest.SetPixel(x + XOffSet, y + YOffset, c);
                }
            }

            // Assuming we are working with the letter 'I', the letter 'I' has pixel width of 3, and a pixel height of 7.
            // After the image has been copied over we want to update the XOffset to the width of the image + 1, (3 + 1 in this case),
            // so when we start copying over the next image we can start at pixel/index 4 of the dest image.
            //
            // The next for loop we are adding a single pixel line between the letters. We start off by subtracting 1 from the Xoffset so we are working 
            // with x = (XOffSet(4) - 1 = 3) . The letter 'I' is contained in pixels (x = (0 - 2), y = (0 - 6)). We only need to go through the height Y times.

            XOffSet += src.Width + 1;

            
            for (int y=0;y < src.Height; y++)
            {
                Color c = FillColor.HasValue ? FillColor.Value : DEFAULT_FILL_COLOR;

                dest.SetPixel(XOffSet - 1, y, c);
            }

            return dest;
        }
        
        private Bitmap TrimImage(Bitmap image)
        {
            int EndOfImageX = 0;
            int EndOfImageY = 0;

            // Two loops are used independently rather than a single loop so we can avoid checking every pixel.
            // In both loops are a looking for the first null pixel, logging the pixel(s) information and exiting the loop.
            // After each loop is done we create a new bitmap the exact size we need and copy everything over so it 'fits'.
            int x = 0;
            while (x < image.Width)
            {
                Color pixelValue = image.GetPixel(x, 0);

                if (pixelValue == NULL_COLOR)
                {
                    EndOfImageX = x;
                    break;
                }
                else
                    x++;
            }

            int y = 0;
            while (y < image.Height)
            {
                Color pixelValue = image.GetPixel(0, y);

                if (pixelValue == NULL_COLOR)
                {
                    EndOfImageY = y;
                    break;
                }
                else
                    y++;
            }

            Bitmap newImage = new Bitmap(EndOfImageX, EndOfImageY);

            for (x = 0; x < newImage.Width; x++)
            {
                for (y = 0; y < newImage.Height; y++)
                {
                    Color c = image.GetPixel(x, y);
                    newImage.SetPixel(x, y, c);
                }
            }

            return newImage;
        }
    }
}