using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{
    public class Selection : Item
    {
        private SelectionStyles _SelectionStyle;
        public SelectionStyles SelectionStyle
        {
            get
            {
                return _SelectionStyle;
            }

            set
            {
                _SelectionStyle = value;
                SelectionStyleChanged(this, new EventArgs());
            }
        }

        public Selection(Point position, Dictionary<MouseButtons, MouseButtonStates> mouseButtonsState, Sensor sensor) : base(position, 0, 0, 1, mouseButtonsState, sensor) { }


        public event EventHandler SelectionStyleChanged = delegate { };


        public new void MouseDown(object sender, MouseEventArgs e)
        {
            base.MouseDown(sender, e);
        }
        public new void MouseUp(object sender, MouseEventArgs e)
        {
            base.MouseUp(sender, e);
        }
        public new void MouseClick(object sender, MouseEventArgs e)
        {
            base.MouseClick(sender, e);
        }
        public new void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
        }        

        public override void Draw(Image image)
        {
            Graphics g = null;
            Graphics gImage = null;
            try
            {
                g = Graphics.FromImage(image);
                Pen pen = new Pen(Brushes.Lime, 1);                
                {
                    switch (SelectionStyle)
                    {
                        case SelectionStyles.BoxDiagonal:
                            g.DrawLine(pen, Location.X, Location.Y, Location.X + Width, Location.Y + Height);
                            g.DrawLine(pen, Location.X, Location.Y + Height, Location.X + Width, Location.Y);
                            g.DrawRectangle(pen, Location.X, Location.Y, Width, Height);
                            break;
                        default: //SelectionStyle.BoxMiddleOrthoAxis
                            g.DrawLine(pen, Location.X, Location.Y + Height / 2, Location.X + Width, Location.Y + Height / 2);
                            g.DrawLine(pen, Location.X + Width / 2, Location.Y, Location.X + Width / 2, Location.Y + Height);
                            g.DrawRectangle(pen, Location.X, Location.Y, Width, Height);

                            foreach (BorderSide item in Border.Sides.Values)
                            {
                                if (item.MouseEntered)
                                {
                                    g.FillRectangle(Brushes.Lime, item.Region);
                                }
                                g.DrawRectangle(pen, item.Region);
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\r" + ex.StackTrace);
            }
            finally
            {
                if (gImage != null)
                    gImage.Dispose();
                if (g != null)
                    g.Dispose();
            }
        }
    }
}
