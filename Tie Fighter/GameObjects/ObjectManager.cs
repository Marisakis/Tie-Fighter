using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tie_Fighter.GameObjects
{
    /// <summary>
    /// Local clientside Object Manager. This class keeps track of all objects, handling movement, drawing and deleting of destroyed object
    /// </summary>
    class ObjectManager
    {
        public List<GameObject<double>> gameObjects { get; set; }

    }
}
