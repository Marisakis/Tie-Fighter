namespace Tie_Fighter.Controllers
{

    /// <summary>
    /// Interface to handle specific actions, from a source such as the Leap Motion, a Keyboard, or a mouse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IActionInput<T>
    {
        /// <summary>
        /// Move the crosshair to a specific target position by replacing the x and y values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void MoveTo(T x, T y);
        /// <summary>
        /// Move the crosshair to a specific target position by addition on the x and y values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void UpdatePosition(T x, T y);
        /// <summary>
        /// Fire on the crosshair position.
        /// </summary>
        void Fire();
    }
}
