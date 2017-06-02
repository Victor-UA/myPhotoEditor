using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myPhotoEditor.Base;

namespace myPhotoEditor
{
    public partial class MainForm : Form
    {

        private bool ImageLoaded;
        private string OriginalImageFile;
        private Selection Selection;

        private Point Original_MousePosition { get; set; }
        private bool MouseInside { get; set; }

        public MainForm()
        {
            InitializeComponent();

            pb_Original.Controls.Add(pb_Selection);
            pb_Selection.Size = pb_Original.Size;
            pb_Selection.Location = new Point(0, 0);
            pb_Selection.BackColor = Color.Transparent;
            pb_Selection.BorderStyle = BorderStyle.None;
            pb_Selection.BringToFront();

            OriginalImageFile = "";
            ImageLoaded = false;
            Selection = new Selection(new Point(0, 0))
            {                
                isEditable = false
            };
            Selection.Changed += SelectionChanged;
            Original_MousePosition = new Point();
            MouseInside = false;
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
                        ImageLoaded = true;
                        Selection.isEditable = false;
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
            Bitmap bitmap = new Bitmap(pb_Selection.Width, pb_Selection.Height, PixelFormat.Format32bppArgb);
            Selection.Draw(bitmap);
            pb_Selection.BackgroundImage = bitmap;
            pb_Selection.Refresh();
        }
        private Bitmap CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion)
        {
            try
            {
                Rectangle destRegion = new Rectangle(0, 0, srcRegion.Width, srcRegion.Height);                
                Bitmap destBitmap = new Bitmap(srcRegion.Width, srcRegion.Height, PixelFormat.Format32bppArgb);            
                using (Graphics grD = Graphics.FromImage(destBitmap))
                {
                    grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
                }
                return destBitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }        

        private void pb_Selection_MouseMove(object sender, MouseEventArgs e)
        {
            tSSL_X.Text = e.X.ToString();
            tSSL_Y.Text = e.Y.ToString();
            Original_MousePosition = new Point(e.X, e.Y);
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

        private void CropImage()
        {
            Rectangle srcRegion = new Rectangle(
                    new Point(
                        Selection.MiddlePointPosition.X - Selection.Size.Width / 2,
                        Selection.MiddlePointPosition.Y - Selection.Size.Height / 2
                    ),
                    Selection.Size
                );
            Bitmap cropBitmap = CopyRegionIntoImage(new Bitmap(pb_Original.Image), Selection.getRegion());
            if (cropBitmap != null)
            {
                pb_Crop.Image = cropBitmap;
                pb_Crop.Refresh();
            }
        }

        private void pb_Selection_MouseLeave(object sender, EventArgs e)
        {
            MouseInside = false;
        }

        private void pb_Selection_MouseClick(object sender, MouseEventArgs e)
        {
            if (ImageLoaded)
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
        }

        private void pb_Selection_MouseEnter(object sender, EventArgs e)
        {
            MouseInside = true;
        }
    }
}
