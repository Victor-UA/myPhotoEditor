using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using myPhotoEditor.Base;
using myPhotoEditor.Tools;

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
        private bool _Grayscale;
        private bool Grayscale
        {
            get
            {
                return _Grayscale;
            }
            set
            {
                _Grayscale = value;
                grayscaleToolStripMenuItem1.Checked = 
                    grayscaleToolStripMenuItem.Checked = _Grayscale;
            }
        }

        private Point Original_MousePosition { get; set; }
        private bool MouseInside { get; set; }


        public MainForm()
        {
            InitializeComponent();

            pb_Original.Controls.Add(pb_Selection);            
            pb_Selection.Size = splitContainer1.Panel1.ClientSize;
            pb_Selection.Location = new Point(0, 0);
            pb_Selection.BackColor = Color.Transparent;
            pb_Selection.BorderStyle = BorderStyle.None;
            pb_Selection.BringToFront();         
            
            splitContainer1.Panel1.MouseWheel += splitContainer1_Panel1_MouseWheel;

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



        //---------Menu---------
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Title = "Оберіть зображення",
                    AddExtension = true,
                    CheckFileExists = true,
                    Filter = "Images| *.jpeg; *.jpg; *.png; *.bmp",
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
                        Text = "myPhotoEditor: " + OriginalImageFile;
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
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = "Оберіть зображення",
                AddExtension = true,
                Filter = "Images| *.jpeg; *.jpg; *.png; *.bmp"
            };
            string[] filename = dialog.FileName.Split('.');
            string fileExtention = filename[filename.Length - 1];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imageFormat;
                switch (fileExtention.ToLower())
                {                    
                    case "png":
                        imageFormat = ImageFormat.Png;
                        break;
                    default:
                        imageFormat = ImageFormat.Jpeg;
                        break;
                }                
                pb_Crop.Image.Save(dialog.FileName, ImageFormat.Jpeg);
            }
        }
        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grayscale = !Grayscale;
            CropImage();
        }


        private void SelectionChanged(object sender, EventArgs e)
        {
            Size size = ((Selection)sender).Size;
            tSSL_SelectionWidth.Text = Math.Round((size.Width) / ImageScale).ToString();
            tSSL_SelectionHeight.Text = Math.Round((size.Height) / ImageScale).ToString();
        }

        private void SelectionReDraw()
        {
            try
            {
                Bitmap bitmap = new Bitmap(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height, PixelFormat.Format32bppArgb);
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
            Point mousePosition = new Point(e.X < -1 ? 65536 + e.X : e.X, e.Y < -1 ? 65536 + e.Y : e.Y);
            tSSL_X.Text = Math.Round((mousePosition.X + pb_Selection.Location.X) / ImageScale).ToString();
            tSSL_Y.Text = Math.Round((mousePosition.Y + pb_Selection.Location.Y) / ImageScale).ToString();
            Original_MousePosition = new Point(mousePosition.X, mousePosition.Y);

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
                        Math.Abs(mousePosition.X - Selection.MiddlePointPosition.X) * 2,
                        Math.Abs(mousePosition.Y - Selection.MiddlePointPosition.Y) * 2
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
                Bitmap cropBitmap = CopyRegionIntoImage(new Bitmap(pb_Original.Image), Selection.getRegion(ImageScale, pb_Selection.Location));
                if (cropBitmap != null)
                {
                    if (grayscaleToolStripMenuItem.Checked)
                        pb_Crop.Image = ImageTools.MakeGrayscale3(cropBitmap);
                    else
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
                pb_Selection.Hide();
                Point oldPBSelectionLocation = pb_Selection.Location;
                pb_Selection.Location = new Point(newX < 0 ? -newX : 0, newY < 0 ? -newY : 0);
                int PBSelectionDX = pb_Selection.Location.X - oldPBSelectionLocation.X;
                int PBSelectionDY = pb_Selection.Location.Y - oldPBSelectionLocation.Y;
                Selection.MiddlePointPosition = new Point(
                    Selection.MiddlePointPosition.X - PBSelectionDX,
                    Selection.MiddlePointPosition.Y - PBSelectionDY
                );
                SelectionReDraw();                
                pb_Selection.Show();
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
            Point mousePosition = new Point(e.X < -1 ? 65536 - e.X : e.X, e.Y < -1 ? 65536 - e.Y : e.Y);

            int newWidth = pb_Original.Width,
                newHeight = pb_Original.Height,
                newX = pb_Original.Location.X,
                newY = pb_Original.Location.Y;

            double step = 0.1;
            double k = (e.Delta > 0) ? 1 + step : 1 - step;

            ImageScale *= k;

            newWidth = (int)(pb_Original.Image.Size.Width * ImageScale);
            newHeight = (int)(pb_Original.Image.Size.Height * ImageScale);

            if (newWidth < 32767 && newHeight < 32767)
            {

                newX = pb_Original.Location.X - (int)((mousePosition.X - pb_Original.Location.X) * (k - 1));
                newY = pb_Original.Location.Y - (int)((mousePosition.Y - pb_Original.Location.Y) * (k - 1));

                pb_Original.Size = new Size(newWidth, newHeight);
                pb_Original.Location = new Point(newX, newY);

                pb_Selection.Hide();
                pb_Selection.Location = new Point(newX < 0 ? -newX : 0, newY < 0 ? -newY : 0);
                pb_Selection.Show();


                Selection.Size = Size.Empty;
                Selection.isEditable = false;
                SelectionReDraw();

            }
            else
            {
                ImageScale /= k;
            }
        }        

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            pb_Selection.Size = splitContainer1.Panel1.ClientSize;
            SelectionReDraw();
        }        
    }
}
