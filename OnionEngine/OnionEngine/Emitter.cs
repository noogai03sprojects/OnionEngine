using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OnionEngine
{
    /// <summary>
    /// A particle emitter. Hopefully it's efficient :D
    /// </summary>
    public class Emitter
    {
        List<Particle> Particles;

        List<Texture2D> Images;
        List<Vector2> Origins;

        int capacity;

        public Vector2 Position;

        Action<Particle> UpdateAction = null;
        
        public Emitter(Vector2 Position, Action<Particle> UpdateMethod, int capacity = 1000)
        {
            this.capacity = capacity;
            Particles = new List<Particle>(capacity);
            Images = new List<Texture2D>();

            for (int i = 0; i < capacity; i++)
            {
                Particles[i] = new Particle();
            }
        }

        public void LoadImages(params string[] paths)
        {
            foreach (string str in paths)
            {
                Images.Add(OE.LoadGraphic(str));
            }
            GenerateOrigins();
        }
        public void LoadImages(IEnumerable<Texture2D> images)
        {
            Images.AddRange(images);
            GenerateOrigins();
        }

        public void GenerateOrigins()
        {
            for (int i = 0; i < Images.Count; i++)
            {
                Origins.Add(new Vector2(Images[i].Width / 2, Images[i].Height / 2));
            }
        }

        public void Reset()
        {
            for (int i = 0; i < capacity; i++)
            {
                Particles[i].Alive = false;
            }
        }

        int GetParticle()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (!Particles[i].Alive)
                {
                    return i;
                }
            }
            return -1;
        }
        void ResetParticle(int index)
        {
            Particles[index].Alive = false;
        }

        public Vector2 RandomizeVelocity(float MinForce, float MaxForce, float MinAngle, float MaxAngle)
        {
            float angle = OE.RandFloat(MinAngle, MaxAngle);
            float force = OE.RandFloat(MinForce, MaxForce);
            Vector2 output = new Vector2(
                    force * (float)Math.Cos(angle),
                    force * (float)Math.Sin(angle)
                );
            return output;
        }
        public float RandomizeAngle()
        {
            return OE.RandFloat(0, MathHelper.TwoPi);
        }

        //public void AddParticles(int count, Vector2 velocity)
        //{

        //}
        public void AddParticle(Vector2 velocity)
        {
            int i = GetParticle();
            Particles[i].Position = Position;
            Particles[i].Velocity = velocity;
            Particles[i].ImageIndex = OE.RandInt(Images.Count);
            Particles[i].Angle = RandomizeAngle();
        }

        public void UpdateParticles()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (Particles[i].Alive)
                {
                    if (UpdateAction != null)
                        UpdateAction(Particles[i]);

                    Particles[i].Position += Particles[i].Velocity * OE.Delta;
                    Particles[i].Angle += Particles[i].AngVel * OE.Delta;
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (Particles[i].Alive)
                {
                    OE.SpriteBatch.Draw(Images[Particles[i].ImageIndex], Particles[i].Position, null, Color.White, Particles[i].Angle, Origins[i], 1, 0, 0);
                }
            }
        }
    }
}
