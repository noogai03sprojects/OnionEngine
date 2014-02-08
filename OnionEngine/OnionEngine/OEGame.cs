using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace OnionEngine
{
    class OEGame : Game
    {
        GraphicsDeviceManager graphics;        

        public OEGame()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            Content.RootDirectory = "Content";
            OE.Content = Content;
            InitGame();
            base.Initialize();
        }

        public void InitGame()
        {
            
            OE.Device = graphics.GraphicsDevice;
            OE.Game = this;
            OE.SpriteBatch = new SpriteBatch(OE.Device);
            OE.PrimBatch = new PrimitiveBatch(OE.Device);
            OE.Stage.Init();
            
        }

        protected override void BeginRun()
        {
            base.BeginRun();
        }

        protected override void Update(GameTime gameTime)
        {
            OE.gameTime = gameTime;
            OE.Update();
            //Console.WriteLine(OE.delta);

            if (OE.HasStage)
            {
                OE.Stage.Update();
            }
            //Console.WriteLine(OE.Device.Viewport.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            OE.SpriteBatch.Begin();
            if (OE.HasStage)
            {
                OE.Stage.Draw();
            }
            OE.SpriteBatch.End();
            OE.Debug.Draw();

            base.Draw(gameTime);
        }
    }
}
