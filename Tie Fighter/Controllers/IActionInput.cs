namespace Tie_Fighter.Controllers
{
    public interface IActionInput<T>
    {
        void MoveTo(T x, T y);
        void UpdatePosition(T x, T y);
        void Fire();
    }
}
