using System;
using System.Drawing;
using Tie_Fighter.Others;

namespace Tie_Fighter.GameObjects
{
    public abstract class GameObject<T> : IDisposable // where T : IConvertible --> Is probably not needed.
    {
        public bool disposed = false;
        protected Bitmap bitmap;
        protected Others.MediaPlayer mediaPlayer;

        public GameObject(Others.MediaPlayer mediaPlayer, T xPercentage, T yPercentage, T widthPercentage, T heightPercentage)
        {
            this.mediaPlayer = mediaPlayer;
            this.xPercentage = xPercentage;
            this.yPercentage = yPercentage;
            this.widthPercentage = widthPercentage;
            this.heightPercentage = heightPercentage;
        }

        public GameObject(Others.MediaPlayer mediaPlayer)
        {
        }

        public void SetXYWH(T xPercentage, T yPercentage, T widthPercentage, T heightPercentage)
        {
            this.xPercentage = xPercentage;
            this.yPercentage = yPercentage;
            this.widthPercentage = widthPercentage;
            this.heightPercentage = heightPercentage;
        }

        public virtual T xPercentage { get; set; } // X-position.
        public virtual T yPercentage { get; set; } // Y-position.
        public virtual T widthPercentage { get; set; } // Desired width of image.
        public virtual T heightPercentage { get; set; } // Desired height of image.
        public virtual Tuple<T, T> GetPosition()
        {
            return new Tuple<T, T>(xPercentage, yPercentage);
        }
        public virtual void Rescale(T width, T height)
        {
            this.widthPercentage = width;
            this.heightPercentage = height;
        }
        public virtual void Draw(Graphics graphics, int screenWidthPixels, int screenHeightPixels)
        {
            //graphics.DrawImage(bitmap, Convert.ToSingle(x), Convert.ToSingle(y));
            int x, y, width, height;
            x = (int)PercentageToPixels(this.xPercentage, screenWidthPixels);
            y = (int)PercentageToPixels(this.yPercentage, screenHeightPixels);
            width = (int)PercentageToPixels(this.widthPercentage, screenWidthPixels);
            height = (int)PercentageToPixels(this.heightPercentage, screenHeightPixels);
            Rectangle rectangle = new Rectangle(x, y, width, height);
            graphics.DrawImage(bitmap, rectangle);
        }

        public double PercentageToPixels(T percentage, int totalPixels)
        {
            if (Convert.ToInt32(percentage) != 0)
                return (totalPixels / (100.0 / Convert.ToDouble(percentage)));
            else return 0;
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
