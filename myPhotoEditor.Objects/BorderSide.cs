using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static myPhotoEditor.Objects.Selection;

namespace myPhotoEditor.Objects
{
    public class BorderSide
    {
        public BorderSides Side { get; private set; }
        public Rectangle Region { get; internal set; }
        private bool _MouseEntered;
        public bool MouseEntered
        {
            get
            {
                return _MouseEntered;
            }
            internal set
            {
                if (value != _MouseEntered)
                {
                    _MouseEntered = value;
                    if (value)
                    {
                        MouseEnterBorderSide(this, new EventArgs());
                    }
                    else
                    {
                        MouseLeaveBorderSide(this, new EventArgs());
                    }
                }
                else
                    _MouseEntered = value;
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
                MouseEntered = Region.Contains(value.Location);                
            }
        }        

        public BorderSide(BorderSides side)
        {
            Side = side;
            MouseEntered = false;
            Region = Rectangle.Empty;
        }

        public event EventHandler MouseEnterBorderSide = delegate { };
        public event EventHandler MouseLeaveBorderSide = delegate { };
    }
}
