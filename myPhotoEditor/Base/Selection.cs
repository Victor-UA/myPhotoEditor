using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    g.DrawLine(pen, 0, 0, Size.Width, Size.Height);
                    g.DrawLine(pen, 0, Size.Height, Size.Width, 0);
                    g.DrawRectangle(pen, 0, 0, Size.Width, Size.Height);
                }
                gImage = Graphics.FromImage(image); 
                gImage.DrawImage(bitmap, MiddlePointPosition.X - Size.Width / 2, MiddlePointPosition.Y - Size.Height / 2);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\r" + ex.StackTrace);
            }
            finally
            {
                if (gImage != null)
                    gImage.Dispose();
                if (g != null)
                    g.Dispose();
            }
            
        }

        public Rectangle getRegion(double scale = 1)
        {
            try
            {
                Point TopLeft = new Point()
                {
                    X = (int)(((double)MiddlePointPosition.X - Size.Width / 2) / scale) + 1,
                    Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2) / scale) + 1
                };

                if (scale == 1)
                {
                    return new Rectangle(TopLeft, Size);
                }
                else
                {
                    Size size = new Size((int)((Size.Width) / scale), (int)((Size.Height) / scale));
                    return new Rectangle(TopLeft, size);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
