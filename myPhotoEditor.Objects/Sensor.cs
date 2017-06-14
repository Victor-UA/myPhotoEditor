using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{
    public class Sensor : PictureBox
    {
        private Cursor _myCursor;
        public Cursor MyCursor
        {
            get
            {
                return _myCursor;
            }

            private set
            {
                _myCursor = value;
                Cursor = value;
            }
        }

        public Sensor() : base()
        {
            Cursor = base.Cursor;
        }
    }
}
