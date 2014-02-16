using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    public class Camera
    {
        Matrix Transform;

        public Vector2 Position = Vector2.Zero;
        public float Angle = 0;
        float zoom = 1;

        bool following = false;
        Entity target = null;

        public float Zoom { 
            get { return zoom; } 
            set { 
                zoom = value; 
                if (zoom < 0.1f) zoom = 0.1f; 
            } 
        }

        public void Update()
        {
            while (Angle > MathHelper.TwoPi)
            {
                Angle -= MathHelper.TwoPi;
            }
            while (Angle < 0)
            {
                Angle += MathHelper.TwoPi;
            }

            //Transform = Matrix.CreateTranslation(Position.X, Position.Y, 0);
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public void Follow(Entity e)
        {
            target = e;
            following = true;
        }

        public void Free()
        {
            following = false;
        }

        public Matrix GetTransform(float parallax = 1)
        {
            while (Angle > MathHelper.TwoPi)
            {
                Angle -= MathHelper.TwoPi;
            }
            while (Angle < 0)
            {
                Angle += MathHelper.TwoPi;
            }

            if (following)
                Position = target.Position;

            Transform = Matrix.CreateTranslation(-Position.X * parallax, -Position.Y * parallax, 0) *
                Matrix.CreateRotationZ(Angle) *
                Matrix.CreateScale(Zoom, Zoom, 0) *
                Matrix.CreateTranslation(OE.ScreenWidth * 0.5f, OE.ScreenHeight * 0.5f, 0);
               
            return Transform;
        }
    }
}
