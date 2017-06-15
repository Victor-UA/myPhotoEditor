using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{
    public class Border
    {
        public Dictionary<BorderSides, BorderSide> Sides { get; private set; }
        internal BorderSides ActiveSide
        {
            get
            {
                foreach (var item in Sides.Values)
                {
                    if (item.MouseEntered)
                    {
                        return item.Side;
                    }
                }
                return BorderSides.None;

            }
        }
        private int Thick;        

        public Border()
        {
            Thick = 10;
            Sides = new Dictionary<BorderSides, BorderSide>() {
                { BorderSides.TopLeft, new BorderSide(BorderSides.TopLeft) },
                { BorderSides.Top, new BorderSide(BorderSides.Top) },
                { BorderSides.TopRight, new BorderSide(BorderSides.TopRight) },
                { BorderSides.Right, new BorderSide(BorderSides.Right) },
                { BorderSides.BottomRight, new BorderSide(BorderSides.BottomRight) },
                { BorderSides.Bottom, new BorderSide(BorderSides.Bottom) },
                { BorderSides.BottomLeft, new BorderSide(BorderSides.BottomLeft) },
                { BorderSides.Left, new BorderSide(BorderSides.Left) }
            };
            foreach (BorderSide item in Sides.Values)
            {
                item.MouseEnter += new EventHandler(delegate (object sender, EventArgs e) { MouseEnterBorderSide(sender, e); });
                item.MouseLeave += new EventHandler(delegate (object sender, EventArgs e) { MouseLeaveBorderSide(sender, e); });
            }

            _MouseEntered = false;
        }

        private bool _MouseEntered;
        public bool MouseEntered
        {
            get
            {                
                return _MouseEntered;
            }
            set
            {
                if (value != MouseEntered)
                {
                    _MouseEntered = value;
                    if (value)
                    {
                        MouseEnter(this, new EventArgs());
                    }
                    else
                    {
                        MouseLeave(this, new EventArgs());
                    }
                }
                else 
                    _MouseEntered = value;
            }
        }

        internal void Change(Point TopLeft, int Width, int Height)
        {
            if (Width < Thick * 2 || Height < Thick * 2)
            {
                Sides[BorderSides.TopLeft].Region = new Rectangle(TopLeft, Size.Empty);
                Sides[BorderSides.Top].Region = new Rectangle(TopLeft, Size.Empty);
                Sides[BorderSides.TopRight].Region = new Rectangle(TopLeft, Size.Empty);
                Sides[BorderSides.Right].Region = new Rectangle(TopLeft, Size.Empty);
                Sides[BorderSides.BottomRight].Region = new Rectangle(TopLeft, Size.Empty);
                Sides[BorderSides.Bottom].Region = new Rectangle(TopLeft, Size.Empty); 
                Sides[BorderSides.BottomLeft].Region = new Rectangle(TopLeft, Size.Empty);
                Sides[BorderSides.Left].Region = new Rectangle(TopLeft, Size.Empty);
            }
            else
            {
                Sides[BorderSides.TopLeft].Region = new Rectangle(TopLeft, new Size(Thick, Thick));
                Sides[BorderSides.Top].Region = new Rectangle(TopLeft.X + Thick, TopLeft.Y, Width - Thick * 2, Thick);
                Sides[BorderSides.TopRight].Region = new Rectangle(TopLeft.X + Width - Thick, TopLeft.Y, Thick, Thick);
                Sides[BorderSides.Right].Region = new Rectangle(TopLeft.X + Width - Thick, TopLeft.Y + Thick, Thick, Height - Thick * 2);
                Sides[BorderSides.BottomRight].Region = new Rectangle(TopLeft.X + Width - Thick, TopLeft.Y + Height - Thick, Thick, Thick);
                Sides[BorderSides.Bottom].Region = new Rectangle(TopLeft.X + Thick, TopLeft.Y + Height - Thick, Width - Thick * 2, Thick);
                Sides[BorderSides.BottomLeft].Region = new Rectangle(TopLeft.X, TopLeft.Y + Height - Thick, Thick, Thick);
                Sides[BorderSides.Left].Region = new Rectangle(TopLeft.X, TopLeft.Y + Thick, Thick, Height - Thick * 2);
            }
        }

        private MouseEventArgs _MouseEventArgs;
        public MouseEventArgs MouseEventArgs
        {
            get
            {
                return _MouseEventArgs;
            }

            set
            {
                _MouseEventArgs = value;
                bool result = false;
                foreach (BorderSide item in Sides.Values)
                {
                    item.MouseEventArgs = value;
                    result |= item.MouseEntered;
                }
                MouseEntered = result;
            }
        }

        internal bool Contains(Point point)
        {
            bool result = false;
            foreach (BorderSide item in Sides.Values)
            {
                result |= item.Region.Contains(point);
            }
            return result;
        }        

        public event EventHandler MouseEnter = delegate { };
        public event EventHandler MouseLeave = delegate { };
        public event EventHandler MouseEnterBorderSide = delegate { };
        public event EventHandler MouseLeaveBorderSide = delegate { };
    }
}