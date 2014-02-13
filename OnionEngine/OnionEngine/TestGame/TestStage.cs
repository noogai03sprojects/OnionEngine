using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnionEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OnionTestGame
{
    class TestStage : Stage
    {
        Player player;

        float timer = 1;

        public List<Enemy> Enemies = new List<Enemy>();

        Text text;

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
                Remove(player);
            }


            timer -= OE.Delta;
            if (timer <= 0)
            {
                Enemies.Add(new Enemy(player));
                Add(Enemies);

                timer = 2f;
            }

            Enemies.RemoveAll(x => !x.Alive);

            base.Update();
        }
    }
}
