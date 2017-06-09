using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
                Selection.Scale = value;
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
                    grayscaleToolStripMenuItem.Checked = value;
                GrayscaleSwitched();
            }
        }
        private bool _MiddleCrossLines;
        private bool MiddleCrossLines
        {
            get
            {
                return _MiddleCrossLines;
            }
            set
            {
                _MiddleCrossLines = value;
                middleCrosslinesToolStripMenuItem.Checked = 
                    middleCrosslinesToolStripMenuItem1.Checked = value;
                MiddleCrossLinesSwitched();
            }
        }        

        private Point Sensor_MousePosition { get; set; }
        private Point Sensor_LastPosition { get; set; }
        private bool MouseInside { get; set; }

        private bool tSSL_SelectionWidth_MouseEntered;
        private bool tSSL_SelectionHeight_MouseEntered;
        private bool tSSL_SelectionMidPosX_MouseEntered;
        private bool tSSL_SelectionMidPosY_MouseEntered;



        //-------Constructor-------
        public MainForm(string[] args)
        {
            InitializeComponent();            

            pb_Original.Controls.Add(pb_OriginalSensor);            
            pb_OriginalSensor.Size = splitContainer1.Panel1.ClientSize;
            pb_OriginalSensor.Location = new Point(0, 0);
            pb_OriginalSensor.BackColor = Color.Transparent;
            pb_OriginalSensor.BorderStyle = BorderStyle.None;
            pb_OriginalSensor.BringToFront();

            pb_Crop.Controls.Add(pb_CropSensor);
            pb_CropSensor.Size = pb_Crop.Size;
            pb_CropSensor.Location = new Point(0, 0);
            pb_CropSensor.BackColor = Color.Transparent;
            pb_CropSensor.BorderStyle = BorderStyle.None;
            pb_CropSensor.BringToFront();

            splitContainer1.Panel1.MouseWheel += splitContainer1_Panel1_MouseWheel;
            statusStrip1.MouseWheel += statusStrip1_MouseWheel;

            tSSL_SelectionWidth_MouseEntered = false;

            OriginalImageFile = "";
            ImageLoaded = false;

            Selection = new Selection(new Point(0, 0))            
            {                
                isEditable = false
            };
            Selection.SizeChanged += SelectionSizeChanged;
            Selection.LocationChanged += SelectionLocationChanged;
            Selection.SelectionStyleChanged += SelectionStyleChanged;

            Selection.SelectionStyle = SelectionStyle.BoxMiddleOrthoAxis;


            ImageScale = 1;
            Sensor_MousePosition = new Point();
            MouseInside = false;
            MiddleCrossLines = true;
            

            if (args.Length > 0)
            {                
                string fileName = args[0];
                if (File.Exists(fileName))
                {
                    OpenFile(fileName);
                }
            }
        }        



        //---------Menu---------
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }        
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }
        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grayscale = !Grayscale;
        }
        private void middleCrosslinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiddleCrossLines = !MiddleCrossLines;
        }
        private void boxAndDiagonlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Selection.SelectionStyle = SelectionStyle.BoxDiagonal;
        }
        private void boxAndOrthoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Selection.SelectionStyle = SelectionStyle.BoxMiddleOrthoAxis;
        }


        //-------Methods-------
        private void OpenFile()
        {
            OpenFile(string.Empty);

        }
        private void OpenFile(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
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
                }
                else
                {
                    OriginalImageFile = fileName;
                }
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
                        ImageScaleToFit();
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

        private void SaveFileAs()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = "Оберіть зображення",
                AddExtension = true,
                Filter = "PNG| *.png; | JPEG| *.jpeg; *.jpg; | BMP| *.bmp; | All Images| *.jpeg; *.jpg; *.png; *.bmp;"
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
                try
                {
                    pb_Crop.Image.Save(dialog.FileName, ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void GrayscaleSwitched()
        {            
            CropImage();
        }
        private void MiddleCrossLinesSwitched()
        {
            if (MiddleCrossLines)
            {
                Bitmap bitmap = new Bitmap(pb_CropSensor.Width, pb_CropSensor.Height);
                Graphics g = Graphics.FromImage(bitmap);
                Pen pen = new Pen(Brushes.LightGreen, 1);
                g.DrawLine(pen, pb_CropSensor.Width / 2, 0, pb_CropSensor.Width / 2, pb_CropSensor.Height);
                g.DrawLine(pen, 0, pb_CropSensor.Height / 2, pb_CropSensor.Width, pb_CropSensor.Height / 2);
                pb_CropSensor.BackgroundImage = bitmap;
            }
            else
            {
                pb_CropSensor.BackgroundImage = null;
            }
        }
        private void SelectionStyleChanged(object sender, EventArgs e)
        {
            switch (Selection.SelectionStyle)
            {
                case SelectionStyle.BoxDiagonal:
                    boxAndDiagonlsToolStripMenuItem.Checked =
                        boxAndDiagonalsToolStripMenuItem.Checked = true;
                    boxAndOrthoToolStripMenuItem.Checked =
                        boxAndMiddleOrthoAxisToolStripMenuItem.Checked = false;
                    break;
                case SelectionStyle.BoxMiddleOrthoAxis:
                    boxAndDiagonlsToolStripMenuItem.Checked =
                        boxAndDiagonalsToolStripMenuItem.Checked = false;
                    boxAndOrthoToolStripMenuItem.Checked =
                        boxAndMiddleOrthoAxisToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
            //SelectionReDraw();
        }

        private void ImageScaleToFit()
        {
            Point focusReal = new Point(
                pb_Original.Image.Width / 2,
                pb_Original.Image.Height / 2
            );
            double scaleWidth = (double)splitContainer1.Panel1.ClientSize.Width / pb_Original.Image.Size.Width;
            double scaleHeight = (double)splitContainer1.Panel1.ClientSize.Height / pb_Original.Image.Size.Height;
            ImageMoveToCenter(focusReal);
            double scale = scaleWidth < scaleHeight ? scaleWidth : scaleHeight;
            ImageScaleTo(focusReal, scale > 2 ? 2 : scale);
        }
        private void ImageScaleTo(Point focusReal, double scale)
        {
            
            //Selection.Size = Size.Empty;
            Selection.isEditable = false;

            int newWidth = pb_Original.Width,
                newHeight = pb_Original.Height,
                newX = pb_Original.Location.X,
                newY = pb_Original.Location.Y;

            newWidth = (int)(pb_Original.Image.Size.Width * scale);
            newHeight = (int)(pb_Original.Image.Size.Height * scale);

            if (newWidth < 32767 && newHeight < 32767)
            {
                newX = pb_Original.Location.X - (int)(focusReal.X * (scale - ImageScale));
                newY = pb_Original.Location.Y - (int)(focusReal.Y * (scale - ImageScale));

                Size size = new Size(newWidth, newHeight);
                Point TopLeft = new Point(newX, newY);

                if (Check_pb_Original_Visibility(new Rectangle(TopLeft, size)))
                {
                    ImageScale = scale;
                    pb_Original.Size = size;
                    pb_Original.Location = TopLeft;
                    
                    ChangeSensorLocation();                    
                }
            }            
            GC.Collect();
        }
        private void ImageMove(Point offset)
        {
            int newX = pb_Original.Location.X + offset.X;
            int newY = pb_Original.Location.Y + offset.Y;

            Point TopLeft = new Point(newX, newY);
            if (Check_pb_Original_Visibility(new Rectangle(TopLeft, pb_Original.Size)))
            {
                pb_Original.Location = TopLeft;                
            }
        }
        private void ImageMoveToCenter(Point focusReal)
        {
            int newX = (int)(splitContainer1.Panel1.Width / 2 - (focusReal.X * ImageScale));
            int newY = (int)(splitContainer1.Panel1.Height / 2 - (focusReal.Y * ImageScale));
            pb_Original.Location = new Point(newX, newY);
            ChangeSensorLocation();
        }

        private void SelectionSizeChanged(object sender, EventArgs e)
        {
            Size size = ((Selection)sender).Size;            
            tSSL_SelectionWidth.Text = Math.Round((size.Width) / ImageScale).ToString();
            tSSL_SelectionHeight.Text = Math.Round((size.Height) / ImageScale).ToString();
            SelectionReDraw();
        }
        private void SelectionLocationChanged(object sender, EventArgs e)
        {
            Point midPoint = ((Selection)sender).MiddlePointPosition;
            tSSL_SelectionMidPosX.Text = Math.Round((midPoint.X + pb_OriginalSensor.Location.X) / ImageScale).ToString();
            tSSL_SelectionMidPosY.Text = Math.Round((midPoint.Y + pb_OriginalSensor.Location.Y) / ImageScale).ToString();
            SelectionReDraw();
        }

        private void SelectionReDraw()
        {
            try
            {
                Bitmap bitmap = new Bitmap(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height, PixelFormat.Format32bppArgb);
                Selection.Draw(bitmap);
                pb_OriginalSensor.BackgroundImage = bitmap;
                pb_OriginalSensor.Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\r" + ex.StackTrace);
            }
        }
        
        private void ChangeSensorLocation()
        {
            int newX = pb_Original.Location.X;
            int newY = pb_Original.Location.Y;

            pb_OriginalSensor.Hide();

            pb_OriginalSensor.Location = new Point(
                newX < 0 ? -newX : 0, 
                newY < 0 ? -newY : 0
            );
            
            Selection.MiddlePointPosition = new Point(
                (int)((Selection.MiddlePointRealPosition.X * ImageScale) - pb_OriginalSensor.Location.X),
                (int)(((Selection.MiddlePointRealPosition.Y * ImageScale) - pb_OriginalSensor.Location.Y))
            );
            //Selection.Size = new Size((int)(Selection.RealSize.Width * ImageScale), (int)(Selection.RealSize.Height * ImageScale));            

            pb_OriginalSensor.Show();
            CropImage();            
        }

        private void CropImage()
        {
            try
            {
                //Bitmap cropBitmap = CopyRegionIntoImage(new Bitmap(pb_Original.Image), Selection.getRegionReal(ImageScale, pb_OriginalSensor.Location));
                Bitmap cropBitmap = CopyRegionIntoImage(new Bitmap(pb_Original.Image), Selection.getRegionReal());
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
        private MouseEventArgs Panel1_2_pb_Selection(MouseEventArgs e)
        {
            return new MouseEventArgs(
                e.Button,
                e.Clicks,
                e.X - pb_Original.Location.X - pb_OriginalSensor.Location.X,
                e.Y - pb_Original.Location.Y - pb_OriginalSensor.Location.Y,
                e.Delta
            );
        }
                
        private Point ExpandMousePosition(MouseEventArgs e)
        {
            return new Point(e.X < -2048 ? 65536 - e.X : e.X, e.Y < -2048 ? 65536 - e.Y : e.Y);
        }

        private bool Check_pb_Original_Visibility(Rectangle original)
        {
            bool result = true;
            result &= (original.X < (splitContainer1.Panel1.ClientSize.Width - 10));
            result &= (original.Y < (splitContainer1.Panel1.ClientSize.Height - 10));
            result &= ((original.X + original.Width) > 10);
            result &= ((original.Y + original.Height) > 10);
            return result;
        }


        //-------EventHadlers-------
        private void pb_Selection_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            Selection.MouseEventArgs = mouse;
            Point mousePosition = new Point(e.X < -2048 ? 65536 + e.X : e.X, e.Y < -2048 ? 65536 + e.Y : e.Y);
            tSSL_X.Text = Math.Round((mousePosition.X + pb_OriginalSensor.Location.X) / ImageScale).ToString();
            tSSL_Y.Text = Math.Round((mousePosition.Y + pb_OriginalSensor.Location.Y) / ImageScale).ToString();
            
            
            if (mouse.Button == MouseButtons.Middle)
            {
                if (Selection.MouseEntered)
                {
                    Selection.Offset(e.X - Sensor_LastPosition.X, e.Y - Sensor_LastPosition.Y, ImageScale);
                    CropImage();
                }
                else
                {
                    Point offset = new Point(
                        e.X - MiddleButtonDown.X,
                        e.Y - MiddleButtonDown.Y
                    );
                    ImageMove(offset);
                }
            }
            else
            {
                if (Selection.isEditable && ImageLoaded)
                {
                    Selection.RealSizeRecalc(new Size(
                        Math.Abs(mousePosition.X - Selection.MiddlePointPosition.X) * 2,
                        Math.Abs(mousePosition.Y - Selection.MiddlePointPosition.Y) * 2
                    ));          
                    CropImage();
                }
            }
            Sensor_MousePosition = new Point(mousePosition.X, mousePosition.Y);
            Sensor_LastPosition = e.Location;
        }        
        private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {            
            pb_Selection_MouseMove(sender, Panel1_2_pb_Selection(e));
        }


        private void pb_Selection_MouseEnter(object sender, EventArgs e)
        {
            MouseInside = true;
            pb_OriginalSensor.Focus();
        }
        private void pb_Selection_MouseLeave(object sender, EventArgs e)
        {
            MouseInside = false;
        }

        private void pb_Selection_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            if (mouse.Button == MouseButtons.Middle)
            {
                MiddleButtonDown = mouse.Location;
                Sensor_LastPosition = mouse.Location;
            }
        }
        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            pb_Selection_MouseDown(sender, Panel1_2_pb_Selection(e));
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
                        Selection.RealSizeRecalc(Size.Empty);
                        Selection.MiddlePointPosition = Sensor_MousePosition;
                        Selection.MiddlePointRealPosition = new Point(
                            (int)((Sensor_MousePosition.X + pb_OriginalSensor.Location.X + 1) / ImageScale),
                            (int)((Sensor_MousePosition.Y + pb_OriginalSensor.Location.Y + 1) / ImageScale)
                        );
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
        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Selection.isEditable)
                pb_Selection_MouseClick(sender, e);
        }

        private void pb_Selection_MouseUp(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Middle)
            {
                //ChangeSensorLocation();
            }
        }
        private void splitContainer1_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            pb_Selection_MouseUp(sender, Panel1_2_pb_Selection(e));
        }

        private void pb_Selection_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            if (ImageLoaded)
            {
                Point focus = ExpandMousePosition(e as MouseEventArgs);
                if (mouse.Button == MouseButtons.Middle)
                {
                    ImageScaleToFit();
                }
                if (mouse.Button == MouseButtons.Left)
                {
                    Point focusReal = new Point(
                        (int)((focus.X - (pb_Original.Location.X < 0 ? pb_Original.Location.X : 0)) / ImageScale),
                        (int)((focus.Y - (pb_Original.Location.Y < 0 ? pb_Original.Location.Y : 0)) / ImageScale)
                    );
                    ImageMoveToCenter(focusReal);
                    ImageScaleTo(focusReal, ImageScale * 2);                    
                }
            }
        }
        private void splitContainer1_Panel1_DoubleClick(object sender, EventArgs e)
        {
            pb_Selection_DoubleClick(sender, Panel1_2_pb_Selection(e as MouseEventArgs));
        }

        private void splitContainer1_Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            Point focus = ExpandMousePosition(e);
            double step = 0.1;
            double k = (e.Delta > 0) ? 1 + step : 1 - step;

            Point focusReal = new Point(
                (int)((focus.X - pb_Original.Location.X + 4) / ImageScale),
                (int)((focus.Y - pb_Original.Location.Y + 4) / ImageScale)
            );
            ImageScaleTo(focusReal, ImageScale * k);
        }
        

        private void statusStrip1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (tSSL_SelectionWidth_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.RealWidth += (int)(2 / ImageScale);
                }
                else
                {
                    if (Selection.Size.Width > 2)
                        Selection.RealWidth -= (int)(2 / ImageScale);
                }
                CropImage();
                return;
            }
            if (tSSL_SelectionHeight_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.RealHeight += (int)(2 / ImageScale);
                }
                else
                {
                    if (Selection.Size.Height > 2)
                        Selection.RealHeight -= (int)(2 / ImageScale);
                }
                CropImage();
                return;
            }
            if (tSSL_SelectionMidPosX_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.Offset(1, 0, ImageScale);
                }
                else
                {
                    Selection.Offset(-1, 0, ImageScale);
                }
                CropImage();
                return;
            }
            if (tSSL_SelectionMidPosY_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.Offset(0, 1, ImageScale);
                }
                else
                {
                    Selection.Offset(0, -1, ImageScale);
                }
                CropImage();
                return;
            }
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            pb_OriginalSensor.Size = splitContainer1.Panel1.ClientSize;
            //SelectionReDraw();
        }

        private void splitContainer1_Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))
                e.Effect = DragDropEffects.Move;
        }        
        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("DragDrop");
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && e.Effect == DragDropEffects.Move)
            {
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (objects.Length > 0)
                {
                    OpenFile(objects[0]);
                }
            }
        }        

        private void pb_CropSensor_Resize(object sender, EventArgs e)
        {
            MiddleCrossLinesSwitched();
        }

        private void tSSL_SelectionWidth_MouseEnter(object sender, EventArgs e)
        {
            tSSL_SelectionWidth_MouseEntered = true;
        }
        private void tSSL_SelectionWidth_MouseLeave(object sender, EventArgs e)
        {
            tSSL_SelectionWidth_MouseEntered = false;
        }

        private void tSSL_SelectionHeight_MouseEnter(object sender, EventArgs e)
        {
            tSSL_SelectionHeight_MouseEntered = true;
        }
        private void tSSL_SelectionHeight_MouseLeave(object sender, EventArgs e)
        {
            tSSL_SelectionHeight_MouseEntered = false;
        }

        private void splitContainer1_Panel1_MouseEnter(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Focus();
        }

        private void statusStrip1_MouseEnter(object sender, EventArgs e)
        {
            statusStrip1.Focus();            
        }

        private void tSSL_SelectionMidPosX_MouseEnter(object sender, EventArgs e)
        {
            tSSL_SelectionMidPosX_MouseEntered = true;
        }
        private void tSSL_SelectionMidPosX_MouseLeave(object sender, EventArgs e)
        {
            tSSL_SelectionMidPosX_MouseEntered = false;
        }

        private void tSSL_SelectionMidPosY_MouseEnter(object sender, EventArgs e)
        {
            tSSL_SelectionMidPosY_MouseEntered = true;
        }
        private void tSSL_SelectionMidPosY_MouseLeave(object sender, EventArgs e)
        {
            tSSL_SelectionMidPosY_MouseEntered = false;
        }        
    }
}
