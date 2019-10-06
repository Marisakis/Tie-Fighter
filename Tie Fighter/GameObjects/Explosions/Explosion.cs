using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.GameObjects.Explosions
{
    public class Explosion<T> : GameObject<T>
    {
        public T TTL { get; set; }
    }
}
