using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor.Base
{
    class Selection : ISelection
    {
        private Point _Position;        
        public Point Position {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
                Changed(this, new EventArgs());
            }
        }
        private Size _Size;
        public Size Size
        {
            get
            {
                return _Size;
            }

            set
            {
                _Size = value;
                Changed(this, new EventArgs());
            }
        }
        
        public bool isEditable { get; set; }        

        public event EventHandler<EventArgs> Changed = delegate { };


        public Selection(Point position, int width, int height)
        {
            Position = position;
            Size = new Size(width, height);
            isEditable = true;
        }
        public Selection(Point position) : this(position, 0, 0) { }

        public void Draw(Image image)
        {

            Graphics g = null;
            Graphics gImage = null;
            try
            {
                int dX = Math.Abs(Size.Width);
                int dY = Math.Abs(Size.Height);

                Bitmap bitmap = new Bitmap(dX * 2 + 1, dY * 2 + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bitmap.MakeTransparent();
                g = Graphics.FromImage(bitmap);
                Pen pen = new Pen(Brushes.Lime, 1);
                {
                    //g.DrawLine(new Pen(Brushes.Lime, 1), dX, dY, Size.Width + dX, Size.Height + dY);
                    g.DrawLine(pen, 0, 0, dX * 2, dY * 2);
                    g.DrawLine(pen, 0, dY * 2, dX * 2, 0);
                    g.DrawRectangle(pen, 0, 0, dX * 2, dY * 2);
                }
                gImage = Graphics.FromImage(image);
                gImage.DrawImage(bitmap, Position.X - dX, Position.Y - dY);
                
            }
            catch (Exception)
            {

            }
            finally
            {
                if (gImage != null)
                    gImage.Dispose();
                if (g != null)
                    g.Dispose();
            }
            
        }
    }
}
