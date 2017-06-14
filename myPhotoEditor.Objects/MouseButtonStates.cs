using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor.Objects
{
    public class MouseButtonStates
    {
        public bool State;        
        public bool Move;

        public MouseButtonStates()
        {
            State = false;
            Move = false;
        }
    }
}
