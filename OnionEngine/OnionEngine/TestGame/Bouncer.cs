using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnionEngine;
using Microsoft.Xna.Framework;

namespace OnionTestGame
{
    class Bouncer : BaseEnemy
    {
        int HP = 50;

        Entity AoEBox;

        bool Charging;


        public Bouncer(Player pl)
            :base(pl)
        {
            
        }

        public override void Init(Stage stage)
        {
            base.Init(stage);

            MakeGraphic(40, 40, Color.White);
            Color = Color.Blue;

            Position = Vector2.Zero;

            CentreOrigin();
            MakeDefaultHitbox();

            AoEBox = new Entity();
            AoEBox.SetHitbox(80, 80);
            AoEBox.Hitbox.CentreOrigin();
            Stage.Add(AoEBox, false, true);

            Type = "enemy";
            
            
        }

        public override void Update()
        {
            Vector2 direction = player.Position - Position;
            direction.Normalize();

            if (!Charging)
            Position += direction * 2;

            InvulnTimer -= OE.Delta;

            if (InvulnTimer <= 0)
                Color = Color.Blue;

            if (Collide("sword") != null)
            {              

                if (InvulnTimer <= 0)
                {
                    HP -= 5;
                    InvulnTimer = 1f;
                    Color = Color.White;
                }
            }

            if (AoEBox.Collide("player") != null)
            {
                Charging = true;
            }

            if (HP <= 0)
            {

                player.Points += 20;
                Kill();
            }

            AoEBox.Position = Position;

            base.Update();
        }
    }
}
