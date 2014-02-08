using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    static class Utils
    {
        public static Texture2D Pixel;

        static Utils()
        {
            Pixel = new Texture2D(OE.Device, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            Pixel.SetData<Color>(data);
        }

        public static Texture2D MakeGraphic(int Width, int Height, Color color)
        {
            Texture2D output = new Texture2D(OE.Device, Width, Height);
            Color[] data = new Color[Width * Height];
            for (int i = 0; i < Width * Height; i++)
            {
                data[i] = color;
            }
            output.SetData<Color>(data);          

            return output;
        }

        public static float ComputeVelocity(float vel, float accel=0, float Drag = 0, float max = 10000)
        {
            if (accel != 0)
            {
                vel += accel;
            }
            else if (Drag != 0)
            {
                float drag = Drag;
                if (vel - drag > 0)
                    vel -= drag;
                else if (vel + drag < 0)
                    vel += drag;
                else
                    vel = 0;
            }
            if (vel != 0)
            {
                if (vel > max)
                    vel = max;
                else if (vel < -max)
                    vel = -max;
            }
            return vel;
        }

        public static bool Intersects(Hitbox _a, Hitbox _b)
        {
            var a = (RectangleF)_a;
            var b = (RectangleF)_b;

            if (a.Left > b.Right)
                return false;
            if (a.Right < b.Left)
                return false;
            if (a.Top > b.Bottom)
                return false;
            if (a.Bottom < b.Top)
                return false;

            return true;            
        }
    }
}
