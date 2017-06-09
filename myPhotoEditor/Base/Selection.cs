﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor.Base
{    
    class Selection : ISelection
    {      
        private Point _middlePointPosition;
        public Point MiddlePointPosition {
            get
            {
                return _middlePointPosition;
            }
            set
            {
                _middlePointPosition = value;
                Changed(this, new EventArgs());
            }
        }
        private Size _Size;
        public Size Size
        {
            get
            {
                return _Size;
            }

            set
            {
                _Size = value;
                Changed(this, new EventArgs());
            }
        }
        
        public bool isEditable { get; set; }        

        public event EventHandler<EventArgs> Changed = delegate { };


        public Selection(Point position, int width, int height)
        {
            MiddlePointPosition = position;
            Size = new Size(width, height);
            isEditable = true;
        }
        public Selection(Point position) : this(position, 0, 0) { }

        public void Draw(Image image, SelectionStyle style)
        {
            Graphics g = null;
            Graphics gImage = null;
            try
            {               
                Bitmap bitmap = new Bitmap(Size.Width + 1, Size.Height + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bitmap.MakeTransparent();
                g = Graphics.FromImage(bitmap);
                Pen pen = new Pen(Brushes.Lime, 1);
                {
                    switch (style)
                    {
                        case SelectionStyle.BoxMiddleOrthoAxis:
                            g.DrawLine(pen, 0, Size.Height / 2, Size.Width, Size.Height / 2);
                            g.DrawLine(pen, Size.Width / 2, 0, Size.Width / 2, Size.Height);
                            g.DrawRectangle(pen, 0, 0, Size.Width, Size.Height);
                            break;                     
                        case SelectionStyle.BoxDiagonal:
                            g.DrawLine(pen, 0, 0, Size.Width, Size.Height);
                            g.DrawLine(pen, 0, Size.Height, Size.Width, 0);
                            g.DrawRectangle(pen, 0, 0, Size.Width, Size.Height);
                            break;
                        default:
                            break;
                    }
                    
                }
                gImage = Graphics.FromImage(image); 
                gImage.DrawImage(bitmap, MiddlePointPosition.X - Size.Width / 2, MiddlePointPosition.Y - Size.Height / 2);
                
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

        public Rectangle getRegion(Point offset)
        {
            try
            {
                Point TopLeft = new Point()
                {
                    X = (int)(((double)MiddlePointPosition.X - Size.Width / 2 + offset.X)) + 1,
                    Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2 + offset.Y)) + 1
                };
                return new Rectangle(TopLeft, Size);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Rectangle getRegion()
        {
            return getRegion(Point.Empty);
        }
        public Rectangle getRegionReal(double scale, Point offset)
        {
            try
            {
                Point TopLeft = new Point()
                {
                    X = (int)(((double)MiddlePointPosition.X - Size.Width / 2 + offset.X) / scale) + 1,
                    Y = (int)(((double)MiddlePointPosition.Y - Size.Height / 2 + offset.Y) / scale) + 1
                };

                if (scale == 1)
                {
                    return new Rectangle(TopLeft, Size);
                }
                else
                {
                    Size size = new Size((int)((Size.Width) / scale), (int)((Size.Height) / scale));
                    return new Rectangle(TopLeft, size);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Rectangle getRegionReal(double scale)
        {
            return getRegionReal(scale, Point.Empty);
        }
    }
}
