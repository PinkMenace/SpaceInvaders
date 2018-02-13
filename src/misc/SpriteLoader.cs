using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.src.misc
{
    class SpriteLoader
    {
        public static ContentManager ContentManager { get; set; }
        public static GraphicsDeviceManager GraphicsManager { get; set; }

        public static List<SpriteFont> Fonts { get; set; }

        public static List<SpriteFont> LoadSpriteFonts()
        {
            List<SpriteFont> fonts = new List<SpriteFont>();

            List<string> FontsAvailable = new List<string>();
            FontsAvailable.Add(@"Fonts\defaultFont");

            foreach (string font in FontsAvailable)
                fonts.Add(ContentManager.Load<SpriteFont>(font));

            return fonts; 
        }

        public static Texture2D LoadTexture(string path)
        {
            try
            {
                return ContentManager.Load<Texture2D>(path);
            }
            catch (ContentLoadException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
