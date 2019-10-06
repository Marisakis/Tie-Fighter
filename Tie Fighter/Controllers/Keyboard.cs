using System.Windows.Forms;

namespace Tie_Fighter.Controllers
{
    public class Keyboard<KeyboardEvent> : GeneralController<KeyboardEvent, int> where KeyboardEvent : KeyEventArgs
    {
        private int _sensitivity = 1;
        private const int _maxValue = 50;

        public Keyboard(IActionInput<int> actionInput) : base(actionInput)
        {

        }

        public override void Action(KeyboardEvent eventData)
        {
            switch (eventData.KeyCode)
            {
                case Keys.Up:
                    Up();
                    break;
                case Keys.Down:
                    Down();
                    break;
                case Keys.Left:
                    Left();
                    break;
                case Keys.Right:
                    Right();
                    break;
                case Keys.W:
                    Up();
                    break;
                case Keys.S:
                    Down();
                    break;
                case Keys.A:
                    Left();
                    break;
                case Keys.D:
                    Right();
                    break;
                case Keys.Oemplus:
                    SensUp();
                    break;
                case Keys.OemMinus:
                    SensDown();
                    break;
                case Keys.Space:
                    base.actionInput.Fire();
                    break;
            }
        }

        public void Up()
        {
            base.actionInput.UpdatePosition(0, 1 * _sensitivity);
        }

        public void Down()
        {
            base.actionInput.UpdatePosition(0, -1 * _sensitivity);
        }

        public void Left()
        {
            base.actionInput.UpdatePosition(-1 * _sensitivity, 0);
        }

        public void Right()
        {
            base.actionInput.UpdatePosition(1 * _sensitivity, 0);
        }

        public void SensUp()
        {
            if (_sensitivity * 2 < _maxValue)
                _sensitivity *= 2;
        }

        public void SensDown()
        {
            if (_sensitivity / 2 > 1)
                _sensitivity /= 2;
        }
    }
}
