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
    public class Emitter : OnionBasic
    {
        List<Particle> Particles;

        List<Texture2D> Images;
        List<Vector2> Origins;

        int capacity;

        public Vector2 Position;

        Action<Particle> UpdateAction = null;

        Entity Target = null;
        bool locked = false;
        private Vector2 offset;
        
        public Emitter(Vector2 Position, Action<Particle> UpdateMethod = null, int capacity = 1000)
        {
            this.capacity = capacity;
            Particles = new List<Particle>(capacity);
            Images = new List<Texture2D>();
            Origins = new List<Vector2>();
            this.Position = Position;
            UpdateAction = UpdateMethod;

            for (int i = 0; i < capacity; i++)
            {
                Particles.Add(new Particle());
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

        public static Vector2 RandomizeVelocity(float MinForce, float MaxForce, float MinAngle, float MaxAngle)
        {
            float angle = OE.RandFloat(MinAngle, MaxAngle);
            float force = OE.RandFloat(MinForce, MaxForce);
            Vector2 output = new Vector2(
                    force * (float)Math.Cos(angle),
                    force * (float)Math.Sin(angle)
                );
            return output;
        }
        public static Vector2 RandomizeVelocity(float MinForce, float MaxForce)
        {
            return Emitter.RandomizeVelocity(MinForce, MaxForce, 0, MathHelper.TwoPi);
        }
        public float RandomizeAngle()
        {
            return OE.RandFloat(0, MathHelper.TwoPi);
        }

        //public void AddParticles(int count, Vector2 velocity)
        //{

        //}

        public void AddParticle(Vector2 velocity, float age)
        {
            AddParticle(velocity, age, Color.White);
        }

        public void AddParticle(Vector2 velocity, float age, Color color)
        {
            int i = GetParticle();
            if (i == -1)
                return;
            Particles[i].Position = Position;
            Particles[i].Velocity = velocity;
            Particles[i].ImageIndex = OE.RandInt(Images.Count);
            Particles[i].Angle = RandomizeAngle();
            Particles[i].Age = age;
            Particles[i].Alpha = 1;
            Particles[i].Color = color;
            Particles[i].Alive = true;
        }

        public override void Update()
        {
            if (locked)
                Position = Target.Position + offset;

            for (int i = 0; i < capacity; i++)
            {
                if (Particles[i].Alive)
                {
                    Particles[i].Age -= OE.Delta;
                    if (Particles[i].Age <= 0)
                    {
                        Particles[i].Alive = false;
                        continue;
                    }

                    if (UpdateAction != null)
                        UpdateAction(Particles[i]);
                    
                    Particles[i].Position += Particles[i].Velocity * OE.Delta;
                    Particles[i].Angle += Particles[i].AngVel * OE.Delta;
                }
            }
            
        }

        public override void Draw()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (Particles[i].Alive)
                {
                    OE.SpriteBatch.Draw(Images[Particles[i].ImageIndex], Particles[i].Position, null, Particles[i].Color * Particles[i].Alpha, Particles[i].Angle, Origins[Particles[i].ImageIndex], 1, 0, 0);
                }
            }
        }

        public void LockTo(Entity e, Vector2 offset)
        {
            locked = true;
            Target = e;
            this.offset = offset;
        }

        public void Unlock()
        {
            locked = false;
            Target = null;
            offset = Vector2.Zero;
        }
    }
}
