using System.Windows.Forms;

namespace Tie_Fighter.Controllers
{
    /// <summary>
    /// Handles KeyboardEvents.
    /// </summary>
    /// <typeparam name="KeyboardEvent"></typeparam>
    public class Keyboard<KeyboardEvent> : GeneralController<KeyboardEvent, int> where KeyboardEvent : KeyEventArgs
    {
        private readonly int _minSensitivity = 3;
        private int _sensitivity = 5;
        private const int _maxSensitivity = 50;

        /// <summary>
        /// Constructor of the keyboard class defines x and y (IActionInput) as an integer. Which is passed to base class.
        /// </summary>
        /// <param name="actionInput"></param>
        public Keyboard(IActionInput<int> actionInput) : base(actionInput)
        {

        }

        /// <summary>
        /// Defines a specific action for pressing a specific button.
        /// </summary>
        /// <param name="eventData"></param>
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

        /// <summary>
        /// Move crosshair in specific direction by calling actionInput and take sensitivity into account.
        /// </summary>

        public void Up()
        {
            base.actionInput.UpdatePosition(0, -1 * _sensitivity);
        }
        /// <summary>
        /// Move crosshair in specific direction by calling actionInput and take sensitivity into account.
        /// </summary>
        public void Down()
        {
            base.actionInput.UpdatePosition(0, 1 * _sensitivity);
        }
        /// <summary>
        /// Move crosshair in specific direction by calling actionInput and take sensitivity into account.
        /// </summary>
        public void Left()
        {
            base.actionInput.UpdatePosition(-1 * _sensitivity, 0);
        }
        /// <summary>
        /// Move crosshair in specific direction by calling actionInput and take sensitivity into account.
        /// </summary>
        public void Right()
        {
            base.actionInput.UpdatePosition(1 * _sensitivity, 0);
        }
        /// <summary>
        /// Increase sensitivity.
        /// </summary>
        public void SensUp()
        {
            if (_sensitivity * 2 < _maxSensitivity)
            {
                _sensitivity *= 2;
            }
        }
        /// <summary>
        /// Decrease sensitivity.
        /// </summary>
        public void SensDown()
        {
            if (_sensitivity / 2 > _minSensitivity)
            {
                _sensitivity /= 2;
            }
        }
    }
}
