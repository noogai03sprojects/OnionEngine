using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OnionEngine
{
    /// <summary>
    /// Does various debuggy things, such as drawing hitboxes for entities.
    /// </summary>
    class Debug
    {
        public bool Enabled = false;

        public bool DrawHitboxes = true;
        public Color HitboxColor = Color.Blue;

        public SpriteFont Font;

        public void Draw()
        {
            if (Enabled)
            {
                OE.Stage.DrawDebug();
                //Console.WriteLine("debug");
            }
        }
    }
}
