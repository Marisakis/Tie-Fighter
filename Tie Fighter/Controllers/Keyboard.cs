using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tie_Fighter.Controllers
{
    public class Keyboard<KeyboardEvent, T> : GeneralController<KeyboardEvent, T> where KeyboardEvent : KeyEventArgs
    {
        public Keyboard(IActionInput<T> actionInput) : base(actionInput)
        {
        }

        public override void Action(KeyboardEvent eventData)
        {
            switch (eventData.KeyCode)
            {
                case Keys.Up:
                    actionInput.MoveTo();
                    break;
                case Keys.Down: break;
                case Keys.Left: break;
                case Keys.Right: break;
                case Keys.W: break;
                case Keys.S: break;
                case Keys.A: break;
                case Keys.D: break;
            }
        }
    }
}
