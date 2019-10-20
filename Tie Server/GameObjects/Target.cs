namespace Tie_Server.GameObjects
{
    /// <summary>
    /// General Target object.
    /// </summary>
    internal class Target : GameObject
    {
        /// <summary>
        /// A Target (Tie Fighter for example) must have a unique ID (it's allowed to do previous id++!), a time to pass (which really shouldn't be higher than 10, an x-position, y-position and width and height.
        /// </summary>
        /// <param name="TTP">Defines how long the fighter will need to fly from begin to end.</param>
        /// <param name="id">Unique ID.</param>
        /// <param name="x">x-pos.</param>
        /// <param name="y">y-pos.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Target(int TTP, int id, double x, double y, int width, int height) : base(id, x, y, width, height)
        {
            this.TTP = TTP;
        }
        /// <summary>
        /// TTP, defines how long the Target will take to fly from 0 to 100 percent of screen.
        /// </summary>
        public int TTP { get; set; }
    }
}
