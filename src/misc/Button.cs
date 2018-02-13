using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.src.input;

namespace SpaceInvaders.src.misc
{
    class Button
    {
        private Rectangle buttonBounds; 
        Texture2D image;

        public Button(Vector2 Position, int Width, int Height, Texture2D ButtonImage)
        {
            buttonBounds = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            image = ButtonImage;
        }
        public Button(int PosX, int Posy, int Width, int Height, Texture2D ButtonImage)
        {
            buttonBounds = new Rectangle(PosX, Posy, Width, Height);
            image = ButtonImage;
        }

        /// <summary>
        /// Due to the nature of working inside of XNA we are required to check on every update cycle to see if the user has
        /// clicked thier mouse.
        /// </summary>
        /// <returns></returns>
        public bool Click()
        {
            if (InputHandler.IsMouseLeftButtonClicked() && InputHandler.MouseIntersects(buttonBounds))
                return true;

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (image != null)
                spriteBatch.Draw(image, buttonBounds, Color.White);
        }
    }
}
