using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    /// <summary>
    /// Used as an object pool by Emitter.
    /// </summary>
    public class Particle
    {
        public static Particle Blank = new Particle();
        public bool Alive;
        public int Age;

        public Vector2 Position;
        public Vector2 Velocity;

        public int ImageIndex;

        public float Angle;
        public float AngVel;
        //public Vector2 Acceleration = Vector2.Zero;

        //public Action<Particle> UpdateMethod;

        public Particle()
        {
            //UpdateMethod = update;            
            Alive = false;
            Age = 0;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            ImageIndex = 0;
            Angle = 0;
            AngVel = 0;
        }

        public Particle(int age, Vector2 position, Vector2 velocity)
        {
            //UpdateMethod = update;
            if (age > 0)
                Alive = true;
            else
                Alive = false;
            Age = age;

            Position = position;
            Velocity = velocity;
        }

        public void Reset()
        {
            Alive = false;
            Age = 0;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
        }

        
    }
}
