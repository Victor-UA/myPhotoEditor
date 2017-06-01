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
    }
}
