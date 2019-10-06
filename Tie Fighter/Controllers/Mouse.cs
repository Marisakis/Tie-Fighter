using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tie_Fighter.Controllers
{
    public class Mouse<MouseEvent> : GeneralController<MouseEvent> where MouseEvent : MouseEventArgs
    {
        public override void Action(MouseEvent eventData)
        {
            int mousePosX = eventData.X;
            int mousePosY = eventData.Y;
        }
    }
}
