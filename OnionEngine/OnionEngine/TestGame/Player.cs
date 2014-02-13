using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnionEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace OnionTestGame
{
    class Player : Entity
    {
        //float SwordSwipeTimer = 0;
        Entity SwordSwipe;
        Vector2 SwordOffset = Vector2.Zero;

        public int Direction = 0;

        public int Lives = 10;
        public bool Invulnerable = false;
        
        public override void Init(Stage stage)
        {
            MakeGraphic(20, 20, Color.Green);
            
            Position = new Vector2(400, 200);
            MaxVelocity.X = 200;
            MaxVelocity.Y = 200;            
            Drag.X = 20;
            Drag.Y = 20;

            //LoadGraphic("SpaceManRun", true, 50, 50);

            CentreOrigin();
            MakeDefaultHitbox();
            Type = "player";

            SwordSwipe = new Entity();
            SwordSwipe.MakeGraphic(30, 30, Color.Blue);
            SwordSwipe.LoadGraphic("sordanim", true, 30, 50);
            SwordSwipe.Type = "sword";

            SwordSwipe.AddAnimation("swing", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 40, false);

            SwordSwipe.CentreOrigin();
            SwordSwipe.MakeDefaultHitbox();

            //AddAnimation("run", new int[]{0, 1, 2, 3, 4, 5, 6, 7}, 7, true);
            //Play("run", true);

            base.Init(stage);
        }

        public override void Update()
        {
            if (OE.Input.Pressed(Keys.Z))
            {
                Attack();
                //SwordSwipe.Flip();
            }
            if (OE.Input.Pressed(Keys.F2))
            {
                Invulnerable = !Invulnerable;
                //SwordSwipe.Flip();
            }
            
            SwordSwipe.Hitbox.CentreOrigin();

            Acceleration.X = 0;
            Acceleration.Y = 0;
            if (OE.Input.Check(Keys.Left))
            {
                Direction = 2;
                Acceleration.X = -20;                
            }
            if (OE.Input.Check(Keys.Right))
            {
                Direction = 0;
                Acceleration.X = 20;
            }
            if (OE.Input.Check(Keys.Up))
            {
                Direction = 3;
                Acceleration.Y = -20;
            }
            if (OE.Input.Check(Keys.Down))
            {
                Direction = 1;
                Acceleration.Y = 20;
            }


            SwordSwipe.Hitbox.CentreOrigin();

            
            //if you're touching an ememy, kill the player.
            if (Collide("enemy") != null && !Invulnerable)
            {
                Console.WriteLine(Lives);
                Position = new Vector2(400, 200);
                Lives--;
                int count = ((TestStage)OE.UserData[0]).Enemies.Count;

                float step = MathHelper.TwoPi / count;
                for (int i = 0; i < count; i++)
                {
                    Vector2 pos = new Vector2( 200 * (float)Math.Cos(i * step),
                                              200 * (float)Math.Sin(i * step));
                    ((TestStage)OE.UserData[0]).Enemies[i].Position = Position + pos;
                    ((TestStage)OE.UserData[0]).Enemies[i].Velocity = Vector2.Zero;
                    ((TestStage)OE.UserData[0]).Enemies[i].InvulnTimer = 0;
                }
                    //200 * (float)Math.Cos(step
            }
            SwordSwipe.Hitbox.CentreOrigin();
            //SwordSwipeTimer -= OE.Delta;
            if (SwordSwipe.Finished)
            {
                Stage.Remove(SwordSwipe);
            }

            //Angle = MathHelper.PiOver4;

            SwordSwipe.Hitbox.CentreOrigin();

            
            SwordSwipe.Hitbox.CentreOrigin();
            //if (OE.Input.Pressed(Buttons.X))
            //{
            //    Attack();
            //}

            //if (OE.Input.LeftThumbStick != Vector2.Zero)
            //{
            //    Position.X += 20 * OE.Input.LeftThumbStick.X;
            //    Position.Y += -20 * OE.Input.LeftThumbStick.Y;
            //}

            if (!SwordSwipe.Finished)
            {
                SwordSwipe.Position = Position + SwordOffset;
            }

            base.Update();
            SwordSwipe.Hitbox.CentreOrigin();
        }

        private void Attack()
        {
            SwordSwipe.Hitbox.CentreOrigin();


            //Console.WriteLine(Direction);

            SwordOffset = new Vector2(
                    20 * (float)Math.Cos(MathHelper.PiOver2 * Direction),
                    20 * (float)Math.Sin(MathHelper.PiOver2 * Direction));


            SwordSwipe.Hitbox.CentreOrigin();
            
            //SwordSwipe.MakeDefaultHitbox();

            //SwordSwipeTimer = 1f;
            SwordSwipe.Angle = MathHelper.PiOver2 * Direction;
            
            SwordSwipe.Play("swing", true);

            //olololololololol

            if ((Direction + 1) % 2 == 0)
            {
                SwordSwipe.Hitbox.Flipped = true;
                SwordSwipe.Hitbox.CentreOrigin();
                //SwordSwipe.Hitbox.Offset -= SwordOffset;
            }
            else
            {
                SwordSwipe.Hitbox.Flipped = false;
                SwordSwipe.Hitbox.CentreOrigin();

                //SwordSwipe.Hitbox.Offset -= SwordOffset;
            }

            Stage.Add(SwordSwipe);
        }
    }
}
