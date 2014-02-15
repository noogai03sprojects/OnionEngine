using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    /// <summary>
    /// Class for drawing text through OnionEngine using the default font.
    /// </summary>
    public class Text : Entity
    {
        public string Value;

        //public Vector2 Position;

        /// <summary>
        /// The integer scale factor; this font looks shit at non-integer sizes
        /// </summary>
        public int ScaleFactor = 1;

        public Text(string text, Vector2 position)
        {
            Position = position;
            Value = text;
            Color = Color.White;
        }

        public new void CentreOrigin()
        {
            Origin = (OE.Debug.Font.MeasureString(Value) * ScaleFactor) / 2;
        }

        public override void Draw()
        {
            OE.SpriteBatch.DrawString(OE.Debug.Font, Value, Position, Color, 0, Origin, ScaleFactor, 0, 0);
        }
    }
}
