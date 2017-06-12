using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor.Objects
{
    public class BorderSide
    {
        public bool MouseEntered { get; internal set; } = false;
        public Rectangle Region { get; internal set; }        
    }
}
