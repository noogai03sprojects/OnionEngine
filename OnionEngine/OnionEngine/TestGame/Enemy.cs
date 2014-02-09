using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnionEngine;
using Microsoft.Xna.Framework;

namespace OnionTestGame
{
    class Enemy : Entity
    {
        Player target;
        public Enemy(Player player)
        {
            target = player;
        }

        public override void Init(Stage stage)
        {
            MakeGraphic(20, 20, Color.Red);

            Position = new Vector2(OE.Random.Next(800), OE.Random.Next(480)); 

            while (Math.Abs((float)((Position - target.Position).Length())) < 200)
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
            Vector2 direction = target.Position - Position;
            direction.Normalize();

            Position += direction * 2;

            if (Collide("sword") != null)
            {
                Kill();
            }

            Angle += 2 * OE.Delta;

            base.Update();
        }
    }
}
