using System;
using System.Drawing;
using System.Windows.Forms;

namespace myPhotoEditor.Objects
{
    public interface ISelection
    {
        Border Border { get; }
        bool CursorIsBlocked { get; set; }
        int Height { get; }
        bool isMoving { get; }
        bool isResizing { get; set; }
        bool isSizing { get; set; }
        Point Location { get; }
        Point MiddlePointPosition { get; set; }
        Point MiddlePointRealPosition { get; set; }
        bool MouseEntered { get; }
        MouseEventArgs MouseEventArgs { set; }
        Point Offset { get; set; }
        Control Parent { get; set; }
        int RealHeight { get; set; }
        int RealWidth { get; set; }
        double Scale { get; set; }
        SelectionStyle SelectionStyle { get; set; }
        Size Size { get; }
        int Width { get; }

        event EventHandler LocationChanged;
        event MouseEventHandler MouseEnter;
        event EventHandler MouseEnterBorder;
        event EventHandler MouseEnterBorderSide;
        event MouseEventHandler MouseLeave;
        event EventHandler MouseLeaveBorder;
        event EventHandler MouseLeaveBorderSide;
        event EventHandler SelectionStyleChanged;
        event EventHandler SizeChanged;

        void Draw(Image image);
        Rectangle getRegion();
        Rectangle getRegion(Point offset);
        Rectangle getRegionReal();
        Rectangle getRegionReal(double scale);
        Rectangle getRegionReal(double scale, Point offset);
        void MouseClick(object sender, MouseEventArgs e);
        void MouseDown(object sender, MouseEventArgs e);
        void MouseMove(object sender, MouseEventArgs e);
        void MouseUp(object sender, MouseEventArgs e);
        void RealSizeRecalc(Size size);
    }
}