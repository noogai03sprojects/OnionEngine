using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionEngine
{
    /// <summary>
    /// Struct for handling frames of animation.
    /// </summary>
    struct Animation
    {
        public int[] Frames;
        public string Name;
        public bool Looped;

        /// <summary>
        /// Ignore this; use the FPS property instead.
        /// </summary>
        public float Delay;

        /// <summary>
        /// The framerate of the animation. Change this instead of Delay.
        /// </summary>
        public int FPS
        {
            set
            {
                if (value > 0)
                    Delay = 1 / (float)value;
                else
                    Delay = 0;
            }
        }

        /// <summary>
        /// Creates an Animation.
        /// </summary>
        /// <param name="name">The name of the animation, to be accessed via Entity.Play()</param>
        /// <param name="frames">An array of integers, representing the frames in the animation. Sprites are by default split up by LoadGraphic into horizontal rows. I suggest using "new int[] { [array here] } for ease of use.</param>
        /// <param name="looped">You guess.</param>
        /// <param name="fps">The framerate to run this animation at. Higher is faster.</param>
        public Animation(string name, int[] frames, bool looped, int fps)
        {
            Frames = frames;
            Name = name;
            Looped = looped;
            if (fps > 0)
                Delay = 1 / (float)fps;
            else
                Delay = 0;

            Length = Frames.Length;
        }

        /// <summary>
        /// The number of frames in the animation.
        /// </summary>
        public int Length;
    }
}
