using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceInvaders.src.misc
{
    class ResourceCache
    {
        public static List<Texture2D> FontTextures { get; set; }
        public static List<Texture2D> AlienTextures { get; set; }
        public static List<Texture2D> TankTexture { get; set; }
        public static List<Texture2D> BulletTexture { get; set; }

        static ResourceCache()
        {
            LoadAlienTextures();
            LoadTankTextures();
            LoadBulletTextures();
            LoadFontImage();
        }

        private static void LoadAlienTextures()
        {
            AlienTextures = new List<Texture2D>();

            List<string> TextureNames = new List<string>();

            TextureNames.Add(@"Sprites\Alien\AlienShip01");
            TextureNames.Add(@"Sprites\Alien\AlienShip02");
            TextureNames.Add(@"Sprites\Alien\AlienShip03");
            TextureNames.Add(@"Sprites\Alien\AlienShip04");
            TextureNames.Add(@"Sprites\Alien\AlienShip05");
            TextureNames.Add(@"Sprites\Alien\AlienShip06");

            AlienTextures = LoadTextures(TextureNames);

            AlienTextures.TrimExcess();
        }

        private static void LoadTankTextures()
        {
            TankTexture = new List<Texture2D>();

            List<string> TextureNames = new List<string>();

            TextureNames.Add(@"Sprites\Player\Tank");

            TankTexture = LoadTextures(TextureNames);
            TankTexture.TrimExcess();
        }

        private static void LoadBulletTextures()
        {
            BulletTexture = new List<Texture2D>();

            List<string> TextureNames = new List<string>();

            TextureNames.Add(@"Sprites\Bullet\Bullet01");
            TextureNames.Add(@"Sprites\Bullet\Bullet02");

            BulletTexture = LoadTextures(TextureNames);
        }

        private static void LoadFontImage()
        {
            FontTextures = new List<Texture2D>();

            List<string> TextureNames = new List<string>();

            TextureNames.Add(@"Character\Letter - A");
            TextureNames.Add(@"Character\Letter - B");
            TextureNames.Add(@"Character\Letter - C");
            TextureNames.Add(@"Character\Letter - D");
            TextureNames.Add(@"Character\Letter - E");
            TextureNames.Add(@"Character\Letter - F");
            TextureNames.Add(@"Character\Letter - G");
            TextureNames.Add(@"Character\Letter - H");
            TextureNames.Add(@"Character\Letter - I");
            TextureNames.Add(@"Character\Letter - J");
            TextureNames.Add(@"Character\Letter - K");
            TextureNames.Add(@"Character\Letter - L");
            TextureNames.Add(@"Character\Letter - M");
            TextureNames.Add(@"Character\Letter - N");
            TextureNames.Add(@"Character\Letter - O");
            TextureNames.Add(@"Character\Letter - P");
            TextureNames.Add(@"Character\Letter - Q");
            TextureNames.Add(@"Character\Letter - R");
            TextureNames.Add(@"Character\Letter - S");
            TextureNames.Add(@"Character\Letter - T");
            TextureNames.Add(@"Character\Letter - U");
            TextureNames.Add(@"Character\Letter - V");
            TextureNames.Add(@"Character\Letter - W");
            TextureNames.Add(@"Character\Letter - X");
            TextureNames.Add(@"Character\Letter - Y");
            TextureNames.Add(@"Character\Letter - Z");

            TextureNames.Add(@"Character\Letter - 0");
            TextureNames.Add(@"Character\Letter - 1");
            TextureNames.Add(@"Character\Letter - 2");
            TextureNames.Add(@"Character\Letter - 3");
            TextureNames.Add(@"Character\Letter - 4");
            TextureNames.Add(@"Character\Letter - 5");
            TextureNames.Add(@"Character\Letter - 6");
            TextureNames.Add(@"Character\Letter - 7");
            TextureNames.Add(@"Character\Letter - 8");
            TextureNames.Add(@"Character\Letter - 9");

            TextureNames.Add(@"Character\Letter - -");
            TextureNames.Add(@"Character\Letter - asterisk");
            TextureNames.Add(@"Character\Letter - EqualsSign");
            TextureNames.Add(@"Character\Letter - QuestionMark");

            TextureNames.Add(@"Character\Space");

            FontTextures = LoadTextures(TextureNames);

            FontTextures.TrimExcess();
        }

        private static List<Texture2D> LoadTextures(List<string> TextureNames)
        {
            List<Texture2D> textures = new List<Texture2D>();

            foreach(string s in TextureNames)
            {
                Texture2D texture = SpriteLoader.LoadTexture(s);
                textures.Add(texture);
            }

            return textures;
        }
    }
}
