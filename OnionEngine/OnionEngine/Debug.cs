using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    class Debug
    {
        public bool Enabled = false;

        public bool DrawHitboxes = true;
        public Color HitboxColor = Color.Blue;

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
