using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.src.animation
{
    class Sprite : AnimatibleObject
    {
        protected bool isCollidable;

        protected Vector2 position;
        protected Vector2 delta;

        public Sprite(Vector2 SpawnPosition) : base()
        {
            DrawOptions = new DrawOptions();

            position = SpawnPosition;
            delta = new Vector2();
            isCollidable = true;
        }

        public bool Collision(Sprite s)
        {
            if (s == null || this == s || !isCollidable || !s.isCollidable)
                return false;

            return
               (position.X <= s.position.X + s.getWidth() &&
                (position.X + getWidth()) >= s.position.X &&
                position.Y <= s.position.Y + s.getHeight() &&
                position.Y >= s.position.Y);
        }

        internal override void Update(int time)
        {
            Animation.Update(time);

            position += delta;
            delta = new Vector2();
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animation.getTexture(), position, DrawOptions.DestinationRectangle, DrawOptions.SourceRectangle, DrawOptions.Origin, DrawOptions.Rotation, DrawOptions.Scale, DrawOptions.Color, DrawOptions.SpriteEffects, DrawOptions.DepthLayer);
        }

        #region Position/Delta Functions

        public Vector2 Position { get { return position; } }
        internal void setPosition(float X, float Y) { position.X = X; position.Y = Y; }
        internal void addPosition(float X, float Y) { position.X += X; position.Y += Y; }

        public Vector2 Delta { get { return delta; } }
        internal void setDelta(float X, float Y) { delta.X = X; delta.Y = Y; }
        internal void addDelta(float X, float Y) { delta.X += X; delta.Y += Y; }

        #endregion

        public double getWidth() { return Animation.TextureWidth * DrawOptions.Scale.X; }
        public double getHeight() { return Animation.TextureHeight * DrawOptions.Scale.Y; }
    }
}
