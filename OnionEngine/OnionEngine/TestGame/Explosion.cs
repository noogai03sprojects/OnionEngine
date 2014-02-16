using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using OnionEngine;

namespace OnionTestGame
{
    class Explosion : Entity
    {
        public CircularHitbox aoe;

        float timer;
        public Explosion(Vector2 position, float radius, float time)
        {
            aoe = new CircularHitbox(position, radius);
            Position = position;
            timer = time;
        }

        public override void Init(Stage stage)
        {
            Type = "explosion";
            base.Init(stage);
        }

        public override void Update()
        {
            timer -= OE.Elapsed;
            if (timer <= 0)
                Kill();

            if (Stage.QueryCircle(aoe, "player").Count > 0)
            {
                (Stage as TestStage).player.Color = Color.Yellow;
                (Stage as TestStage).player.LoseLife();

                Kill();
            }
            base.Update();
        }
        public override void Kill()
        {
            (Stage as TestStage).Explosions.Remove(this);
            base.Kill();
        }
    }
}
