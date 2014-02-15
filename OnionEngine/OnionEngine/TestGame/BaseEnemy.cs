using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnionEngine;

namespace OnionTestGame
{
    class BaseEnemy : Entity
    {
        protected Player player;
        public float InvulnTimer = 0;

        public BaseEnemy(Player play)
        {
            player = play;
        }

        public override void Update()
        {
            

            base.Update();
            //InvulnTimer -= OE.Delta;
        }
    }
}
