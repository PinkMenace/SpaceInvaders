using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SpaceInvaders.src;
using SpriteLoader = SpaceInvaders.src.misc.SpriteLoader;
using Constaints = SpaceInvaders.src.misc.Constaints;

namespace SpaceInvaders
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpaceInvaderGameWindow game;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {
            SetWindowSize();

            SpriteLoader.ContentManager = Content;
            SpriteLoader.GraphicsManager = graphics;
            game = new SpaceInvaderGameWindow();

            base.Initialize();
            
        }

        private void SetWindowSize()
        {
            graphics.PreferredBackBufferWidth = Constaints.ScreenResolution.ScreenWidth;
            graphics.PreferredBackBufferHeight = Constaints.ScreenResolution.ScreenHeight;

            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            game.Update(1);

            src.input.InputHandler.UpdateKeyboardState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (game != null)
                game.Draw(spriteBatch);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
