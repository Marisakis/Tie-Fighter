using System.Windows.Forms;

namespace Tie_Fighter.Controllers
{
    /// <summary>
    /// Handles MouseEvents.
    /// </summary>
    /// <typeparam name="MouseEvent"></typeparam>
    public class Mouse<MouseEvent> : GeneralController<MouseEvent, int> where MouseEvent : MouseEventArgs
    {
        bool fired;
        public Mouse(IActionInput<int> actionInput) : base(actionInput)
        {
        }

        public override void Action(MouseEvent eventData)
        {
            int mousePosX = eventData.X;
            int mousePosY = eventData.Y;
            base.actionInput.MoveTo(mousePosX, mousePosY);
            if (eventData.Button == MouseButtons.Left && !fired)
            {
                base.actionInput.Fire();
                fired = true;
            }else
            {
                if (eventData.Button != MouseButtons.Left)
                {
                    fired = false;
                }
            }
        }


    }
}
