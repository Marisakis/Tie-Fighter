using System;
using System.Drawing;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects
{
    public abstract class GameObject : IDisposable 
    {
        public bool disposed = false;
        protected Bitmap bitmap;
        protected Others.MediaPlayerHandler mediaPlayer;
        private Rectangle rectangle;
        public int id;

        public GameObject(Others.MediaPlayerHandler mediaPlayerHandler, int percentageX, int percentageY, int percentageWidth, int percentageHeight)
        {
            this.mediaPlayer = mediaPlayerHandler;
            this.percentageX = percentageX;
            this.percentageY = percentageY;
            this.percentageWidth = percentageWidth;
            this.percentageHeight = percentageHeight;
            this.rectangle = new Rectangle();
        }
        public virtual int percentageX { get; set; } // X-position.
        public virtual int percentageY { get; set; } // Y-position.
        public virtual int percentageWidth { get; set; } // Desired width of image.
        public virtual int percentageHeight { get; set; } // Desired height of image.
        public virtual Tuple<int, int> GetPosition()
        {
            return new Tuple<int, int>(percentageX, percentageY);
        }

        public virtual void SetXY(int pixelsX, int pixelsY, int pixelsWidth, int pixelsHeight)
        {
            this.percentageX = PixelsToPercentage(pixelsX, pixelsWidth);
            this.percentageY = PixelsToPercentage(pixelsY, pixelsHeight);
        }

        public virtual void Draw(Graphics graphics, int pixelsWidth, int pixelsHeight, bool centerImage = false)
        { 
            int x = PercentageToPixels(this.percentageX, pixelsWidth);
            int y = PercentageToPixels(this.percentageY, pixelsHeight);
            int width = PercentageToPixels(this.percentageWidth, pixelsWidth);
            int height = PercentageToPixels(this.percentageHeight, pixelsHeight);

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

        public int PercentageToPixels(int percentage, int totalPixels)
        {
            return (int)(percentage*(totalPixels/100.0));
        }

        public int PixelsToPercentage(int pixels, int totalPixels)
        {
            return (int)(((pixels+0.0) / totalPixels) * 100.0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) bitmap.Dispose();
            disposed = true;
        }
    }
}
