using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OnionEngine
{
    class Entity
    {
        protected Stage Stage;

        public Texture2D Graphic = null;
        public Vector2 Origin = Vector2.Zero;

        public Vector2 Position = Vector2.Zero;
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;
        public Vector2 MaxVelocity = Vector2.Zero;
        public Vector2 Drag = Vector2.Zero;

        public float Angle = 0;
        public float AngularVelocity = 0;
        public float AngularAcceleration = 0;
        public float MaxAngularVelocity = 0;
        public float AngularDrag = 0;

        public string Type = "none";
        public Hitbox Hitbox;
        public bool HasHitbox = false;

        public bool Alive = true;

        public virtual void Init(Stage stage)
        {
            Stage = stage;            
        }

        public virtual void Update()
        {
            float velocityDelta = 0;

            velocityDelta = Utils.ComputeVelocity(Velocity.X, Acceleration.X, Drag.X, MaxVelocity.X);
            Velocity.X = velocityDelta;
            velocityDelta = Utils.ComputeVelocity(Velocity.Y, Acceleration.Y, Drag.Y, MaxVelocity.Y);
            Velocity.Y = velocityDelta;
            
            //Velocity += Acceleration;

            //Velocity.X = MathHelper.Clamp(Velocity.X, -MaxVelocity.X, MaxVelocity.X);

            Position += Velocity * OE.delta;

            //AngularVelocity = Utils.ComputeVelocity(AngularVelocity, AngularAcceleration, AngularDrag, MaxAngularVelocity);
            
            Angle += AngularVelocity * OE.delta;
            while (Angle > MathHelper.TwoPi)
                Angle -= MathHelper.TwoPi;
            while (Angle < 0)
                Angle += MathHelper.TwoPi;

            Hitbox.Update();

            

            //Console.WriteLine(Velocity.X);
        }

        public void Flip()
        {
            Hitbox.Flip();
            Angle += MathHelper.PiOver2;
        }

        public void MakeGraphic(int w, int h, Color col)
        {
            Graphic = Utils.MakeGraphic(w, h, col);
        }

        public void MakeDefaultHitbox()
        {
            if (Graphic != null)
            {
                Hitbox = new Hitbox(Graphic.Width, Graphic.Height, this);
                Hitbox.Offset = -Origin;
            }
            else
                throw new InvalidOperationException("Graphic not assigned. Cannot assign default hitbox.");
            HasHitbox = true;
            
        }
        public void SetHitbox(int width, int height)
        {
            Hitbox = new Hitbox(width, height, this);
            HasHitbox = true;
        }
        public void SetHitbox(int width, int height, Vector2 offset)
        {
            SetHitbox(width, height);
            Hitbox.Offset = offset;
        }

        public void Kill()
        {
            Alive = false;
            Stage.Remove(this);
        }

        public void Draw()
        {
            OE.SpriteBatch.Draw(Graphic, Position, null, Color.White, Angle, Origin, 1, 0, 0);
        }

        public void CentreOrigin()
        {
            if (Graphic != null)
            {
                Origin = new Vector2(Graphic.Width / 2, Graphic.Height / 2);
            }
        }

        public Entity Collide(string type)
        {
            if (HasHitbox)
            {
                var entities = OE.Stage.GetEntities(type);

                if (entities != null)
                {
                    for (int i = 0; i < entities.Count; i++)
                    {               
                        
                        if (Utils.Intersects(Hitbox, entities[i].Hitbox))
                            return entities[i];
                    }
                }
                return null;
            }
            else
            {
                throw new InvalidOperationException("No hitbox!");
            }
        }
    }
}
