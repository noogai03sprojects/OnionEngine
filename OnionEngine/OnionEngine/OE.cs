using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OnionEngine
{
    /// <summary>
    /// The engine's catch-all main class, like FP in FlashPunk and FlxG in Flixel.
    /// </summary>
    static class OE
    {
        public static bool HasStage = false;
        public static Stage Stage;

        public static GameTime gameTime;
        public static float Delta;

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
            Delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (IsFirstFrame

            Input.Update();
        }

        public static Texture2D LoadGraphic(string path)
        {
            return Content.Load<Texture2D>(path);
        }

        public static int RandInt(int max)
        {
            return Random.Next(max);
        }
        public static float RandFloat(float min, float max)
        {
            return (float)Random.NextDouble() * (max - min) + min;
        }
    }
}
