using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    /// <summary>
    /// Represents a circular hitbox for intersection with round objects, eg an 
    /// </summary>
    public class CircularHitbox
    {
        public float Radius;

        /// <summary>
        /// The co-ordinates of the centre of the circle
        /// </summary>
        public Vector2 Position;

        public CircularHitbox(Vector2 Position, float radius)
        {
            this.Position = Position;
            this.Radius = radius;
        }
    }
}
