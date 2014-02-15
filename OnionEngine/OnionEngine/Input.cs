using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    /// <summary>
    /// Input managing class. Don't instantiate this; use OE.Input instead.
    /// </summary>
    public class Input
    {
        KeyboardState oldKeyboardState, newKeyboardState;
        GamePadState oldGamePadState, newGamePadState;

        Dictionary<string, Keys[]> Shortcuts = new Dictionary<string,Keys[]>();

        public Input()
        {
            
        }

        internal void Update()
        {
            oldKeyboardState = newKeyboardState;
            newKeyboardState = Keyboard.GetState();

            oldGamePadState = newGamePadState;
            newGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public void Define(string name, params Keys[] keys)
        {
            Shortcuts.Add(name, keys);
        }


        /// <summary>
        /// Checks if a key is currently held down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public bool Check(Keys key)
        {
            return newKeyboardState.IsKeyDown(key);
        }
        
        /// <summary>
        /// Checks if any keys in a predefined sequence are held down.
        /// </summary>
        /// <param name="key">The name of the sequence.</param>
        /// <returns></returns>
        public bool Check(string key)
        {
            for (int i = 0; i < Shortcuts[key].Length; i++)
            {
                if (newKeyboardState.IsKeyDown(Shortcuts[key][i]))
                    return true;
            }
            return false;
        }

        public bool Pressed(Keys key)
        {
            return oldKeyboardState.IsKeyUp(key) && newKeyboardState.IsKeyDown(key);
        }
        public bool Pressed(string key)
        {
            for (int i = 0; i < Shortcuts[key].Length; i++)
            {
                if (oldKeyboardState.IsKeyUp(Shortcuts[key][i]) && newKeyboardState.IsKeyDown(Shortcuts[key][i]))
                    return true;
            }
            return false;
        }

        public bool Released(Keys key)
        {
            return oldKeyboardState.IsKeyDown(key) && newKeyboardState.IsKeyUp(key);
        }
        public bool Released(string key)
        {
            for (int i = 0; i < Shortcuts[key].Length; i++)
            {
                if (oldKeyboardState.IsKeyDown(Shortcuts[key][i]) && newKeyboardState.IsKeyUp(Shortcuts[key][i]))
                    return true;
            }
            return false;
        }

        public Vector2 LeftThumbStick
        {
            get
            {
                return newGamePadState.ThumbSticks.Left;
            }
        }
        public Vector2 RightThumbStick
        {
            get
            {
                return newGamePadState.ThumbSticks.Right;
            }
        }

        public bool Check(Buttons button)
        {
            return newGamePadState.IsButtonDown(button);            
        }
        public bool Pressed(Buttons button)
        {
            return newGamePadState.IsButtonDown(button) && oldGamePadState.IsButtonUp(button);
        }
    }
}
