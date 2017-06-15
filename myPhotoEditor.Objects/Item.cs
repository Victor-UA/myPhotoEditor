using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{
    public abstract class Item
    {
        public Point Location
        {
            get
            {
                return MidPoint2TopLeft();
            }
        }
        public Point MiddlePointPosition {
            get
            {
                return new Point(
                    (int)(MiddlePointRealPosition.X * Scale) - Offset.X,
                    (int)(MiddlePointRealPosition.Y * Scale) - Offset.Y
                );
            }
            set
            {
                MiddlePointRealPosition = new Point(
                    (int)((value.X + Offset.X) / Scale),
                    (int)((value.Y + Offset.Y) / Scale)
                );
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
                Border.Change(MidPoint2TopLeft(), Width, Height);
                LocationChanged(this, new EventArgs());
            }
        }
        private Point OldPosition { get; set; }
        private Point _Offset;
        public Point Offset
        {
            get
            {
                return _Offset;
            }

            set
            {
                _Offset = value;
                Border.Change(MidPoint2TopLeft(), Width, Height);
            }
        }
        private Sensor _Sensor;
        public Sensor Sensor
        {
            get
            {
                return _Sensor;
            }

            set
            {
                _Sensor = value;
            }
        }
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
                        Sensor.Cursor = Sensor.MyCursor;
                    }
                }

            }
        }

        private bool MouseDownInside { get; set; }
        private Point MouseLeftButtonDownPosition { get; set; }

        public Size Size
        {
            get
            {
                return new Size(
                    (int)(RealSize.Width * Scale),
                    (int)(RealSize.Height * Scale)
                );
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
                if (value.Width >= 0 && value.Height >= 0)
                {
                    _RealSize = value;
                }
                Border.Change(MidPoint2TopLeft(), Width, Height);
                SizeChanged(this, new EventArgs());
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
                RealSize = new Size(value, _RealSize.Height);
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
                RealSize = new Size(RealSize.Width, value);
            }
        }
        private double _Scale;
        public double Scale
        {
            get
            {
                return _Scale;
            }

            internal set
            {
                _Scale = value;
                Border.Change(MidPoint2TopLeft(), Width, Height);
            }
        }

        public Border Border { get; private set; }

        private ItemStates _State;
        public ItemStates State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = value;
            }
        }
        
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
                        Sensor.Cursor = BlockedCursor;
                        MouseEnter(this, MouseEventArgs);
                    }
                    else
                    {
                        if (!CursorIsBlocked)
                        {
                            Sensor.Cursor = Sensor.MyCursor;
                        }
                        else
                        {
                            Sensor.Cursor = BlockedCursor;
                        }
                        MouseLeave(this, MouseEventArgs);
                    }
                }
                if (value && !Border.MouseEntered && !CursorIsBlocked)
                {
                    Sensor.Cursor = Cursors.Hand;
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
        protected Dictionary<MouseButtons, MouseButtonStates> MouseButtonsState { get; set; }        

        public bool MouseDownInsideBorder { get; private set; }

        public Item(Point position, int width, int height, Sensor sensor)
        {
            Border = new Border();
            Border.MouseEnterBorderSide += Border_Side_MouseEnter;
            Border.MouseLeave += Border_MouseLeave;

            Sensor = sensor;
            Sensor.Items.Add(this);
            _Scale = Sensor.ImageScale;
            Offset = Sensor.Location;
            MouseButtonsState = Sensor.MouseButtonsState;

            MiddlePointPosition = position;
            RealSize = new Size(width, height);
            State = ItemStates.Creating;            
            _CursorIsBlocked = false;
        }
        public Item(Point position, Sensor sensor) 
            : this(position, 0, 0, sensor) { }


        public void MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("MouseDown");
            if (MouseButtonsState[MouseButtons.Left].State)
            {
                MouseLeftButtonDownPosition = e.Location;
                OldPosition = MiddlePointPosition;
                oldRealSize = RealSize;
                MouseDownInside = getRegion().Contains(MouseLeftButtonDownPosition);
                MouseDownInsideBorder = Border.Contains(MouseLeftButtonDownPosition);
                ResizingSide = Border.ActiveSide;
                Debug.WriteLine(MouseDownInside);
            }
        }
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseButtonsState[MouseButtons.Left].Moving = false;
                if (Array.Exists(new ItemStates[] 
                    {
                        ItemStates.Moving,
                        ItemStates.Resizing
                    }, item => item == State))
                {
                    State = ItemStates.Normal;
                }
            }

            bool anyMove = false;
            foreach (var item in MouseButtonsState.Values)
            {
                anyMove |= item.Moving;
                if (anyMove)
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
                if (State == ItemStates.Creating)
                {
                    State = ItemStates.Normal;
                    CursorIsBlocked = false;
                }
                else
                {
                    //if (!MouseDownInside &&
                    //    !(Array.Exists(new ItemStates[] 
                    //    {
                    //        ItemStates.Moving,
                    //        ItemStates.Resizing
                    //    }, item => item == State))
                    //)
                    //{
                    //    State = ItemStates.Creating;
                    //    CursorIsBlocked = true;

                    //    RealSizeRecalc(Size.Empty);
                    //    MiddlePointPosition = e.Location;                        
                    //}
                }
            }
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {
            MouseEntered = getRegion().Contains(e.Location);
            Border.MouseEventArgs = e;
            if (MouseButtonsState[MouseButtons.Left].State)
            {
                MouseButtonsState[MouseButtons.Left].Moving = true;
                CursorIsBlocked = true;
                if (MouseDownInside)
                {
                    if (!MouseDownInsideBorder)
                    {
                        if (State != ItemStates.Moving)
                        {
                            State = ItemStates.Moving;
                        }
                    }
                    else
                    {
                        if (State != ItemStates.Resizing)
                        {
                            if (ResizingSide != BorderSides.None)
                            {
                                State = ItemStates.Resizing;
                            }
                        }
                    }

                }
                if (State == ItemStates.Moving)
                {
                    MiddlePointPosition = new Point(
                        OldPosition.X + (e.X - MouseLeftButtonDownPosition.X),
                        OldPosition.Y + (e.Y - MouseLeftButtonDownPosition.Y)
                    );                    
                }
                if (State == ItemStates.Resizing)
                {
                    switch (ResizingSide)
                    {
                        case BorderSides.TopLeft:
                            RealSize = new Size(
                                oldRealSize.Width - (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2),
                                oldRealSize.Height - (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2)
                            );
                            break;
                        case BorderSides.Top:
                            RealHeight = oldRealSize.Height - (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.TopRight:                            
                            RealSize = new Size(
                                oldRealSize.Width + (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2),
                                oldRealSize.Height - (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2)
                            );                            
                            break;
                        case BorderSides.Right:
                            RealWidth = oldRealSize.Width + (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);                            
                            break;
                        case BorderSides.BottomRight:
                            RealSize = new Size(
                                oldRealSize.Width + (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2),
                                oldRealSize.Height + (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2)
                            );                            
                            break;
                        case BorderSides.Bottom:
                            RealHeight = oldRealSize.Height + (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2);
                            break;
                        case BorderSides.BottomLeft:
                            RealSize = new Size(
                                oldRealSize.Width - (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2),
                                oldRealSize.Height + (int)((e.Y - MouseLeftButtonDownPosition.Y) / Scale * 2)
                            );
                            break;
                        case BorderSides.Left:
                            RealWidth = oldRealSize.Width - (int)((e.X - MouseLeftButtonDownPosition.X) / Scale * 2);
                            break;
                        case BorderSides.None:
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if (State == ItemStates.Creating)
                {
                    RealSize = new Size(
                        (int)(Math.Abs(e.X - MiddlePointPosition.X) * 2 / Scale),
                        (int)(Math.Abs(e.Y - MiddlePointPosition.Y) * 2 / Scale)
                    );
                }
            }            
        }

        public event EventHandler SizeChanged = delegate { };
        public event EventHandler LocationChanged = delegate { };        
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
                        Sensor.Cursor = Cursors.SizeNWSE;
                        break;
                    case BorderSides.Top:
                        Sensor.Cursor = Cursors.SizeNS;
                        break;
                    case BorderSides.TopRight:
                        Sensor.Cursor = Cursors.SizeNESW;
                        break;
                    case BorderSides.Right:
                        Sensor.Cursor = Cursors.SizeWE;
                        break;
                    case BorderSides.BottomRight:
                        Sensor.Cursor = Cursors.SizeNWSE;
                        break;
                    case BorderSides.Bottom:
                        Sensor.Cursor = Cursors.SizeNS;
                        break;
                    case BorderSides.BottomLeft:
                        Sensor.Cursor = Cursors.SizeNESW;
                        break;
                    case BorderSides.Left:
                        Sensor.Cursor = Cursors.SizeWE;
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

        public Point MidPoint2TopLeft()
        {
            return new Point()
            {
                X = (int)(((double)MiddlePointPosition.X - Size.Width / 2)),
                Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2))
            };
        }
        public Point TopLeft2MidPoint()
        {
            return new Point()
            {
                X = (int)(((double)Location.X + Size.Width / 2)),
                Y = (int)(((double)Location.Y + Size.Height / 2))
            };
        }        

        public abstract void Draw(Image image);

        public Rectangle getRegion()
        {            
            return new Rectangle(
                new Point(
                    MiddlePointPosition.X - Size.Width / 2,
                    MiddlePointPosition.Y - Size.Height / 2
                ), 
                Size
            );
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
