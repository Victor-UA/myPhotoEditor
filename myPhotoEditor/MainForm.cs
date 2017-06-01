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
        private bool Original_MouseInside { get; set; }

        public MainForm()
        {
            InitializeComponent();
            OriginalImageFile = "";
            ImageLoaded = false;
            Selection = new Selection(new Point(0, 0))
            {                
                isEditable = false
            };
            Selection.Changed += SelectionChanged;
            Original_MousePosition = new Point();
            Original_MouseInside = false;
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
                    }
                    catch (Exception)
                    {
                        OriginalImageFile = "";
                        ImageLoaded = false;
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

        private void Original_BackGround_ReDraw()
        {
            Bitmap bitmap = new Bitmap(pb_Original.Width, pb_Original.Height, PixelFormat.Format32bppArgb);
            Selection.Draw(bitmap);
            pb_Original.BackgroundImage = bitmap;
        }
        private void pb_Original_MouseMove(object sender, MouseEventArgs e)
        {
            tSSL_X.Text = e.X.ToString();
            tSSL_Y.Text = e.Y.ToString();
            Original_MousePosition = new Point(e.X, e.Y);
            if (Selection.isEditable)
            {
                Selection.Size = new Size(
                    e.X - Selection.Position.X,
                    e.Y - Selection.Position.Y
                );
                Original_BackGround_ReDraw();    
            }
        }

        private void pb_Original_Click(object sender, EventArgs e)
        {
            if (!Selection.isEditable)
            {
                Selection.Position = Original_MousePosition;
                Selection.Size = new Size();
            }
            Selection.isEditable = !Selection.isEditable;
        }

        private void pb_Original_MouseEnter(object sender, EventArgs e)
        {
            Original_MouseInside = true;
        }

        private void pb_Original_MouseLeave(object sender, EventArgs e)
        {
            Original_MouseInside = false;
        }
    }
}
