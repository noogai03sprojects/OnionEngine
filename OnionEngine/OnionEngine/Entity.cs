using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OnionEngine
{
    /// <summary>
    /// This class, or a derivative, will be basically every object in your game.
    /// </summary>
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

        public Color Color = Color.White;

        #region Animation stuff

        public bool Animated = false;
        public Rectangle[] Frames = null;
        Rectangle frameBounds = Rectangle.Empty;
        float frameTimer;
        List<Animation> animations = new List<Animation>();
        Animation currentAnim;
        int frame = 0;

        public bool Finished = true;
        #endregion

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

            Position += Velocity * OE.Delta;

            //AngularVelocity = Utils.ComputeVelocity(AngularVelocity, AngularAcceleration, AngularDrag, MaxAngularVelocity);
            
            Angle += AngularVelocity * OE.Delta;
            while (Angle > MathHelper.TwoPi)
                Angle -= MathHelper.TwoPi;
            while (Angle < 0)
                Angle += MathHelper.TwoPi;

            if (HasHitbox)
                Hitbox.Update();

            if (Animated)
                UpdateAnimation();

            //Console.WriteLine(Velocity.X);
        }

        private void UpdateAnimation()
        {
            if (!Finished)
            {
                frameTimer += OE.Delta;

                if (frameTimer > currentAnim.Delay)
                {
                    frameTimer -= currentAnim.Delay;
                    frame++;
                    if (frame >= currentAnim.Length)
                    {
                        if (currentAnim.Looped)
                            frame = 0;
                        else
                        {
                            Finished = true;
                            frame = 0;
                            return;
                        }
                    }
                }

                frameBounds = Frames[currentAnim.Frames[frame]];
            }
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
                if (Animated)
                {
                    Hitbox = new Hitbox(frameBounds.Width, frameBounds.Height, this);
                    Hitbox.Offset = -Origin;
                }
                else
                {
                    Hitbox = new Hitbox(Graphic.Width, Graphic.Height, this);
                    Hitbox.Offset = -Origin;
                }
                //if (Hitbox.Flipped)
                //    H
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

        /// <summary>
        /// Draws the sprite. This is normally called internally if the entity is in the Stage's RenderList.
        /// </summary>
        public virtual void Draw()
        {
            if (Animated)
                OE.SpriteBatch.Draw(Graphic, Position, frameBounds, Color, Angle, Origin, 1, 0, 0);
            else
                OE.SpriteBatch.Draw(Graphic, Position, null, Color, Angle, Origin, 1, 0, 0);
        }

        public void CentreOrigin()
        {
            if (Graphic != null)
            {
                if (Animated)
                    Origin = new Vector2(frameBounds.Width / 2, frameBounds.Height / 2);
                else
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

        public void LoadGraphic(string path, bool Animated = false, int width = 0, int height = 0)
        {
            Graphic = OE.LoadGraphic(path);

            if (Animated)
            {
                this.Animated = true;
                Rectangle frame = new Rectangle(0, 0, width, height);

                

                int widthInFrames = (int)(Graphic.Width / width);
                int heightInFrames = (int)(Graphic.Height / height);

                Frames = new Rectangle[widthInFrames * heightInFrames];
                Frames[0] = frame;
                frameBounds = frame;

                for (int y = 0; y < heightInFrames; y++)
                {
                    for (int x = 0; x < widthInFrames; x++)
                    {
                        frame.X = width * x;
                        frame.Y = height * y;

                        Frames[widthInFrames * y + x] = frame;
                    }
                }
            }
        }

        public void AddAnimation(string name, int[] frames, int fps, bool loop)
        {
            animations.Add(new Animation(name, frames, loop, fps));
        }

        public void Play(string name, bool force)
        {
            if (currentAnim.Name == name && !force)
                return;

            int i = 0;
            while (i < animations.Count)
            {
                if (animations[i].Name == name)
                {
                    currentAnim = animations[i];
                    Finished = false;
                    frame = 0;
                }

                i++;
            }
        }
    }
}
