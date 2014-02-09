using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionEngine
{
    struct Animation
    {
        public int[] Frames;
        public string Name;
        public bool Looped;
        public float Delay;

        public Animation(string name, int[] frames, bool looped, int fps)
        {
            Frames = frames;
            Name = name;
            Looped = looped;
            if (fps > 0)
                Delay = 1 / (float)fps;
            else
                Delay = 0;
        }
    }
}
