using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.src.animation
{
    class Frame
    {
        public Texture2D Texture { get; }
        public double Duration { get; }

        public Frame(Texture2D FrameImage, double FrameDuration)
        {
            Texture = FrameImage;
            Duration = FrameDuration;
        }
    }
}
