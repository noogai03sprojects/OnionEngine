using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnionEngine;
using Microsoft.Xna.Framework;

namespace OnionTestGame
{
    class Enemy : BaseEnemy
    {
        
        public Enemy(Player player)
            :base (player)
        {
            
        }

        int HP = 20;

        public float InvulnTimer = 0;

        public override void Init(Stage stage)
        {
            MakeGraphic(20, 20, Color.White);
            Color = Color.Red;

            Position = new Vector2(OE.Random.Next(800), OE.Random.Next(480)); 

            while (Math.Abs((float)((Position - player.Position).Length())) < 200)
                Position = new Vector2(OE.Random.Next(800), OE.Random.Next(480));  
            
            MaxVelocity.X = 200;
            MaxVelocity.Y = 200;
            //Drag.X = 40;
            //Drag.Y = 40;

            
            CentreOrigin();
            MakeDefaultHitbox();
            Type = "enemy";

            base.Init(stage);
        }

        public override void Update()
        {
            Vector2 direction = player.Position - Position;
            direction.Normalize();

            Velocity += direction * 8;
            InvulnTimer -= OE.Delta;
            if (InvulnTimer <= 0)
                Color = Color.Red;

            if (Collide("sword") != null)
            {
                if (InvulnTimer <= 0)
                {
                    HP -= 5;
                    InvulnTimer = 0.2f;
                    Color = Color.White;
                }

                if (HP <= 0)
                {
                    Kill();
                    player.Points += 5;
                }

                Velocity = player.Velocity + new Vector2(
                    200 * (float)Math.Cos(MathHelper.PiOver2 * player.Direction),
                    200 * (float)Math.Sin(MathHelper.PiOver2 * player.Direction));
            }

            Angle += 2 * OE.Delta;
            
            base.Update();
        }

        public override void Kill()
        {
            (Stage as TestStage).MakeExplosion(Position,150);
            base.Kill();
        }
    }
}
