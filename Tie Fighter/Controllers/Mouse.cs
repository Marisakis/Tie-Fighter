using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tie_Fighter.Controllers
{
    public class Mouse<MouseEvent, T> : GeneralController<MouseEvent, int> where MouseEvent : MouseEventArgs
    {
        public Mouse(IActionInput<int> actionInput) : base(actionInput)
        {
        }

        public override void Action(MouseEvent eventData)
        {
            int mousePosX = eventData.X;
            int mousePosY = eventData.Y;
            base.actionInput.MoveTo(mousePosX, mousePosY);
        }
    }
}
