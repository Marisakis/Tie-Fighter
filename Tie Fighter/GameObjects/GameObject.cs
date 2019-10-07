﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tie_Fighter.GameObjects
{
    public abstract class GameObject<T> : IDisposable // where T : IConvertible --> Is probably not needed.
    {
        bool disposed = false;
        public Bitmap bitmap { get; set; }

        public virtual T x { get; set; } // X-position.
        public virtual T y { get; set; } // Y-position.
        public virtual T width { get; set; } // Desired width of image.
        public virtual T height { get; set; } // Desired height of image.
        public virtual Tuple<T, T> GetPosition()
        {
            return new Tuple<T, T>(x, y);
        }
        public virtual void Rescale(T width, T height) 
        {
            this.bitmap = new Bitmap(this.bitmap, new Size(Convert.ToInt32(width), Convert.ToInt32(height)));
        }
        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawImage(bitmap, Convert.ToSingle(x), Convert.ToSingle(y));
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
