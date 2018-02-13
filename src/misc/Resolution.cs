using Microsoft.Xna.Framework;

namespace SpaceInvaders.src.misc
{
    struct Resolution
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public Resolution(int Width, int Height)
        {
            ScreenWidth = Width;
            ScreenHeight = Height;
        }

        public Vector2 getScale()
        {
            // The orginal sprites where displayed at 256x224
            int DefaultScreenWidth = 256;
            int DefaultScreenHeight = 224;

            int PosXScale = ScreenWidth / DefaultScreenWidth;
            int PosYScale = ScreenHeight / DefaultScreenHeight;

            return new Vector2(PosXScale, PosYScale);
        }

        public Vector2 getCenterOfScreen()
        {
            int PosX = ScreenWidth >> 1;
            int PosY = ScreenHeight >> 1;

            return new Vector2(PosX, PosY);
        }
    }
}
