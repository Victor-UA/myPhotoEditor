﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using myPhotoEditor.About;
using myPhotoEditor.Objects;
using myPhotoEditor.Properties;
using myPhotoEditor.Tools;

namespace myPhotoEditor.Main
{
    public partial class MainForm : Form
    {
        private bool ImageLoaded;
        private string OriginalImageFile;
        private Selection Selection;
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
                try
                {
                    Selection.SelectionStyle = value;
                }
                catch (Exception ex)
                {
                }
                switch (value)
                {
                    case SelectionStyles.BoxDiagonal:
                        boxAndDiagonlsToolStripMenuItem.Checked =
                            boxAndDiagonalsToolStripMenuItem.Checked = true;
                        boxAndOrthoToolStripMenuItem.Checked =
                            boxAndMiddleOrthoAxisToolStripMenuItem.Checked = false;
                        break;
                    case SelectionStyles.BoxMiddleOrthoAxis:
                        boxAndDiagonlsToolStripMenuItem.Checked =
                            boxAndDiagonalsToolStripMenuItem.Checked = false;
                        boxAndOrthoToolStripMenuItem.Checked =
                            boxAndMiddleOrthoAxisToolStripMenuItem.Checked = true;
                        break;
                    default:
                        break;
                }
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
                pb_OriginalSensor.ImageScale = value;
                tSSL_ImageScale.Text = Math.Round(value, 2).ToString();
            }
        }
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

        private Dictionary<MouseButtons, MouseButtonStates> myMouseButtons { get; set; }


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
            myMouseButtons = pb_OriginalSensor.MouseButtonsState;


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

            Selection = null;

            ImageScale = 1;

            if (args.Length > 0)
            {
                string fileName = args[0];
                if (File.Exists(fileName))
                {
                    OpenFile(fileName);
                }
            }
            else
            {
                LoadOriginalImage(OriginalImageSourceTypes.Clipboard);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.MainWindowLocation != null)
            {
                Location = Settings.Default.MainWindowLocation;
            }
            if (Settings.Default.MainWindowSize != null)
            {
                Size = Settings.Default.MainWindowSize;
            }
            if (Settings.Default.MainWindowState != FormWindowState.Minimized)
            {
                WindowState = Settings.Default.MainWindowState;
            }            
            MiddleCrossLines = Settings.Default.MiddleCrossLines;

            if (Settings.Default.LastOpenedFiles != null)
            {
                foreach (string item in Settings.Default.LastOpenedFiles)
                {
                    AddLastOpenedFileToMenu(item);
                }                
            }

            SelectionStyle = Settings.Default.SelectionStyle;            
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.MainWindowLocation = Location;
            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.MainWindowSize = Size;
            }
            else
            {
                Settings.Default.MainWindowSize = RestoreBounds.Size;
            }
            Settings.Default.MainWindowState = WindowState;
            Settings.Default.MiddleCrossLines = MiddleCrossLines;
            Settings.Default.SelectionStyle = SelectionStyle;

            int TopBoardIndex = fileToolStripMenuItem.DropDownItems.IndexOf(tsmi_LastOpenFilesTopBoard);            
            int BottomBoardIndex = fileToolStripMenuItem.DropDownItems.IndexOf(tsmi_LastOpenFilesBottomBoard);
            if (BottomBoardIndex - TopBoardIndex > 1)
            {
                Settings.Default.LastOpenedFiles = new System.Collections.Specialized.StringCollection();
                for (int index = BottomBoardIndex - 1; index > TopBoardIndex; index--)
                {
                    Settings.Default.LastOpenedFiles.Add(fileToolStripMenuItem.DropDownItems[index].Text);
                }
            }

            Settings.Default.Save();
        }



        //---------Menu---------
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void openFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOriginalImage(OriginalImageSourceTypes.Clipboard);
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }
        private void LastOpenedFile_Click(object sender, EventArgs e)
        {
            OpenFile(((ToolStripMenuItem)sender).Text);
        }
        private void AddLastOpenedFileToMenu(string fileName)
        {
            if (!fileToolStripMenuItem.DropDownItems.ContainsKey(fileName))
            {
                Image image = null;
                try
                {
                    image = Image.FromFile(fileName);
                }
                catch (Exception){ }
                ToolStripMenuItem menuItem = new ToolStripMenuItem(fileName, image, LastOpenedFile_Click)
                {
                    Name = fileName
                };
                int TopBoardIndex = fileToolStripMenuItem.DropDownItems.IndexOf(tsmi_LastOpenFilesTopBoard);
                fileToolStripMenuItem.DropDownItems.Insert(TopBoardIndex + 1, menuItem);
                int BottomBoardIndex = fileToolStripMenuItem.DropDownItems.IndexOf(tsmi_LastOpenFilesBottomBoard);
                for (int index = TopBoardIndex + 11; index < BottomBoardIndex; index++)
                {
                    fileToolStripMenuItem.DropDownItems.RemoveAt(index);
                }
                tsmi_LastOpenFilesTopBoard.Visible = BottomBoardIndex - TopBoardIndex > 1;
            }
        }
        private void sendToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pb_Crop.Image);
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
            SelectionStyle = SelectionStyles.BoxDiagonal;
            SensorReDraw();
        }
        private void boxAndOrthoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectionStyle = SelectionStyles.BoxMiddleOrthoAxis;
            SensorReDraw();
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pb_OriginalSensor.Items.Remove(Selection);
            CreateSelection(new Point(0, 0));
            Selection.MiddlePointRealPosition = new Point(pb_Original.Image.Width / 2, pb_Original.Image.Height / 2);
            Selection.RealWidth = pb_Original.Image.Width;
            Selection.RealHeight = pb_Original.Image.Height;
            Selection.State = ItemStates.Normal;
            SensorReDraw();
        }
        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pb_OriginalSensor.Items.Remove(Selection);
            Selection = null;
            SensorReDraw();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
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
                    LoadOriginalImage(OriginalImageSourceTypes.File);
                }
            }
            catch (Exception)
            {
                OriginalImageFile = "";
            }
        }

        private void LoadOriginalImage(OriginalImageSourceTypes sourceType)
        {
            try
            {
                switch (sourceType)
                {
                    case OriginalImageSourceTypes.File:
                        pb_Original.Load(OriginalImageFile);
                        break;
                    case OriginalImageSourceTypes.Clipboard:
                        pb_Original.Image = Clipboard.GetImage();
                        break;
                }
                pb_Original.Size = pb_Original.Image.Size;
                pb_Original.Location = new Point(0, 0);
                pb_OriginalSensor.Focus();
                ImageLoaded = true;
                ImageScale = 1;
                Text = "myPhotoEditor: " + OriginalImageFile;
                ImageScaleToFit();
                clearSelectionToolStripMenuItem_Click(this, new EventArgs());
                AddLastOpenedFileToMenu(OriginalImageFile);
            }
            catch (Exception)
            {
                OriginalImageFile = "";
                ImageLoaded = false;
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
            //Selection.State = ItemStates.Normal;

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
            ImageMove(offset.X, offset.Y);
        }
        private void ImageMove(int dX, int dY)
        {
            int newX = pb_Original.Location.X + dX;
            int newY = pb_Original.Location.Y + dY;

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

        private void CreateSelection(Point point)
        {
            Selection = new Selection(point, pb_OriginalSensor)
            {
                SelectionStyle = SelectionStyle
            };
            Selection.SizeChanged += Selection_SizeChanged;
            Selection.LocationChanged += Selection_LocationChanged;            
            Selection.Border.MouseEnter += Selection_MouseEnterBorder;
            Selection.Border.MouseLeave += Selection_MouseLeaveBorder;
            foreach (BorderSide item in Selection.Border.Sides.Values)
            {
                item.MouseEnter += Selection_MouseEnterBorderSide;
                item.MouseLeave += Selection_MouseLeaveBorderSide;
            }
            Selection_LocationChanged(Selection, new EventArgs());
            //if (boxAndDiagonlsToolStripMenuItem.Checked)
            //{
            //    SelectionStyle = SelectionStyles.BoxDiagonal;
            //}
            //else
            //{
            //    SelectionStyle = SelectionStyles.BoxMiddleOrthoAxis;
            //}
        }

        private void Selection_SizeChanged(object sender, EventArgs e)
        {
            Size size = ((Selection)sender).Size;
            tSSL_SelectionWidth.Text = Math.Round((size.Width) / ImageScale).ToString();
            tSSL_SelectionHeight.Text = Math.Round((size.Height) / ImageScale).ToString();
            SensorReDraw();
            CropImage();
        }
        private void Selection_LocationChanged(object sender, EventArgs e)
        {
            Point midPoint = ((Selection)sender).MiddlePointPosition;
            tSSL_SelectionMidPosX.Text = Math.Round((midPoint.X + pb_OriginalSensor.Location.X) / ImageScale).ToString();
            tSSL_SelectionMidPosY.Text = Math.Round((midPoint.Y + pb_OriginalSensor.Location.Y) / ImageScale).ToString();
            SensorReDraw();
            CropImage();
        }
        private void Selection_MouseEnterBorder(object sender, EventArgs e)
        {
            SensorReDraw();
        }
        private void Selection_MouseLeaveBorder(object sender, EventArgs e)
        {
            SensorReDraw();
        }
        private void Selection_MouseEnterBorderSide(object sender, EventArgs e)
        {
            SensorReDraw();
        }
        private void Selection_MouseLeaveBorderSide(object sender, EventArgs e)
        {
            SensorReDraw();
        }

        private void SensorReDraw()
        {
            try
            {
                pb_OriginalSensor.Draw();
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

            SensorReDraw();
            pb_OriginalSensor.Show();
        }

        private void CropImage()
        {
            try
            {
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
        private MouseEventArgs Panel1_2_pb_OriginalSensor(MouseEventArgs e)
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
        private void pb_OriginalSensor_MouseMove(object sender, MouseEventArgs e)
        {
            tSSL_X.Text = Math.Round((e.X + pb_OriginalSensor.Location.X) / ImageScale).ToString();
            tSSL_Y.Text = Math.Round((e.Y + pb_OriginalSensor.Location.Y) / ImageScale).ToString();

            if (myMouseButtons[MouseButtons.Middle].State)
            {
                ImageMove(e.X - myMouseButtons[MouseButtons.Middle].Location.X, e.Y - myMouseButtons[MouseButtons.Middle].Location.Y);
            }
        }
        private void pb_OriginalSensor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ChangeSensorLocation();
            }
        }
        private void pb_OriginalSensor_MouseClick(object sender, MouseEventArgs e)
        {
            if (ImageLoaded)
            {
                if (myMouseButtons[MouseButtons.Left].State)
                {
                    if (Selection == null)
                    {
                        CreateSelection(e.Location);
                    }
                }
            }
        }
        private void pb_OriginalSensor_DoubleClick(object sender, EventArgs e)
        {
            if (ImageLoaded)
            {
                Point focus = ExpandMousePosition(e as MouseEventArgs);
                if ((e as MouseEventArgs).Button == MouseButtons.Middle)
                {
                    ImageScaleToFit();
                }
                if ((e as MouseEventArgs).Button == MouseButtons.Left)
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
        private void splitContainer1_Panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pb_OriginalSensor_DoubleClick(sender, Panel1_2_pb_OriginalSensor(e));
        }
        private void pb_OriginalSensor_LocationChanged(object sender, EventArgs e)
        {
            //Selection.Offset = pb_OriginalSensor.Location;
        }

        private void splitContainer1_Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ImageLoaded)
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
        }

        private void statusStrip1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (tSSL_SelectionWidth_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.RealWidth += ((int)(2 / ImageScale) == 0 ? 2 : (int)(2 / ImageScale));
                }
                else
                {
                    if (Selection.Size.Width > 2)
                        Selection.RealWidth -= ((int)(2 / ImageScale) == 0 ? 2 : (int)(2 / ImageScale));
                }
                CropImage();
                return;
            }
            if (tSSL_SelectionHeight_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.RealHeight += ((int)(2 / ImageScale) == 0 ? 2 : (int)(2 / ImageScale));
                }
                else
                {
                    if (Selection.Size.Height > 2)
                        Selection.RealHeight -= ((int)(2 / ImageScale) == 0 ? 2 : (int)(2 / ImageScale));
                }
                CropImage();
                return;
            }
            if (tSSL_SelectionMidPosX_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.MiddlePointRealPosition = new Point(
                        Selection.MiddlePointRealPosition.X + ((int)(1 / ImageScale) > 0 ? (int)(1 / ImageScale) : 1),
                        Selection.MiddlePointRealPosition.Y
                    );
                }
                else
                {
                    Selection.MiddlePointRealPosition = new Point(
                        Selection.MiddlePointRealPosition.X - ((int)(1 / ImageScale) > 0 ? (int)(1 / ImageScale) : 1),
                        Selection.MiddlePointRealPosition.Y
                    );
                }
                CropImage();
                return;
            }
            if (tSSL_SelectionMidPosY_MouseEntered)
            {
                if (e.Delta > 0)
                {
                    Selection.MiddlePointRealPosition = new Point(
                        Selection.MiddlePointRealPosition.X,
                        Selection.MiddlePointRealPosition.Y + ((int)(1 / ImageScale) > 0 ? (int)(1 / ImageScale) : 1)
                    );
                }
                else
                {
                    Selection.MiddlePointRealPosition = new Point(
                        Selection.MiddlePointRealPosition.X,
                        Selection.MiddlePointRealPosition.Y - ((int)(1 / ImageScale) > 0 ? (int)(1 / ImageScale) : 1)
                    );
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
            pb_OriginalSensor.Focus();
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
