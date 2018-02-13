using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.src.animation
{
    class DrawOptions
    {
        public Rectangle? SourceRectangle { get; set; }
        public Rectangle? DestinationRectangle { get; set; }
        public Vector2? Origin { get; set; }
        
        public float DepthLayer { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        
        public DrawOptions()
        {
            SourceRectangle = null;
            DestinationRectangle = null;
            Origin = null;

            DepthLayer = 0;
            Rotation = 0;
            Color = Color.White;
            Scale = new Vector2(1);
            SpriteEffects = SpriteEffects.None;
        }
    }
}
