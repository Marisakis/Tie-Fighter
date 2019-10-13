using System.Windows.Forms;

namespace Tie_Fighter.Controllers
{
    public class Keyboard<KeyboardEvent> : GeneralController<KeyboardEvent, int> where KeyboardEvent : KeyEventArgs
    {
        private int _minSensitivity = 3;
        private int _sensitivity = 5;
        private const int _maxSensitivity = 50;

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
            base.actionInput.UpdatePosition(0, -1 * _sensitivity);
        }

        public void Down()
        {
            base.actionInput.UpdatePosition(0, 1 * _sensitivity);
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
            if (_sensitivity * 2 < _maxSensitivity)
                _sensitivity *= 2;
        }

        public void SensDown()
        {
            if (_sensitivity / 2 > _minSensitivity)
                _sensitivity /= 2;
        }
    }
}
