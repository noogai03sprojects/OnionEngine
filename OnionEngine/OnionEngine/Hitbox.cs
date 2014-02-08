using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    class Hitbox
    {
        public Entity Owner;
        public Vector2 Position;
        public Vector2 Size;
        public Vector2 Offset = Vector2.Zero;

        //public static readonly Hitbox None = new Hitbox(0, 0, null);

        public Hitbox(int Width, int Height, Entity owner)
        {
            Position = owner.Position;
            Size = new Vector2(Width, Height);
            Owner = owner;
        }

        public void Update()
        {
            Position = Owner.Position + Offset;
        }

        public void Flip()
        {
            float _y = Size.Y;
            Size.Y = Size.X;
            Size.X = _y;
        }

        static public explicit operator RectangleF(Hitbox h)
        {
            return new RectangleF(h.X, h.Y, h.Width, h.Height);
        }

        public float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }
        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }
        public float Width
        {
            get { return Size.X; }
            set { Size.X = value; }
        }
        public float Height
        {
            get { return Size.Y; }
            set { Size.Y = value; }
        }
    }
}
