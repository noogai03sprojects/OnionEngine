using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OnionEngine
{
    static class OE
    {
        public static bool HasStage = false;
        public static Stage Stage;

        public static GameTime gameTime;
        public static float delta;

        public static SpriteBatch SpriteBatch;
        public static GraphicsDevice Device;
        public static PrimitiveBatch PrimBatch;

        public static OEGame Game;

        public static Input Input = new Input();

        public static Debug Debug = new Debug();

        public static Random Random = new Random();

        public static List<object> UserData = new List<object>();

        public static ContentManager Content;

        //public static readonly bool IsFirstFrame = true;

        public static void SetStage(Stage s)
        {
            Stage = s;
            HasStage = true;
        }

        public static void Update()
        {            
            delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (IsFirstFrame

            Input.Update();
        }
    }
}
