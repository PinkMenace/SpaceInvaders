using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.src.game.world;
using SpaceInvaders.src.misc;
using System;
using System.Collections.Generic;
using DColor = System.Drawing.Color;

namespace SpaceInvaders.src
{
    class SpaceInvaderGameWindow
    {
        ScreenState MenuState;
        GameScreen game;

        List<Button> buttons;
        int[] ButtonIndex;
        int StartButtonIndex;
        int ContinueButtonIndex;
        int ExitGameButtonIndex;
        int ExitProgramButtonIndex;

        Button BStartGame;
        Button BContinuegame;
        Button BExitGame;
        Button BExitProgram;

        public SpaceInvaderGameWindow()
        {
            InitializeButtons();
        }

        public void InitializeButtons()
        {
            try
            {
                StartButtonIndex = 0;
                ContinueButtonIndex = 1;
                ExitGameButtonIndex = 2;
                ExitProgramButtonIndex = 3;

                int CenterX = Constaints.ScreenResolution.ScreenWidth >> 1;
                int CenterY = Constaints.ScreenResolution.ScreenHeight >> 1;

                Texture2D StartButtonTexture = new TextToImage("Start Game", DColor.Blue).Image;
                Texture2D ContinueButtonTexture = new TextToImage("Continue Game", DColor.Blue).Image;
                Texture2D ExitGameButtonTexture = new TextToImage("Exit Game", DColor.Blue).Image;
                Texture2D ExitProgramTexture = new TextToImage("Exit Program", DColor.Blue).Image;

                int ScaleX = (int)(Constaints.ScreenResolution.getScale().X);
                int ScaleY = (int)(Constaints.ScreenResolution.getScale().Y);

                Vector2 StartButtonLocation = new Vector2((CenterX >> 1), CenterY >> 1);
                Vector2 ExitButtonLocation = new Vector2( (CenterX >> 1), CenterY + (CenterY >> 1));

                BStartGame = new Button(StartButtonLocation, StartButtonTexture.Width * ScaleX, StartButtonTexture.Height * ScaleY, StartButtonTexture);
                BContinuegame = new Button(StartButtonLocation, ContinueButtonTexture.Width * ScaleX, ContinueButtonTexture.Height * ScaleY, ContinueButtonTexture);
                BExitGame = new Button(ExitButtonLocation, ExitGameButtonTexture.Width * ScaleX, ExitGameButtonTexture.Height * ScaleY, ExitGameButtonTexture);
                BExitProgram = new Button(ExitButtonLocation, ExitProgramTexture.Width * ScaleX, ExitProgramTexture.Height * ScaleY, ExitProgramTexture);

                buttons = new List<Button>();

                buttons.Add(BStartGame);
                buttons.Add(BContinuegame);
                buttons.Add(BExitGame);
                buttons.Add(BExitProgram);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Update(int time)
        {
            if (input.InputHandler.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                if (game != null)
                    MenuState = (MenuState == ScreenState.MainMenu) ? ScreenState.Playing : ScreenState.MainMenu;

            if (MenuState == ScreenState.Playing)
            {
                if (game != null)
                    GameUpdate(time);
            }

            if (input.InputHandler.IsMouseLeftButtonClicked())
            {
                if (MenuState == ScreenState.MainMenu)
                {
                    if (buttons[StartButtonIndex].Click()) { StartButtonClicked(); }
                    else if (buttons[ExitGameButtonIndex].Click() || buttons[ExitProgramButtonIndex].Click()) { ExitButtonClicked(); }
                }
                else if (MenuState == ScreenState.OptionsMenu)
                {
                    // DisplayMenuOptions
                }
                else
                {
                    // Something WENT WRONG!
                    Console.WriteLine("SOMETHINGS WENT WRONG!!");
                }
            }

            input.InputHandler.Update();
        }

        public void GameUpdate(int time)
        {
            if (game.State == GameState.Playing)
                game.Update(time);
            else if (game.State == GameState.GameOver)
            {
                game = null;
                MenuState = ScreenState.MainMenu;
            }
            else
                game = new GameScreen();
        }

        public void StartButtonClicked()
        {
            if (game == null)
                game = new GameScreen();

            MenuState = ScreenState.Playing;
        }

        public void ExitButtonClicked()
        {
            if(game != null)
            {
                game = null;
                MenuState = ScreenState.MainMenu;
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(MenuState == ScreenState.Playing && game != null)
            {
                game.Render(spriteBatch);
            }
            else if(MenuState == ScreenState.MainMenu)
            {
                if (game != null)
                {
                    buttons[ContinueButtonIndex].Draw(spriteBatch);
                    buttons[ExitGameButtonIndex].Draw(spriteBatch);
                }
                else
                {
                    buttons[StartButtonIndex].Draw(spriteBatch);
                    buttons[ExitProgramButtonIndex].Draw(spriteBatch);
                }
            }
            else
            {
                // Something WENT WRONG!
            }
        }
    }
}