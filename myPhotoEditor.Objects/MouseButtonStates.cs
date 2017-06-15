using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor
{
    public class MouseButtonStates
    {
        public bool State { get; private set; }
        public bool Moving;
        public Point Location;        

        public MouseButtonStates()
        {
            State = false;
            Moving = false;
            Location = Point.Empty;
        }

        public void setState(bool state, Point location)
        {
            State = state;
            Location = location;
        }
    }
}
