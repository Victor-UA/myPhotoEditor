using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{    
    public class Selection
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
                BorderChanged();
                LocationChanged(this, new EventArgs());
            }
        }
        private Point _MiddlePointRealPosition;
        public Point MiddlePointRealPosition
        {
            get
            {
                return _MiddlePointRealPosition;
            }

            set
            {
                _MiddlePointRealPosition = value;                
            }
        }

        private Size _Size;
        public Size Size
        {
            get
            {
                return _Size;
            }

            private set
            {
                _Size = value;
                BorderChanged();
                SizeChanged(this, new EventArgs());
            }
        }
        public int Width
        {
            get
            {
                return Size.Width;
            }            
        }
        public int Height
        {
            get
            {
                return Size.Height;
            }            
        }
        private Size _RealSize;
        public Size RealSize
        {
            get
            {
                return _RealSize;
            }

            set
            {
                _RealSize = value;
            }
        }
        public int RealWidth
        {
            get
            {
                return RealSize.Width;
            }

            set
            {
                if (value >= 0)
                {
                    RealSize = new Size(value, _RealSize.Height);
                    SizeRecalc();
                }
            }
        }
        public int RealHeight
        {
            get
            {
                return RealSize.Height;
            }

            set
            {
                if (value >= 0)
                {
                    RealSize = new Size(RealSize.Width, value);
                    SizeRecalc();
                }
            }
        }
        private double _Scale;
        public double Scale
        {
            get
            {
                return _Scale;
            }

            set
            {
                _Scale = value;
                SizeRecalc();
            }
        }
        public Border Border { get; private set; } = new Border();

        private bool _MouseEntered;
        public bool MouseEntered
        {
            get
            {
                return _MouseEntered;
            }
            private set
            {
                _MouseEntered = value;
                if (value)
                {
                    MouseEnter(this, MouseEventArgs);
                }
                else
                {
                    MouseLeave(this, MouseEventArgs);
                }
            }
        }        

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

                Border.MouseEventArgs = value;

                if (getRegion().Contains(value.Location))
                {
                    
                    if (!MouseEntered)
                    {                                                
                        MouseEntered = true;                        
                    }
                }
                else
                {                    
                    if (MouseEntered)
                    {                                               
                        MouseEntered = false;
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
        public event EventHandler MouseEnterBorder = delegate { };
        public event EventHandler MouseLeaveBorder = delegate { };
        public event EventHandler MouseEnterBorderSide = delegate { };
        public event EventHandler MouseLeaveBorderSide = delegate { };

        public bool isEditable { get; set; }        

        public Selection(Point position, int width, int height, double scale)
        {
            MiddlePointPosition = position;
            Size = new Size(width, height);
            Scale = scale;
            isEditable = true;            
        }
        public Selection(Point position) : this(position, 0, 0, 1) { }

        private void BorderChanged()
        {
            Point TopLeft = MidPoint2TopLeft();
            int Thick = 40;
            Border.Sides[BorderSides.Top].Region = new Rectangle(TopLeft, new Size(Width, Thick));
            Border.Sides[BorderSides.Right].Region = new Rectangle(TopLeft.X + Width - Thick, TopLeft.Y, Thick, Height);
            Border.Sides[BorderSides.Bottom].Region = new Rectangle(TopLeft.X, TopLeft.Y + Height - Thick, Width, Thick);
            Border.Sides[BorderSides.Left].Region = new Rectangle(TopLeft, new Size(Thick, Height));            
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseEnterBorder(sender, e);
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseLeaveBorder(sender, e);
        }

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

        public void RealSizeRecalc(Size size)
        {
            RealSize = new Size(
                (int)(Size.Width / Scale),
                (int)(Size.Height / Scale)
            );
            Size = size;
        }
        private void SizeRecalc()
        {
            Size = new Size(
                (int)(RealSize.Width * Scale), 
                (int)(RealSize.Height * Scale)
            );
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

                            foreach (BorderSide item in Border.Sides.Values)
                            {
                                if (item.MouseEntered)
                                {
                                    g.FillRectangle(Brushes.Lime, item.Region);
                                }
                                g.DrawRectangle(pen, item.Region);
                            }                            
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
        public Rectangle getRegionReal()
        {
            return new Rectangle(
                new Point(
                    1 + MiddlePointRealPosition.X - RealSize.Width / 2,
                    1 + MiddlePointRealPosition.Y - RealSize.Height / 2
                ),
                RealSize
            );
        }        
    }
}
