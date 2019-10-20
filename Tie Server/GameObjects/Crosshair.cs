namespace Tie_Server.GameObjects
{
    /// <summary>
    /// Crosshair object.
    /// </summary>
    public class Crosshair : GameObject
    {
        /// <summary>
        /// A crosshair has dimensions on a specific position. Id might be used later.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// <param name="x">Position x.</param>
        /// <param name="y">Position y.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Crosshair(int id, double x, double y, int width, int height) : base(id, x, y, width, height)
        {
            isFiring = false;
        }

        /// <summary>
        /// Used to determine whether or not a user is firing.
        /// </summary>
        public bool isFiring { get; set; }
    }
}
