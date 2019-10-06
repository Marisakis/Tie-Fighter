using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.GameObjects
{
    public class Fighter<T> : GameObject<T>
    {
        public T ttp { get; set; } // Time to pass, in milliseconds.
    }
}
