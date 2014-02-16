using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    public static class Utils
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
        public static bool Intersects(Hitbox b, CircularHitbox c)
        {         
            //Vector2 closest = new Vector2(MathHelper.Clamp(c.Position.X, b.Centre.X - b.Size.X / 2, b.Centre.X + b.Size.X / 2),
            //    MathHelper.Clamp(c.Position.Y, b.Centre.Y - b.Size.Y / 2, b.Centre.Y + b.Size.Y / 2));            

            //Vector2 distance = c.Position -closest;

            //return distance.LengthSquared() < (c.Radius * c.Radius);
            float cX = c.Position.X;
            float cY = c.Position.X;
            float rX = b.Centre.X;
            float rY = b.Centre.Y;
            float halfWidth = b.Width / 2;
            float halfHeight = b.Height / 2;


            float closestX = MathHelper.Clamp(cX, rX - halfWidth, rX + halfWidth);
            float closestY = MathHelper.Clamp(cY, rY - halfHeight, rY + halfHeight);

            float distX = cX - closestX;
            float distY = cY - closestY;

            float distSq = (distX * distX) + (distY * distY);
            return distSq < (c.Radius * c.Radius);
        }
    }
}
