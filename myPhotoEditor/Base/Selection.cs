using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPhotoEditor.Base
{    
    class Selection
    {
        public Point Location
        {
            get
            {
                return MidPoint2TopLeft();
            }            
        }
        private Point _MiddlePointPosition;
        public Point MiddlePointPosition {
            get
            {
                return _MiddlePointPosition;
            }
            set
            {
                _MiddlePointPosition = value;                
                LocationChanged(this, new EventArgs());
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
                SizeChanged(this, new EventArgs());
            }
        }
        public int Width
        {
            get
            {
                return Size.Width;
            }
            set
            {
                Size = new Size(value, Size.Height);
            }
        }
        public int Height
        {
            get
            {
                return Size.Height;
            }
            set
            {
                Size = new Size(Size.Width, value);
            }
        }

        public bool MouseEntered { get; private set; }

        private MouseEventArgs _MouseEventArgs;
        public MouseEventArgs MouseEventArgs
        {
            private get
            {
                return _MouseEventArgs;
            }

            set
            {
                _MouseEventArgs = value;
                if (getRegion().Contains(value.Location))
                {
                    if (!MouseEntered)
                    {
                        MouseEntered = true;
                        MouseEnter(this, value);
                    }
                }
                else
                {
                    if (MouseEntered)
                    {
                        MouseEntered = false;
                        MouseLeave(this, value);
                    }
                }
            }
        }

        private SelectionStyle _SelectionStyle;
        public SelectionStyle SelectionStyle
        {
            get
            {
                return _SelectionStyle;
            }

            set
            {
                _SelectionStyle = value;
                SelectionStyleChanged(this, new EventArgs());
            }
        }

        public event EventHandler SizeChanged = delegate { };
        public event EventHandler LocationChanged = delegate { };
        public event EventHandler SelectionStyleChanged = delegate { };
        public event MouseEventHandler MouseEnter = delegate { };
        public event MouseEventHandler MouseLeave = delegate { };


        public bool isEditable { get; set; }

        

        public Selection(Point position, int width, int height)
        {
            MiddlePointPosition = position;
            Size = new Size(width, height);
            isEditable = true;            
        }
        public Selection(Point position) : this(position, 0, 0) { }

        private Point MidPoint2TopLeft()
        {
            return new Point()
            {
                X = (int)(((double)MiddlePointPosition.X - Size.Width / 2)),
                Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2))
            };
        }
        private Point TopLeft2MidPoint()
        {
            return new Point()
            {
                X = (int)(((double)Location.X + Size.Width / 2)),
                Y = (int)(((double)Location.Y + Size.Height / 2))
            };
        }
        
        public void Draw(Image image)
        {
            Graphics g = null;
            Graphics gImage = null;
            try
            {
                g = Graphics.FromImage(image);
                Pen pen = new Pen(Brushes.Lime, 1);
                {
                    switch (SelectionStyle)
                    {
                        
                        case SelectionStyle.BoxDiagonal:
                            g.DrawLine(pen, Location.X, Location.Y, Location.X + Width, Location.Y + Height);
                            g.DrawLine(pen, Location.X, Location.Y + Height, Location.X + Width, Location.Y);
                            g.DrawRectangle(pen, Location.X, Location.Y, Width, Height);
                            break;
                        default: //SelectionStyle.BoxMiddleOrthoAxis
                            g.DrawLine(pen, Location.X, Location.Y + Height / 2, Location.X + Width, Location.Y + Height / 2);
                            g.DrawLine(pen, Location.X + Width / 2, Location.Y, Location.X + Width / 2, Location.Y + Height);
                            g.DrawRectangle(pen, Location.X, Location.Y, Width, Height);
                            break;
                    }
                }
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

        public Rectangle getRegion(Point offset)
        {
            try
            {
                Point TopLeft = new Point()
                {
                    X = (int)(((double)MiddlePointPosition.X - Size.Width / 2 + offset.X)) + 1,
                    Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2 + offset.Y)) + 1
                };
                return new Rectangle(TopLeft, Size);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Rectangle getRegion()
        {
            return getRegion(Point.Empty);
        }
        public Rectangle getRegionReal(double scale, Point offset)
        {
            try
            {
                Point TopLeft = new Point()
                {
                    X = (int)(((double)MiddlePointPosition.X - Size.Width / 2 + offset.X) / scale) + 1,
                    Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2 + offset.Y) / scale) + 1
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
        public Rectangle getRegionReal(double scale)
        {
            return getRegionReal(scale, Point.Empty);
        }


        public void Offset(Point offset)
        {
            Offset(offset.X, offset.Y);
        }
        public void Offset(int dX, int dY)
        {
            MiddlePointPosition = new Point(
                MiddlePointPosition.X + dX,
                MiddlePointPosition.Y + dY
            );
        }
    }
}
