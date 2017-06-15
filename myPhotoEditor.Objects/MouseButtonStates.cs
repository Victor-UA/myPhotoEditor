using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor
{
    public class MouseButtonStates
    {
        public bool State;        
        public bool Moving;

        public MouseButtonStates()
        {
            State = false;
            Moving = false;
        }
    }
}
