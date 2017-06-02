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
        private Point _middlePointPosition;
        
        public Point MiddlePointPosition {
            get
            {
                return _middlePointPosition;
            }
            set
            {
                _middlePointPosition = value;
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
            MiddlePointPosition = position;
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
                Bitmap bitmap = new Bitmap(Size.Width + 1, Size.Height + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bitmap.MakeTransparent();
                g = Graphics.FromImage(bitmap);
                Pen pen = new Pen(Brushes.Lime, 1);
                {
                    //g.DrawLine(new Pen(Brushes.Lime, 1), dX, dY, Size.Width + dX, Size.Height + dY);
                    g.DrawLine(pen, 0, 0, Size.Width, Size.Height);
                    g.DrawLine(pen, 0, Size.Height, Size.Width, 0);
                    g.DrawRectangle(pen, 0, 0, Size.Width, Size.Height);
                }
                gImage = Graphics.FromImage(image); 
                gImage.DrawImage(bitmap, MiddlePointPosition.X - Size.Width / 2, MiddlePointPosition.Y - Size.Height / 2);
                
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

        public Rectangle getRegion()
        {
            try
            {
                Point TopLeft = new Point()
                {
                    X = MiddlePointPosition.X - Size.Width / 2,
                    Y = MiddlePointPosition.Y - Size.Height / 2
                };
                return new Rectangle(TopLeft, Size);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
