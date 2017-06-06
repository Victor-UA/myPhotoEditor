using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using myPhotoEditor.Base;

namespace myPhotoEditor
{
    public partial class MainForm : Form
    {

        private bool ImageLoaded;
        private string OriginalImageFile;
        private Selection Selection;
        private Size Offset;
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
                tSSL_ImageScale.Text = Math.Round(value, 2).ToString();                
            }
        }
        private Point MiddleButtonDown;

        private Point Original_MousePosition { get; set; }
        private bool MouseInside { get; set; }


        public MainForm()
        {
            InitializeComponent();

            pb_Original.Controls.Add(pb_Selection);
            //pb_Selection.Size = pb_Original.Size;
            pb_Selection.Location = new Point(0, 0);
            pb_Selection.BackColor = Color.Transparent;
            pb_Selection.BorderStyle = BorderStyle.None;
            //pb_Selection.BringToFront();         
            
            splitContainer1.Panel1.MouseWheel += new MouseEventHandler(splitContainer1_Panel1_MouseWheel);

            OriginalImageFile = "";
            ImageLoaded = false;
            Selection = new Selection(new Point(0, 0))
            {                
                isEditable = false
            };
            Selection.Changed += SelectionChanged;
            Offset = Size.Empty;
            ImageScale = 1;
            Original_MousePosition = new Point();
            MouseInside = false;
        }

        private void Pb_Selection_MouseWheel(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Title = "Оберіть зображення",
                    AddExtension = true,
                    CheckFileExists = true,
                    Filter = "Images| *.jpg; *.png; *.bmp",
                    Multiselect = false                    
                };
                openFileDialog.ShowDialog();
                OriginalImageFile = openFileDialog.FileName;
                if (!string.IsNullOrWhiteSpace(OriginalImageFile))
                {
                    try
                    {
                        pb_Original.Load(OriginalImageFile);
                        pb_Original.Size = pb_Original.Image.Size;
                        pb_Original.Location = new Point(0, 0);
                        splitContainer1.Panel1.Focus();
                        ImageLoaded = true;
                        Selection.isEditable = false;
                        ImageScale = 1;
                    }
                    catch (Exception)
                    {
                        OriginalImageFile = "";
                        ImageLoaded = false;
                        Selection.isEditable = false;
                    }
                }
            }
            catch (Exception)
            {
                OriginalImageFile = "";                
            }
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            Size size = ((Selection)sender).Size;
            tSSL_SelectionWidth.Text = size.Width.ToString();
            tSSL_SelectionHeight.Text = size.Height.ToString();
        }

        private void SelectionReDraw()
        {
            try
            {
                Bitmap bitmap = new Bitmap(pb_Selection.Width, pb_Selection.Height, PixelFormat.Format32bppArgb);
                Selection.Draw(bitmap);
                pb_Selection.BackgroundImage = bitmap;
                pb_Selection.Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\r" + ex.StackTrace);
            }
        }
        private Bitmap CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion)
        {
            try
            {
                if (srcRegion.Width > 0 && srcRegion.Height > 0)
                {
                    Rectangle destRegion = new Rectangle(0, 0, srcRegion.Width, srcRegion.Height);
                    Bitmap destBitmap = new Bitmap(srcRegion.Width, srcRegion.Height, PixelFormat.Format32bppArgb);
                    using (Graphics grD = Graphics.FromImage(destBitmap))
                    {
                        grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
                    }
                    return destBitmap;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }        

        private void pb_Selection_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            tSSL_X.Text = e.X.ToString();
            tSSL_Y.Text = e.Y.ToString();
            Original_MousePosition = new Point(e.X, e.Y);

            if (mouse.Button == MouseButtons.Middle)
            {
                Point mousePosNow = mouse.Location;

                int deltaX = mousePosNow.X - MiddleButtonDown.X;
                int deltaY = mousePosNow.Y - MiddleButtonDown.Y;

                int newX = pb_Original.Location.X + deltaX;
                int newY = pb_Original.Location.Y + deltaY;

                pb_Original.Location = new Point(newX, newY);
            }
            else
            {
                
                if (Selection.isEditable && ImageLoaded)
                {
                    Selection.Size = new Size(
                        Math.Abs(e.X - Selection.MiddlePointPosition.X) * 2,
                        Math.Abs(e.Y - Selection.MiddlePointPosition.Y) * 2
                    );
                    SelectionReDraw();
                    CropImage();
                }
            }
        }

        private void CropImage()
        {
            try
            {
                Bitmap cropBitmap = CopyRegionIntoImage(new Bitmap(pb_Original.Image), Selection.getRegion(ImageScale));
                if (cropBitmap != null)
                {
                    pb_Crop.Image = cropBitmap;
                    pb_Crop.Refresh();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\r" + ex.StackTrace);
            }
        }

        private void pb_Selection_MouseLeave(object sender, EventArgs e)
        {
            MouseInside = false;
        }

        private void pb_Selection_MouseClick(object sender, MouseEventArgs e)
        {
            
            MouseEventArgs mouse = e as MouseEventArgs;
            if (ImageLoaded)
            {
                if (mouse.Button == MouseButtons.Left)
                {
                    if (!Selection.isEditable)
                    {                        
                        Selection.MiddlePointPosition = Original_MousePosition;
                        Selection.Size = new Size();
                    }
                    else
                    {
                        CropImage();
                    }
                    Selection.isEditable = !Selection.isEditable;
                }
                splitContainer1.Panel1.Focus();
            }
            
        }

        private void pb_Selection_MouseEnter(object sender, EventArgs e)
        {
            MouseInside = true;
        }

        private void pb_Selection_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            if (mouse.Button == MouseButtons.Middle)
            {
                MiddleButtonDown = mouse.Location;
            }
        }

        private void pb_Selection_MouseUp(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Middle)
            {
                Point mousePosNow = mouse.Location;

                int deltaX = mousePosNow.X - MiddleButtonDown.X;
                int deltaY = mousePosNow.Y - MiddleButtonDown.Y;

                int newX = pb_Original.Location.X + deltaX;
                int newY = pb_Original.Location.Y + deltaY;

                pb_Original.Location = new Point(newX, newY);
            }
        }
        

        private void pb_Selection_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            if (ImageLoaded)
            {
                if (mouse.Button == MouseButtons.Middle)
                {
                    pb_Original.Size = pb_Original.Image.Size;
                    pb_Selection.Size = pb_Original.Size;
                    splitContainer1.Panel1.HorizontalScroll.Value = 0;
                    splitContainer1.Panel1.VerticalScroll.Value = 0;
                    pb_Original.Location = new Point(0, 0);
                    ImageScale = 1;
                }
            }
        }

        private void splitContainer1_Panel1_DoubleClick(object sender, EventArgs e)
        {
            pb_Selection_DoubleClick(sender, e);
        }

        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Selection.isEditable)
                pb_Selection_MouseClick(sender, e);
        }
        private void splitContainer1_Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            int newWidth = pb_Original.Width,
                newHeight = pb_Original.Height,
                newX = pb_Original.Location.X,
                newY = pb_Original.Location.Y;

            double step = 0.1;
            double k = (e.Delta > 0) ? 1 + step : 1 - step;

            ImageScale *= k;

            newWidth = (int)(pb_Original.Image.Size.Width * ImageScale);
            newHeight = (int)(pb_Original.Image.Size.Height * ImageScale);
            if (newWidth < 32676 && newHeight < 32676)
            {

                newX = pb_Original.Location.X - (int)((e.X - pb_Original.Location.X) * (k - 1));
                newY = pb_Original.Location.Y - (int)((e.Y - pb_Original.Location.Y) * (k - 1));

                pb_Original.Size = new Size(newWidth, newHeight);
                pb_Original.Location = new Point(newX, newY);
            }
            else
            {
                ImageScale /= k;
            }
        }
    }
}
