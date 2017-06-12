using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{
    public class Border
    {
        public Dictionary<BorderSides, BorderSide> Sides { get; private set; }        

        public Border()
        {
            Sides = new Dictionary<BorderSides, BorderSide>();
            
            SideInit(BorderSides.Top, new BorderSide(BorderSides.Top));
            SideInit(BorderSides.Right, new BorderSide(BorderSides.Right));
            SideInit(BorderSides.Bottom, new BorderSide(BorderSides.Bottom));
            SideInit(BorderSides.Left, new BorderSide(BorderSides.Left));

            _MouseEntered = false;
        }

        private void SideInit(BorderSides eSide, BorderSide side)
        {
            Sides.Add(eSide, side);
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
                        MouseEnterBorder(this, new EventArgs());
                    }
                    else
                    {
                        MouseLeaveBorder(this, new EventArgs());
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
                bool result = false;
                foreach (BorderSide item in Sides.Values)
                {
                    item.MouseEventArgs = value;
                    result |= item.MouseEntered;
                }
                MouseEntered = result;
            }
        }


        public event EventHandler MouseEnterBorder = delegate { };
        public event EventHandler MouseLeaveBorder = delegate { };        
    }
}