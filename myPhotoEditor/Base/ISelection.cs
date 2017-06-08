﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor.Base
{
    interface ISelection
    {        
        Point MiddlePointPosition { get; set; }
        Size Size { get; set; }        
        
        bool isEditable { get; set; }

        event EventHandler<EventArgs> Changed;

        void Draw(Image image, SelectionStyle style);
        Rectangle getRegion(double scale, Point offset);
        Rectangle getRegion(double scale);
    }
}
