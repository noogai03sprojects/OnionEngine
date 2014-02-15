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
    public class Debug
    {
        public bool Enabled = false;

        public bool DrawHitboxes = true;
        public Color HitboxColor = Color.Blue;

        public SpriteFont Font;
        public Text FPS;

        public Debug()
        {
            FPS = new Text("FPS: ", new Vector2(OE.ScreenWidth - 200, 10));            
        }

        public void Draw()
        {
            FPS.Alive = false;
            if (Enabled)
            {
                FPS.Alive = true;
                FPS.Value = "FPS: " + (int)(1 / OE.Delta);
                OE.Stage.DrawDebug();
                //Console.WriteLine("debug");
                
            }
        }
    }
}
