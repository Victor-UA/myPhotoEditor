using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{
    public class Sensor : PictureBox
    {
        public Dictionary<MouseButtons, MouseButtonStates> MouseButtonsState { get; private set; }
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
        private double _ImageScale;
        public double ImageScale
        {
            get
            {
                return _ImageScale;
            }
            set
            {
                _ImageScale = value;
                foreach (var item in Items)
                {
                    item.Scale = value;
                }
            }
        }

        
        public ItemsList Items { get; private set; }

        public Sensor() : base()
        {
            Cursor = base.Cursor;
            MouseButtonsState = new Dictionary<MouseButtons, MouseButtonStates>()
            {
                { MouseButtons.Left, new MouseButtonStates() },
                { MouseButtons.Middle, new MouseButtonStates() },
                { MouseButtons.Right, new MouseButtonStates() },
                { MouseButtons.XButton1, new MouseButtonStates() },
                { MouseButtons.XButton2, new MouseButtonStates()  }
            };
            Items = new ItemsList();
            ImageScale = 0;
            MouseMove += ((object sender, MouseEventArgs e) =>
            {
                foreach (var item in Items)
                {
                    try
                    {
                        item.MouseMove(sender, e);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            MouseDown += ((object sender, MouseEventArgs e) =>
            {
                MouseButtonsState[e.Button].State = true;
                foreach (var item in Items)
                {
                    try
                    {
                        item.MouseDown(sender, e);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            MouseUp += ((object sender, MouseEventArgs e) =>
            {
                MouseButtonsState[e.Button].State = false;
                foreach (var item in Items)
                {
                    try
                    {
                        item.MouseUp(sender, e);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            MouseClick += ((object sender, MouseEventArgs e) =>
            {
                foreach (var item in Items)
                {
                    try
                    {
                        item.MouseClick(sender, e);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            LocationChanged += ((object sender, EventArgs e) => 
            {
                foreach (var item in Items)
                {
                    try
                    {
                        item.Offset = Location;
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }

        public void Draw()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            foreach (var item in Items)
            {
                try
                {
                    item.Draw(bitmap);
                }
                catch (Exception)
                {
                }
            }
            BackgroundImage = bitmap;
            Refresh();
        }

    }
}
