// =============================================
//         Editor:     Daniel Abdulahad
//         Last edit:  2020-03-24
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

// https://github.com/A1rPun/MonoGame.InputManager

using Microsoft.Xna.Framework.Input;
using System;

namespace AWorldDestroyed.Models
{
    public static class InputManager
    {
        private static KeyboardState previousKeyboardState;
        private static KeyboardState currentKeyboardState;
        private static MouseState previousMouseState;
        private static MouseState currentMouseState;

        static InputManager()
        {
            Update();   
        }

        public static void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            previousMouseState = Mouse.GetState();
        }

        #region MouseEvents
        public static bool IsMousePressed(MouseInput input)
        {
            return IsMousePressed(currentMouseState, input);
        }

        public static bool IsMouseHeld(MouseInput input)
        {
            return IsMousePressed(currentMouseState, input) && IsMousePressed(previousMouseState, input);
        }

        public static bool MouseJustPressed(MouseInput input)
        {
            return IsMousePressed(currentMouseState, input) && !IsMousePressed(previousMouseState, input);
        }

        public static bool MouseJustReleased(MouseInput input)
        {
            return !IsMousePressed(currentMouseState, input) && IsMousePressed(previousMouseState, input);
        }
        #endregion

        #region KeyboadEvents
        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }

        public static bool KeyJustPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public static bool KeyJustReleased(Keys key)
        {
            return !currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
        #endregion

        public static bool AnythingPressed()
        {
            return AnyKeyPressed() || AnyMousePressed();
        }

        public static bool AnyKeyPressed()
        {
            return currentKeyboardState.GetPressedKeys().Length > 0;
        }

        public static bool AnyMousePressed()
        {
            foreach (MouseInput key in Enum.GetValues(typeof(MouseInput)))
            {
                if (IsMousePressed(currentMouseState, key))
                    return true;
            }

            return false;
        }

        private static bool IsMousePressed(MouseState state, MouseInput input)
        {
            switch (input)
            {
                case MouseInput.LeftButton:
                    return state.LeftButton == ButtonState.Pressed;
                case MouseInput.MiddleButton:
                    return state.MiddleButton == ButtonState.Pressed;
                case MouseInput.RightButton:
                    return state.RightButton == ButtonState.Pressed;
                case MouseInput.Button1:
                    return state.XButton1 == ButtonState.Pressed;
                case MouseInput.Button2:
                    return state.XButton2 == ButtonState.Pressed;
            }

            return false;
        }
    }

    public enum MouseInput
    {
        None,
        LeftButton,
        MiddleButton,
        RightButton,
        Button1,
        Button2
    }
}
