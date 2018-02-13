using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.src.input
{
    class InputHandler
    {
        PlayerIndex playerIndex;
        private GamePadState previousGamePadState;

        static KeyboardState previousKeyboardState;
        static MouseState previousMouseState;

        public InputHandler(PlayerIndex index)
        {
            playerIndex = index;
            previousGamePadState = GamePad.GetState(index);
        }

        static InputHandler()
        {
            previousKeyboardState = Keyboard.GetState();
        }

        #region Keyboard

        public static void UpdateKeyboardState()
        {
            previousKeyboardState = Keyboard.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                return true;

            return false;
        }

        public static bool IsKeyReleased(Keys key)
        {
            if (Keyboard.GetState().IsKeyUp(key) && previousKeyboardState.IsKeyDown(key))
                return true;

            return false;
        }

        public static bool IsKeyHeld(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key) && previousKeyboardState.IsKeyDown(key))
                return true;

            return false;
        }

        #endregion

        #region Mouse

        public static void Update()
        {
            previousMouseState = Mouse.GetState();
        }

        public static Point getMousePosition()
        {
            return Mouse.GetState().Position;
        }

        public static bool MouseIntersects(Rectangle objectBounds)
        {
            Point mousePoint = Mouse.GetState().Position;

            if (objectBounds == null || mousePoint == null)
                return false;

            return
                ((mousePoint.X <= (objectBounds.X + objectBounds.Width)) &&
                 (mousePoint.X >= objectBounds.X) &&
                 (mousePoint.Y <= (objectBounds.Y + objectBounds.Height)) &&
                 (mousePoint.Y >= objectBounds.Y));
        }

        public static int getScrollWheelValue()
        {
            return Mouse.GetState().ScrollWheelValue;
        }

        public static void setMousePoition(int X, int Y)
        {
            Mouse.SetPosition(X, Y);
        }

        public static bool IsMouseLeftButtonClicked()
        {
            if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                return true;

            return false;
        }
        public static bool IsMouseLeftButtonReleased()
        {
            if (previousMouseState.LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
                return true;

            return false;
        }

        public static bool IsMouseRightButtonClicked()
        {
            if (previousMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                return true;

            return false;
        }
        public static bool IsMouseRightButtonReleased()
        {
            if (previousMouseState.RightButton == ButtonState.Pressed && Mouse.GetState().RightButton == ButtonState.Released)
                return true;

            return false;
        }

        public static bool IsMouseMiddletButtonClicked()
        {
            if (previousMouseState.MiddleButton == ButtonState.Released && Mouse.GetState().MiddleButton == ButtonState.Pressed)
                return true;

            return false;
        }
        public static bool IsMouseMiddleButtonReleased()
        {
            if (previousMouseState.MiddleButton == ButtonState.Pressed && Mouse.GetState().MiddleButton == ButtonState.Released)
                return true;

            return false;
        }

        public static bool IsMouseXButton1ButtonClicked()
        {
            if (previousMouseState.XButton1 == ButtonState.Released && Mouse.GetState().XButton1 == ButtonState.Pressed)
                return true;

            return false;
        }
        public static bool IsMouseXButton1ButtonReleased()
        {
            if (previousMouseState.XButton1 == ButtonState.Pressed && Mouse.GetState().XButton1 == ButtonState.Released)
                return true;

            return false;
        }

        public static bool IsMouseXButton2ButtonClicked()
        {
            if (previousMouseState.XButton2 == ButtonState.Released && Mouse.GetState().XButton2 == ButtonState.Pressed)
                return true;

            return false;
        }
        public static bool IsMouseXButton2ButtonReleased()
        {
            if (previousMouseState.XButton2 == ButtonState.Pressed && Mouse.GetState().XButton2 == ButtonState.Released)
                return true;

            return false;
        }
        #endregion

        #region Xbox Controller

        public void UpdateGamePadState()
        {
            previousGamePadState = GamePad.GetState(playerIndex);
        }

        public double XAxisLeftThumbStick()
        {
            return GamePad.GetState(playerIndex).ThumbSticks.Left.X;
        }

        public double YAxisLeftThumbStick()
        {
            return GamePad.GetState(playerIndex).ThumbSticks.Left.Y;
        }

        public bool IsButtonSingleButtonPress(Buttons button, int Controller)
        {
            if (GamePad.GetState(Controller).IsButtonDown(button) && previousGamePadState.IsButtonUp(button))
                return true;


            return false;
        }

        public bool IsButtonSingleButtonReleased(Buttons button, int Controller)
        {
            if (GamePad.GetState(Controller).IsButtonUp(button) && previousGamePadState.IsButtonDown(button))
                return true;

            return false;
        }

        #endregion

        public string DEBUG_KeyboardState()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var k in typeof(Keys).GetEnumValues())
            {
                string line = k.ToString();
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public string DEBUG_GAMEPADSTATE()
        {
            StringBuilder sb = new StringBuilder();

            if (GamePad.GetState(1).IsConnected)
            {
                List<Buttons> buttons = ButtonList();

                sb.Append("Controller 1:");
                sb.AppendLine(" ");

                foreach (Buttons b in buttons)
                {
                    sb.AppendFormat("Button Pressed: {0}", IsButtonSingleButtonPress(b, 1));
                    sb.AppendLine(" ");
                }

                sb.AppendFormat("Left Axis: {0}", GamePad.GetState(1).ThumbSticks.Left);
                sb.AppendFormat("Right Axis: {0}", GamePad.GetState(1).ThumbSticks.Right);
            }
            else
                sb.AppendLine("Controller 1 NOT Connected");

            if (GamePad.GetState(2).IsConnected)
            {
                List<Buttons> buttons = ButtonList();

                sb.Append("Controller 2:");
                sb.AppendLine(" ");

                foreach (Buttons b in buttons)
                {
                    sb.AppendFormat("Button Pressed: {0}", IsButtonSingleButtonPress(b, 2));
                    sb.AppendLine(" ");
                }

                sb.AppendFormat("Left Axis: {0}", GamePad.GetState(2).ThumbSticks.Left);
                sb.AppendFormat("Right Axis: {0}", GamePad.GetState(2).ThumbSticks.Right);
            }
            else
                sb.AppendLine("Controller 2 NOT Connected");

            return sb.ToString();
        }

        private List<Buttons> ButtonList()
        {
            List<Buttons> buttonList = new List<Buttons>();
            buttonList.Add(Buttons.A);
            buttonList.Add(Buttons.B);
            buttonList.Add(Buttons.X);
            buttonList.Add(Buttons.Y);

            buttonList.Add(Buttons.LeftShoulder);
            buttonList.Add(Buttons.RightShoulder);

            buttonList.Add(Buttons.LeftTrigger);
            buttonList.Add(Buttons.RightTrigger);

            buttonList.Add(Buttons.Start);
            buttonList.Add(Buttons.Back);

            buttonList.Add(Buttons.DPadUp);
            buttonList.Add(Buttons.DPadDown);
            buttonList.Add(Buttons.DPadLeft);
            buttonList.Add(Buttons.DPadRight);

            buttonList.Add(Buttons.LeftThumbstickDown);
            buttonList.Add(Buttons.RightThumbstickDown);

            return buttonList;
        }

        public void ReleaseAllButton()
        {/*
            previousKeyboardState = new KeyboardState();

            PreviousGamePadState[0] = new GamePadState();
            PreviousGamePadState[1] = new GamePadState();*/
        }
    }
}