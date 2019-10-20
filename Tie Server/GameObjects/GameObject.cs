namespace Tie_Server.GameObjects
{
    /// <summary>
    /// General GameObject.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Unique ID.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// x-pos.
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// y-pos.
        /// </summary>
        public double y { get; set; }
        /// <summary>
        /// Width of object.
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// Height of object.
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// A GameObject should always have an ID, x-value, y-value, width and height. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected GameObject(int id, double x, double y, int width, int height)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

    }
}
