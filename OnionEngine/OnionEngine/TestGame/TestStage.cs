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
    class TestStage : Stage
    {
        public Player player;

        float timer = 1;

        public List<BaseEnemy> Enemies = new List<BaseEnemy>();

        Text text;
        Text score;

        Emitter emitter;

        public List<Explosion> Explosions = new List<Explosion>();

        public override void Init()
        {
            bgColor = Color.CornflowerBlue;

            player = new Player();            
            Add(player);

            //Enemies.Add(new Enemy(player));
            //Add(Enemies);

            OE.UserData.Add(this);
            OE.Debug.Enabled = true;

            text = new Text("Lives: " + player.Lives, new Vector2(10, 10));
            Add(text);
            score = new Text("Score: " + player.Points, new Vector2(200, 10));
            Add(score);

            emitter = new Emitter(new Vector2(200, 200), new Action<Particle>(TestStage.UpdateParticle));
            emitter.LoadImages(new Texture2D[] { Utils.MakeGraphic(4, 4, Color.White) });
            emitter.GenerateOrigins();
            //emitter.
            Add(emitter);

            
            emitter.LockTo(player, Vector2.Zero);
            OE.Camera.Follow(player);

            base.Init();
        }

        public override void Update()
        {
            if (OE.Input.Pressed(Keys.F1))
            {
                OE.Debug.Enabled = !OE.Debug.Enabled;
            }
            if (OE.Input.Pressed(Keys.A))
            {
                Enemies.Add(new Bouncer(player));
                Add(Enemies);
            }

            text.Value = "Lives: " + player.Lives;
            score.Value = "Score: " + player.Points;

            if (player.Invulnerable)
                text.Value = "Invulnerable";

            if (player.Lives <= 0)
            {
                text.Value = "GAME OVER.";
                player.Kill();
            }


            timer -= OE.Delta;
            if (timer <= 0)
            {
                //Enemies.Add(new Enemy(player));
                //Add(Enemies);

                timer = 2f;
            }

            Enemies.RemoveAll(x => !x.Alive);

            //for (int i = 0; i < 2; i++)
                //emitter.AddParticle(Emitter.RandomizeVelocity(50, 200), 1f, new Color(OE.RandInt(255), OE.RandInt(255), OE.RandInt(255)));

            base.Update();
        }

        public static void UpdateParticle(Particle particle)
        {
            particle.Velocity *= 0.98f;
            particle.Alpha -= OE.Delta;
        }

        public void MakeParticleExplosion(Vector2 position, float size, int number = 50)
        {
            emitter.Position = position;
            for (int i = 0; i < number; i++)
            {
                int value = OE.RandInt(255);
                emitter.AddParticle(Emitter.RandomizeVelocity(10, size), 1f, new Color(value, value, value));
            }
        }

        public void MakeExplosion(Vector2 position, float radius)
        {
            Explosions.Add(new Explosion(position, radius, 1));
            Add(Explosions);
            //Add(new Explosion(position, radius, 2), false, true);
        }

        public override void DrawDebug()
        {
            OE.PrimBatch.Begin(PrimitiveType.LineList);

            for (int i = 0; i < Explosions.Count; i++)
            {
                OE.PrimBatch.DrawCircle(Explosions[i].aoe.Position, Explosions[i].aoe.Radius, Color.Green);
            }

            OE.PrimBatch.End();
            base.DrawDebug();
        }
        
    }
}
