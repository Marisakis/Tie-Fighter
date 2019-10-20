namespace Tie_Server.GameObjects
{
    /// <summary>
    /// Explosion object. An explosion has dimensions and is on a specific location. 
    /// </summary>
    internal class Explosion : GameObject
    {
        /// <summary>
        /// Explosion has a unique ID so a client can determine whether an explosion should be removed.
        /// </summary>
        /// <param name="TTL">Time to live, before explosion is removed.</param>
        /// <param name="id">Unique ID.</param>
        /// <param name="x">Position x.</param>
        /// <param name="y">Position y.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Explosion(int TTL, int id, double x, double y, int width, int height) : base(id, x, y, width, height)
        {
            this.TTL = TTL;
        }

        /// <summary>
        /// Time To Live (in millis) of explosion.
        /// </summary>
        public int TTL { get; set; }
    }
}
