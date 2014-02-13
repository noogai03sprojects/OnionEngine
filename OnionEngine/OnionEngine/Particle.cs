using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionEngine
{
    /// <summary>
    /// Used as an object pool by Emitter.
    /// </summary>
    public struct Particle
    {
        public bool Alive = false;
        public int Age = 0;

        public Action UpdateMethod;
    }
}
