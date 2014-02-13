using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnionEngine
{
    /// <summary>
    /// A particle emitter. Hopefully it's efficient :D
    /// </summary>
    public class Emitter
    {
        List<Particle> Particles;

        
        public Emitter(int capacity = 1000)
        {
            Particles = new List<Particle>(capacity);
        }
    }
}
