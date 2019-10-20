using System;
using System.Drawing;

namespace Tie_Fighter.GameObjects
{
    /// <summary>
    /// Basic methods to draw and display. Note that everything is in percentages for optimal compatibility.
    /// </summary>
    public abstract class GameObject : IDisposable
    {
        public bool disposed = false;
        protected Bitmap bitmap;
        protected Others.MediaPlayerHandler mediaPlayer;
        private Rectangle rectangle;
        public Others.MediaPlayer usedMediaPlayer;
        public int id { get; set; }
        /// <summary>
        /// GameObject handles the drawing of objects, and contains methods to convert a percentage to pixel (range). 
        /// </summary>
        /// <param name="mediaPlayerHandler">_Can_ be used to play sounds, but _is allowed_ to be null.</param>
        /// <param name="percentageX">Set the x percentage value, is later converted to pixels.</param>
        /// <param name="percentageY">Set the y percentage value, is later converted to pixels.</param>
        /// <param name="percentageWidth">Set the percentage width.</param>
        /// <param name="percentageHeight">Set the percentage height.</param>
        public GameObject(Others.MediaPlayerHandler mediaPlayerHandler, int percentageX, int percentageY, int percentageWidth, int percentageHeight)
        {
            mediaPlayer = mediaPlayerHandler;
            this.percentageX = percentageX;
            this.percentageY = percentageY;
            this.percentageWidth = percentageWidth;
            this.percentageHeight = percentageHeight;
            rectangle = new Rectangle();
        }

        /// <summary>
        /// Sets x value in percentage of an object you want to draw.
        /// </summary>
        public virtual int percentageX { get; set; } // X-position.
        /// <summary>
        /// Sets y value in percentage of an object you want to draw.
        /// </summary>
        public virtual int percentageY { get; set; } // Y-position.
        /// <summary>
        /// Sets width value in percentage of an object you want to draw.
        /// </summary>
        public virtual int percentageWidth { get; set; } // Desired width of image.
        /// <summary>
        /// Sets height value in percentage of an object you want to draw.
        /// </summary>
        public virtual int percentageHeight { get; set; } // Desired height of image.

        /// <summary>
        /// Get the x and y value using a Tuple.
        /// </summary>
        /// <returns></returns>
        public virtual Tuple<int, int> GetPosition()
        {
            return new Tuple<int, int>(percentageX, percentageY);
        }
        /// <summary>
        /// Set the X and Y value, in pixels.
        /// </summary>
        /// <param name="pixelsX">Used to draw an object on a specific x position.</param>
        /// <param name="pixelsY">Used to draw an object on a specific y position.</param>
        /// <param name="pixelsWidth">Used to calculate percentage x.</param>
        /// <param name="pixelsHeight">Used to calculate percentage y.</param>
        public virtual void SetXY(int pixelsX, int pixelsY, int pixelsWidth, int pixelsHeight)
        {
            percentageX = PixelsToPercentage(pixelsX, pixelsWidth);
            percentageY = PixelsToPercentage(pixelsY, pixelsHeight);
        }

        /// <summary>
        /// Draw a specific object.
        /// </summary>
        /// <param name="graphics">Graphics are used to draw on a Forms window. May not be null.</param>
        /// <param name="pixelsWidth">Total width in pixels (for dynamic drawing depending on screen ratio and screen dimensions).</param>
        /// <param name="pixelsHeight">Total height in pixels (for dynamic drawing depending on screen ratio and screen dimensions).</param>
        /// <param name="centerImage">Center the object.</param>
        public virtual void Draw(Graphics graphics, int pixelsWidth, int pixelsHeight, bool centerImage = false)
        {
            int x = PercentageToPixels(percentageX, pixelsWidth);
            int y = PercentageToPixels(percentageY, pixelsHeight);
            int width = PercentageToPixels(percentageWidth, pixelsWidth);
            int height = PercentageToPixels(percentageHeight, pixelsHeight);

            if (centerImage)
            {
                x -= width / 2;
                y -= height / 2;
            }
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            graphics.DrawImage(bitmap, rectangle);
        }
        /// <summary>
        /// Convert percentage to pixels.
        /// </summary>
        /// <param name="percentage"></param>
        /// <param name="totalPixels"></param>
        /// <returns></returns>
        public int PercentageToPixels(int percentage, int totalPixels)
        {
            return (int)(percentage * (totalPixels / 100.0));
        }
        /// <summary>
        /// Convert pixels to percentage.
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="totalPixels"></param>
        /// <returns></returns>
        public int PixelsToPercentage(int pixels, int totalPixels)
        {
            return (int)(((pixels + 0.0) / totalPixels) * 100.0);
        }
        /// <summary>
        /// Stop the WMP that is in use.
        /// </summary>
        public void StopMediaPlayer()
        {
            if (usedMediaPlayer != null)
            {
                usedMediaPlayer.EndPlay();
            }
        }
        /// <summary>
        /// Dispose the bitmap image to prevent a Stack overflow.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the bitmap image to prevent a StackOverflow.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                bitmap.Dispose();
            }

            disposed = true;
        }
    }
}
