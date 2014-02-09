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

        public override void Init()
        {
            bgColor = Color.CornflowerBlue;

            player = new Player();            
            Add(player);

            Enemies.Add(new Enemy(player));
            Add(Enemies);

            OE.UserData.Add(this);
            
            base.Init();
        }

        public override void Update()
        {
            if (OE.Input.Pressed(Keys.F1))
            {
                OE.Debug.Enabled = !OE.Debug.Enabled;
            }
            timer -= OE.Delta;
            if (timer <= 0)
            {
                Enemies.Add(new Enemy(player));
                Add(Enemies);

                timer = 1f;
            }

            Enemies.RemoveAll(x => !x.Alive);

            base.Update();
        }
    }
}
