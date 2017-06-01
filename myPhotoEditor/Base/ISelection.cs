using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor.Base
{
    interface ISelection
    {
        Point Position { get; set; }
        Size Size { get; set; }
        bool isEditable { get; set; }

        event EventHandler<EventArgs> Changed;
    }
}
