﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
                Border.Change(MidPoint2TopLeft(), Width, Height);
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
        private Point OldPosition { get; set; }
        public Point Offset { get; set; }
        private Control _Parent;
        public Control Parent
        {
            get
            {
                return _Parent;
            }

            set
            {
                _Parent = value;
            }
        }
        private Cursor oldParentCursor { get; set; }
        private Cursor BlockedCursor { get; set; }
        private bool _CursorIsBlocked;
        public bool CursorIsBlocked
        {
            get
            {
                return _CursorIsBlocked;
            }

            set
            {
                _CursorIsBlocked = value;
                if (value)
                {
                    BlockedCursor = Cursor.Current;
                }
                else
                {
                    if (!MouseEntered)
                    {
                        Parent.Cursor = oldParentCursor;
                    }
                }
                
            }
        }

        bool MouseDownInside;

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
                Border.Change(MidPoint2TopLeft(), Width, Height);
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
        private Size RealSize
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
        private bool ResizeBlocked { get; set; }
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
                    _RealSize = new Size(value, _RealSize.Height);
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
                    _RealSize = new Size(RealSize.Width, value);
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
        public Border Border { get; private set; }

        private bool _isMoving;
        public bool isMoving
        {
            get
            {
                return _isMoving;
            }
            protected set
            {
                _isMoving = value;                
            }
        }
        public bool isSizing { get; set; }
        public bool isResizing { get; set; }
        private BorderSides ResizingSide { get; set; }
        private Size oldRealSize { get; set; }        

        private bool _MouseEntered;
        public bool MouseEntered
        {
            get
            {
                return _MouseEntered;
            }
            private set
            {
                if (_MouseEntered != value)
                {
                    _MouseEntered = value;
                    if (value)
                    {
                        if (!CursorIsBlocked)
                        {
                            oldParentCursor = Parent.Cursor;
                        }
                        else
                        {
                            Parent.Cursor = BlockedCursor;
                        }
                        MouseEnter(this, MouseEventArgs);
                    }
                    else
                    {
                        if (!CursorIsBlocked)
                        {
                            Parent.Cursor = oldParentCursor;
                        }
                        else
                        {
                            Parent.Cursor = BlockedCursor;
                        }
                        MouseLeave(this, MouseEventArgs);
                    }
                }
                if (value && !Border.MouseEntered && !CursorIsBlocked)
                {
                    Parent.Cursor = Cursors.Hand;
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

                MouseEntered = getRegion().Contains(value.Location);                
                Border.MouseEventArgs = value;
            }
        }
        private Dictionary<MouseButtons, MouseButtonStates> MouseButtonsState { get; set; }
        private Point MouseLeftButtonDownPosition { get; set; }

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

        public bool MouseDownInsideBorder { get; private set; }

        public Selection(Point position, int width, int height, double scale, Dictionary<MouseButtons, MouseButtonStates> mouseButtonsState, Control parent)
        {
            MouseButtonsState = mouseButtonsState;
            Border = new Border();
            Border.MouseEnterBorderSide += Border_Side_MouseEnter;
            Border.MouseLeave += Border_MouseLeave;
            MiddlePointPosition = position;
            Size = new Size(width, height);
            ResizeBlocked = false;
            Scale = scale;
            isMoving = false;
            isSizing = false;
            isResizing = false;
            Offset = Point.Empty;
            Parent = parent;
            oldParentCursor = Parent.Cursor;
            CursorIsBlocked = false;
        }
        public Selection(Point position, Dictionary<MouseButtons, MouseButtonStates> mouseButtonsState, Control parent) : this(position, 0, 0, 1, mouseButtonsState, parent) { }


        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtonsState[MouseButtons.Left].State)
            {
                MouseLeftButtonDownPosition = e.Location;
                OldPosition = MiddlePointPosition;
                oldRealSize = RealSize;
                MouseDownInside = getRegion().Contains(MouseLeftButtonDownPosition);
                MouseDownInsideBorder = Border.Contains(MouseLeftButtonDownPosition);
                ResizingSide = Border.ActiveSide;
                Debug.WriteLine(string.Format("{0}, {1}, {2}", e.Location, MouseDownInside, MouseDownInsideBorder));
            }
        }
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseButtonsState[MouseButtons.Left].Move = false;
                isMoving = false;
                isResizing = false;
            }

            bool anyMove = false;
            foreach (var item in MouseButtonsState.Values)
            {
                if (anyMove |= item.Move)
                {                    
                    break;
                }
            }
            if (!anyMove)
            {
                CursorIsBlocked = false;
            }
        }
        public void MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseButtonsState[MouseButtons.Left].State)
            {
                if (isSizing)
                {
                    isSizing = false;
                    CursorIsBlocked = false;
                }
                else
                {
                    if (!isMoving && !isResizing)
                    {
                        isSizing = true;
                        CursorIsBlocked = true;

                        RealSizeRecalc(Size.Empty);
                        MiddlePointPosition = e.Location;
                        MiddlePointRealPosition = new Point(
                            (int)((e.X + Offset.X + 1) / Scale),
                            (int)((e.Y + Offset.Y + 1) / Scale)
                        );
                        
                    }
                }
            }
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtonsState[MouseButtons.Left].State)
            {
                if (MouseDownInside)
                {                    
                    if (!MouseButtonsState[MouseButtons.Left].Move)
                    {
                        if (!MouseDownInsideBorder)
                        {
                            if (!isMoving)
                            {
                                isMoving = true;
                            }
                        }
                        else
                        {
                            if (!isResizing)
                            {                                
                                if (ResizingSide != BorderSides.None)
                                    isResizing = true;
                            }
                        }
                    }
                    MouseButtonsState[MouseButtons.Left].Move = true;
                    CursorIsBlocked = true;
                }
                else
                {
                    if (MouseDownInside && MouseDownInsideBorder)
                        Debug.WriteLine("Error");
                }
                if (isMoving)
                {
                    MiddlePointPosition = new Point(
                        OldPosition.X + (e.X - MouseLeftButtonDownPosition.X),
                        OldPosition.Y + (e.Y - MouseLeftButtonDownPosition.Y)
                    );

                    MiddlePointRealPosition = new Point(
                        (int)((MiddlePointPosition.X + Offset.X) / Scale),
                        (int)((MiddlePointPosition.Y + Offset.Y) / Scale)
                    );
                }
                if (isResizing)
                {
                    ResizeBlocked = true;
                    switch (ResizingSide)
                    {
                        case BorderSides.TopLeft:
                            
                            RealWidth = oldRealSize.Width - (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);
                            RealHeight = oldRealSize.Height - (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.Top:
                            RealHeight = oldRealSize.Height - (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.TopRight:
                            RealWidth = oldRealSize.Width + (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);
                            RealHeight = oldRealSize.Height - (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.Right:
                            RealWidth = oldRealSize.Width + (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);                            
                            break;
                        case BorderSides.BottomRight:
                            RealWidth = oldRealSize.Width + (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);
                            RealHeight = oldRealSize.Height + (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.Bottom:
                            RealHeight = oldRealSize.Height + (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.BottomLeft:
                            RealWidth = oldRealSize.Width - (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);
                            RealHeight = oldRealSize.Height + (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.Left:
                            RealWidth = oldRealSize.Width - (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);
                            break;
                        case BorderSides.None:
                            break;
                        default:
                            break;
                    }
                    ResizeBlocked = false;
                    SizeRecalc();                    
                }
            }
            else
            {
                if (isSizing)
                {
                    RealSizeRecalc(new Size(
                        Math.Abs(e.X - MiddlePointPosition.X) * 2,
                        Math.Abs(e.Y - MiddlePointPosition.Y) * 2
                    ));
                }
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

        private void Border_Side_MouseEnter(object sender, EventArgs e)
        {
            if (!CursorIsBlocked)
            {
                switch ((sender as BorderSide).Side)
                {
                    case BorderSides.TopLeft:
                        Parent.Cursor = Cursors.SizeNWSE;
                        break;
                    case BorderSides.Top:
                        Parent.Cursor = Cursors.SizeNS;
                        break;
                    case BorderSides.TopRight:
                        Parent.Cursor = Cursors.SizeNESW;
                        break;
                    case BorderSides.Right:
                        Parent.Cursor = Cursors.SizeWE;
                        break;
                    case BorderSides.BottomRight:
                        Parent.Cursor = Cursors.SizeNWSE;
                        break;
                    case BorderSides.Bottom:
                        Parent.Cursor = Cursors.SizeNS;
                        break;
                    case BorderSides.BottomLeft:
                        Parent.Cursor = Cursors.SizeNESW;
                        break;
                    case BorderSides.Left:
                        Parent.Cursor = Cursors.SizeWE;
                        break;
                    default:
                        break;
                }
            }

            MouseEnterBorder(sender, e);
        }
        private void Border_MouseLeave(object sender, EventArgs e)
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
            _RealSize = new Size(
                (int)(Size.Width / Scale),
                (int)(Size.Height / Scale)
            );
            Size = size;
        }
        private void SizeRecalc()
        {
            if (!ResizeBlocked)
            {
                Size = new Size(
                    (int)(RealSize.Width * Scale),
                    (int)(RealSize.Height * Scale)
                );
            }
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
                            /*
                            foreach (BorderSide item in Border.Sides.Values)
                            {
                                if (item.MouseEntered)
                                {
                                    g.FillRectangle(Brushes.Lime, item.Region);
                                }
                                //g.DrawRectangle(pen, item.Region);
                            } 
                            */                           
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
                    X = (int)(((double)MiddlePointPosition.X - Size.Width / 2 + offset.X)),
                    Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2 + offset.Y))
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
                    MiddlePointRealPosition.X - RealSize.Width / 2,
                    MiddlePointRealPosition.Y - RealSize.Height / 2
                ),
                RealSize
            );
        }        
    }
}
