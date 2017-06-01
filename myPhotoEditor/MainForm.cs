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

namespace myPhotoEditor
{
    public partial class MainForm : Form
    {

        private bool ImageLoaded;
        private string OriginalImageFile;

        public MainForm()
        {
            InitializeComponent();
            OriginalImageFile = "";
            ImageLoaded = false;
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

        private void pb_Original_MouseMove(object sender, MouseEventArgs e)
        {
            tSSL_X.Text = e.X.ToString();
            tSSL_Y.Text = e.Y.ToString();
        }
    }
}
