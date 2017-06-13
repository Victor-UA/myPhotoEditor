using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPhotoEditor.Main
{
    public static class MouseButtonsState
    {
        public static Dictionary<MouseButtons, bool> State = new Dictionary<MouseButtons, bool>()
        {
            { MouseButtons.Left, false },
            { MouseButtons.Middle, false },
            { MouseButtons.Right, false },
            { MouseButtons.XButton1, false },
            { MouseButtons.XButton2, false }
        };
    }
}
