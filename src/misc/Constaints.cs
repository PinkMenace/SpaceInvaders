using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SpaceInvaders.src.misc
{
    class Constaints
    {
        public static Resolution ScreenResolution = new Resolution(1024, 800);

        private static Rectangle setPlayableGameArea()
        {
            double MinScreenBuffer = .02;
            double MaxScreenBuffer = .98;

            return new Rectangle((int)(ScreenResolution.ScreenWidth * MinScreenBuffer), (int)(ScreenResolution.ScreenHeight * MinScreenBuffer),
                (int)(ScreenResolution.ScreenWidth * MaxScreenBuffer), (int)(ScreenResolution.ScreenHeight * MaxScreenBuffer));
        }

        public static Rectangle GameArea = setPlayableGameArea();
    }
}
