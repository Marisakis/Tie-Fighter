using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.GameObjects
{
    public abstract class Fighter<T> : GameObject<T>
    {
        public virtual T TTP { get; set; } // Time to pass, in milliseconds.
        public virtual void PlayFlySound()
        {

        }
    }
}
