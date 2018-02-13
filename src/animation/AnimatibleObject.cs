using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.src.animation
{
    class AnimatibleObject
    {
        public AnimatibleObject()
        {
            DrawOptions = new DrawOptions();
            LoadAnimation();
        }

        internal Animation Animation { get; set; }
        internal DrawOptions DrawOptions { get; set; }

        internal virtual void LoadAnimation() { }
        internal virtual void LoadDrawOptions() { }
        internal virtual void Update(int time) { Animation.Update(time); }
        internal virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
