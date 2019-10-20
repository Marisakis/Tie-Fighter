namespace Tie_Fighter.Controllers
{

    /// <summary>
    /// Interface to handle specific actions, from a source such as the Leap Motion, a Keyboard, or a mouse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IActionInput<T>
    {
        void MoveTo(T x, T y);
        void UpdatePosition(T x, T y);
        void Fire();
    }
}
