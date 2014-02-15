﻿using System;
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
        Player player;

        float timer = 1;

        public List<Enemy> Enemies = new List<Enemy>();

        Text text;

        Emitter emitter;

        public override void Init()
        {
            bgColor = Color.CornflowerBlue;

            player = new Player();            
            Add(player);

            Enemies.Add(new Enemy(player));
            Add(Enemies);

            OE.UserData.Add(this);
            OE.Debug.Enabled = true;

            text = new Text("Lives: " + player.Lives, new Vector2(10, 10));
            Add(text);

            emitter = new Emitter(new Vector2(200, 200), new Action<Particle>(TestStage.UpdateParticle));
            emitter.LoadImages(new Texture2D[] { Utils.MakeGraphic(4, 4, Color.White) });
            emitter.GenerateOrigins();
            //emitter.
            Add(emitter);

            
            emitter.LockTo(player, Vector2.Zero);

            base.Init();
        }

        public override void Update()
        {
            if (OE.Input.Pressed(Keys.F1))
            {
                OE.Debug.Enabled = !OE.Debug.Enabled;
            }
            if (OE.Input.Pressed(Keys.D1))
            {
                Enemies.Add(new Enemy(player));
                Add(Enemies);
            }

            text.Value = "Lives: " + player.Lives;
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
                Enemies.Add(new Enemy(player));
                Add(Enemies);

                timer = 2f;
            }

            Enemies.RemoveAll(x => !x.Alive);

            for (int i = 0; i < 2; i++)
                emitter.AddParticle(Emitter.RandomizeVelocity(50, 200), 1f, new Color(OE.RandInt(255), OE.RandInt(255), OE.RandInt(255)));

            base.Update();
        }

        public static void UpdateParticle(Particle particle)
        {
            particle.Velocity *= 0.98f;
            particle.Alpha -= OE.Delta;
        }
        
    }
}
