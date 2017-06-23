using myPhotoEditor.Objects;

namespace myPhotoEditor.Main
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip_OriginalSide = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFromBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSelectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.boxAndDiagonalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxAndMiddleOrthoAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pb_Original = new System.Windows.Forms.PictureBox();
            this.pb_OriginalSensor = new myPhotoEditor.Objects.Sensor();
            this.pb_CropSensor = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip_CropSide = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToClipboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.grayscaleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.middleCrosslinesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pb_Crop = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.middleCrosslinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxAndDiagonlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxAndOrthoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_X = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_Y = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_ImageScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_SelectionMidPosX = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_SelectionMidPosY = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_SelectionWidth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_SelectionHeight = new System.Windows.Forms.ToolStripStatusLabel();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip_OriginalSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_OriginalSensor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_CropSensor)).BeginInit();
            this.contextMenuStrip_CropSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Crop)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.ContextMenuStrip = this.contextMenuStrip_OriginalSide;
            this.splitContainer1.Panel1.Controls.Add(this.pb_Original);
            this.splitContainer1.Panel1.Controls.Add(this.pb_OriginalSensor);
            this.splitContainer1.Panel1.SizeChanged += new System.EventHandler(this.splitContainer1_Panel1_SizeChanged);
            this.splitContainer1.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragDrop);
            this.splitContainer1.Panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragEnter);
            this.splitContainer1.Panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseDoubleClick);
            this.splitContainer1.Panel1.MouseEnter += new System.EventHandler(this.splitContainer1_Panel1_MouseEnter);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pb_CropSensor);
            this.splitContainer1.Panel2.Controls.Add(this.pb_Crop);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(789, 373);
            this.splitContainer1.SplitterDistance = 418;
            this.splitContainer1.TabIndex = 0;
            // 
            // contextMenuStrip_OriginalSide
            // 
            this.contextMenuStrip_OriginalSide.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.openFromBufferToolStripMenuItem,
            this.toolStripMenuItem2,
            this.selectAllToolStripMenuItem1,
            this.clearSelectionToolStripMenuItem1,
            this.toolStripMenuItem4});
            this.contextMenuStrip_OriginalSide.Name = "contextMenuStrip_OriginalSide";
            this.contextMenuStrip_OriginalSide.Size = new System.Drawing.Size(188, 142);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openFromBufferToolStripMenuItem
            // 
            this.openFromBufferToolStripMenuItem.Name = "openFromBufferToolStripMenuItem";
            this.openFromBufferToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openFromBufferToolStripMenuItem.Text = "Open from Clipboard";
            this.openFromBufferToolStripMenuItem.Click += new System.EventHandler(this.openFromClipboardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(184, 6);
            // 
            // clearSelectionToolStripMenuItem1
            // 
            this.clearSelectionToolStripMenuItem1.Name = "clearSelectionToolStripMenuItem1";
            this.clearSelectionToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.clearSelectionToolStripMenuItem1.Text = "ClearSelection";
            this.clearSelectionToolStripMenuItem1.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boxAndDiagonalsToolStripMenuItem,
            this.boxAndMiddleOrthoAxisToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItem4.Text = "Selection style";
            // 
            // boxAndDiagonalsToolStripMenuItem
            // 
            this.boxAndDiagonalsToolStripMenuItem.Name = "boxAndDiagonalsToolStripMenuItem";
            this.boxAndDiagonalsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.boxAndDiagonalsToolStripMenuItem.Text = "Box and diagonals";
            this.boxAndDiagonalsToolStripMenuItem.Click += new System.EventHandler(this.boxAndDiagonlsToolStripMenuItem_Click);
            // 
            // boxAndMiddleOrthoAxisToolStripMenuItem
            // 
            this.boxAndMiddleOrthoAxisToolStripMenuItem.Name = "boxAndMiddleOrthoAxisToolStripMenuItem";
            this.boxAndMiddleOrthoAxisToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.boxAndMiddleOrthoAxisToolStripMenuItem.Text = "Box and middle ortho axis";
            this.boxAndMiddleOrthoAxisToolStripMenuItem.Click += new System.EventHandler(this.boxAndOrthoToolStripMenuItem_Click);
            // 
            // pb_Original
            // 
            this.pb_Original.ImageLocation = "";
            this.pb_Original.Location = new System.Drawing.Point(0, 0);
            this.pb_Original.Name = "pb_Original";
            this.pb_Original.Size = new System.Drawing.Size(416, 371);
            this.pb_Original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Original.TabIndex = 0;
            this.pb_Original.TabStop = false;
            // 
            // pb_OriginalSensor
            // 
            this.pb_OriginalSensor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_OriginalSensor.ContextMenuStrip = this.contextMenuStrip_OriginalSide;
            this.pb_OriginalSensor.Cursor = System.Windows.Forms.Cursors.Default;
            this.pb_OriginalSensor.ImageScale = 0D;
            this.pb_OriginalSensor.Location = new System.Drawing.Point(0, 0);
            this.pb_OriginalSensor.Name = "pb_OriginalSensor";
            this.pb_OriginalSensor.Size = new System.Drawing.Size(416, 371);
            this.pb_OriginalSensor.TabIndex = 1;
            this.pb_OriginalSensor.TabStop = false;
            this.pb_OriginalSensor.LocationChanged += new System.EventHandler(this.pb_OriginalSensor_LocationChanged);
            this.pb_OriginalSensor.DoubleClick += new System.EventHandler(this.pb_OriginalSensor_DoubleClick);
            this.pb_OriginalSensor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_OriginalSensor_MouseClick);
            this.pb_OriginalSensor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_OriginalSensor_MouseMove);
            this.pb_OriginalSensor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_OriginalSensor_MouseUp);
            // 
            // pb_CropSensor
            // 
            this.pb_CropSensor.ContextMenuStrip = this.contextMenuStrip_CropSide;
            this.pb_CropSensor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_CropSensor.Location = new System.Drawing.Point(0, 0);
            this.pb_CropSensor.Name = "pb_CropSensor";
            this.pb_CropSensor.Size = new System.Drawing.Size(365, 371);
            this.pb_CropSensor.TabIndex = 4;
            this.pb_CropSensor.TabStop = false;
            this.pb_CropSensor.Resize += new System.EventHandler(this.pb_CropSensor_Resize);
            // 
            // contextMenuStrip_CropSide
            // 
            this.contextMenuStrip_CropSide.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem1,
            this.sendToClipboardToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.grayscaleToolStripMenuItem1,
            this.middleCrosslinesToolStripMenuItem1});
            this.contextMenuStrip_CropSide.Name = "contextMenuStrip_CropSide";
            this.contextMenuStrip_CropSide.Size = new System.Drawing.Size(170, 98);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.saveAsToolStripMenuItem1.Text = "SaveAs";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // sendToClipboardToolStripMenuItem1
            // 
            this.sendToClipboardToolStripMenuItem1.Name = "sendToClipboardToolStripMenuItem1";
            this.sendToClipboardToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.sendToClipboardToolStripMenuItem1.Text = "Send to Clipboard";
            this.sendToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.sendToClipboardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 6);
            // 
            // grayscaleToolStripMenuItem1
            // 
            this.grayscaleToolStripMenuItem1.Name = "grayscaleToolStripMenuItem1";
            this.grayscaleToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.grayscaleToolStripMenuItem1.Text = "Grayscale";
            this.grayscaleToolStripMenuItem1.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // middleCrosslinesToolStripMenuItem1
            // 
            this.middleCrosslinesToolStripMenuItem1.Name = "middleCrosslinesToolStripMenuItem1";
            this.middleCrosslinesToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.middleCrosslinesToolStripMenuItem1.Text = "Middle Crosslines";
            this.middleCrosslinesToolStripMenuItem1.Click += new System.EventHandler(this.middleCrosslinesToolStripMenuItem_Click);
            // 
            // pb_Crop
            // 
            this.pb_Crop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Crop.Location = new System.Drawing.Point(0, 0);
            this.pb_Crop.Name = "pb_Crop";
            this.pb_Crop.Size = new System.Drawing.Size(365, 371);
            this.pb_Crop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Crop.TabIndex = 0;
            this.pb_Crop.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(789, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openFromClipboardToolStripMenuItem,
            this.toolStripMenuItem5,
            this.saveAsToolStripMenuItem,
            this.sendToClipboardToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openFromClipboardToolStripMenuItem
            // 
            this.openFromClipboardToolStripMenuItem.Name = "openFromClipboardToolStripMenuItem";
            this.openFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openFromClipboardToolStripMenuItem.Text = "Open from Clipboard";
            this.openFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.openFromClipboardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(184, 6);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.saveAsToolStripMenuItem.Text = "SaveAs";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // sendToClipboardToolStripMenuItem
            // 
            this.sendToClipboardToolStripMenuItem.Name = "sendToClipboardToolStripMenuItem";
            this.sendToClipboardToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.sendToClipboardToolStripMenuItem.Text = "Send to Clipboard";
            this.sendToClipboardToolStripMenuItem.Click += new System.EventHandler(this.sendToClipboardToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayscaleToolStripMenuItem,
            this.middleCrosslinesToolStripMenuItem,
            this.toolStripMenuItem3,
            this.selectAllToolStripMenuItem,
            this.clearSelectionToolStripMenuItem,
            this.selectionStyleToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // middleCrosslinesToolStripMenuItem
            // 
            this.middleCrosslinesToolStripMenuItem.Name = "middleCrosslinesToolStripMenuItem";
            this.middleCrosslinesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.middleCrosslinesToolStripMenuItem.Text = "Middle Crosslines";
            this.middleCrosslinesToolStripMenuItem.Click += new System.EventHandler(this.middleCrosslinesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 6);
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.clearSelectionToolStripMenuItem.Text = "Clear Selection";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // selectionStyleToolStripMenuItem
            // 
            this.selectionStyleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boxAndDiagonlsToolStripMenuItem,
            this.boxAndOrthoToolStripMenuItem});
            this.selectionStyleToolStripMenuItem.Name = "selectionStyleToolStripMenuItem";
            this.selectionStyleToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.selectionStyleToolStripMenuItem.Text = "Selection style";
            // 
            // boxAndDiagonlsToolStripMenuItem
            // 
            this.boxAndDiagonlsToolStripMenuItem.Name = "boxAndDiagonlsToolStripMenuItem";
            this.boxAndDiagonlsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.boxAndDiagonlsToolStripMenuItem.Text = "Box and diagonals";
            this.boxAndDiagonlsToolStripMenuItem.Click += new System.EventHandler(this.boxAndDiagonlsToolStripMenuItem_Click);
            // 
            // boxAndOrthoToolStripMenuItem
            // 
            this.boxAndOrthoToolStripMenuItem.Name = "boxAndOrthoToolStripMenuItem";
            this.boxAndOrthoToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.boxAndOrthoToolStripMenuItem.Text = "Box and middle ortho axis";
            this.boxAndOrthoToolStripMenuItem.Click += new System.EventHandler(this.boxAndOrthoToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tSSL_X,
            this.toolStripStatusLabel3,
            this.tSSL_Y,
            this.toolStripStatusLabel5,
            this.tSSL_ImageScale,
            this.toolStripStatusLabel6,
            this.tSSL_SelectionMidPosX,
            this.toolStripStatusLabel7,
            this.tSSL_SelectionMidPosY,
            this.toolStripStatusLabel2,
            this.tSSL_SelectionWidth,
            this.toolStripStatusLabel4,
            this.tSSL_SelectionHeight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 397);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(789, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.MouseEnter += new System.EventHandler(this.statusStrip1_MouseEnter);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(14, 19);
            this.toolStripStatusLabel1.Text = "X";
            // 
            // tSSL_X
            // 
            this.tSSL_X.AutoSize = false;
            this.tSSL_X.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSSL_X.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSSL_X.Name = "tSSL_X";
            this.tSSL_X.Size = new System.Drawing.Size(50, 19);
            this.tSSL_X.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(14, 19);
            this.toolStripStatusLabel3.Text = "Y";
            // 
            // tSSL_Y
            // 
            this.tSSL_Y.AutoSize = false;
            this.tSSL_Y.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSSL_Y.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSSL_Y.Name = "tSSL_Y";
            this.tSSL_Y.Size = new System.Drawing.Size(50, 19);
            this.tSSL_Y.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(34, 19);
            this.toolStripStatusLabel5.Text = "Scale";
            // 
            // tSSL_ImageScale
            // 
            this.tSSL_ImageScale.AutoSize = false;
            this.tSSL_ImageScale.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSSL_ImageScale.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSSL_ImageScale.Name = "tSSL_ImageScale";
            this.tSSL_ImageScale.Size = new System.Drawing.Size(50, 19);
            this.tSSL_ImageScale.Text = "1";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(59, 19);
            this.toolStripStatusLabel6.Text = "sMidPosX";
            // 
            // tSSL_SelectionMidPosX
            // 
            this.tSSL_SelectionMidPosX.AutoSize = false;
            this.tSSL_SelectionMidPosX.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSSL_SelectionMidPosX.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSSL_SelectionMidPosX.DoubleClickEnabled = true;
            this.tSSL_SelectionMidPosX.Name = "tSSL_SelectionMidPosX";
            this.tSSL_SelectionMidPosX.Size = new System.Drawing.Size(50, 19);
            this.tSSL_SelectionMidPosX.Text = "0";
            this.tSSL_SelectionMidPosX.ToolTipText = "Selection middle point x";
            this.tSSL_SelectionMidPosX.MouseEnter += new System.EventHandler(this.tSSL_SelectionMidPosX_MouseEnter);
            this.tSSL_SelectionMidPosX.MouseLeave += new System.EventHandler(this.tSSL_SelectionMidPosX_MouseLeave);
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(59, 19);
            this.toolStripStatusLabel7.Text = "sMidPosY";
            // 
            // tSSL_SelectionMidPosY
            // 
            this.tSSL_SelectionMidPosY.AutoSize = false;
            this.tSSL_SelectionMidPosY.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSSL_SelectionMidPosY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSSL_SelectionMidPosY.Name = "tSSL_SelectionMidPosY";
            this.tSSL_SelectionMidPosY.Size = new System.Drawing.Size(50, 19);
            this.tSSL_SelectionMidPosY.Text = "0";
            this.tSSL_SelectionMidPosY.ToolTipText = "Selection middle y";
            this.tSSL_SelectionMidPosY.MouseEnter += new System.EventHandler(this.tSSL_SelectionMidPosY_MouseEnter);
            this.tSSL_SelectionMidPosY.MouseLeave += new System.EventHandler(this.tSSL_SelectionMidPosY_MouseLeave);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(44, 19);
            this.toolStripStatusLabel2.Text = "sWidth";
            // 
            // tSSL_SelectionWidth
            // 
            this.tSSL_SelectionWidth.AutoSize = false;
            this.tSSL_SelectionWidth.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSSL_SelectionWidth.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSSL_SelectionWidth.Name = "tSSL_SelectionWidth";
            this.tSSL_SelectionWidth.Size = new System.Drawing.Size(50, 19);
            this.tSSL_SelectionWidth.Text = "0";
            this.tSSL_SelectionWidth.ToolTipText = "Selection width";
            this.tSSL_SelectionWidth.MouseEnter += new System.EventHandler(this.tSSL_SelectionWidth_MouseEnter);
            this.tSSL_SelectionWidth.MouseLeave += new System.EventHandler(this.tSSL_SelectionWidth_MouseLeave);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(48, 19);
            this.toolStripStatusLabel4.Text = "sHeight";
            // 
            // tSSL_SelectionHeight
            // 
            this.tSSL_SelectionHeight.AutoSize = false;
            this.tSSL_SelectionHeight.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tSSL_SelectionHeight.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSSL_SelectionHeight.Name = "tSSL_SelectionHeight";
            this.tSSL_SelectionHeight.Size = new System.Drawing.Size(50, 19);
            this.tSSL_SelectionHeight.Text = "0";
            this.tSSL_SelectionHeight.ToolTipText = "Selection height";
            this.tSSL_SelectionHeight.MouseEnter += new System.EventHandler(this.tSSL_SelectionHeight_MouseEnter);
            this.tSSL_SelectionHeight.MouseLeave += new System.EventHandler(this.tSSL_SelectionHeight_MouseLeave);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem1
            // 
            this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
            this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.selectAllToolStripMenuItem1.Text = "Select All";
            this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 421);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "MainForm";
            this.Text = "myPhotoEditor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip_OriginalSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_OriginalSensor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_CropSensor)).EndInit();
            this.contextMenuStrip_CropSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Crop)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pb_Original;
        private System.Windows.Forms.PictureBox pb_Crop;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_X;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_Y;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_SelectionWidth;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_SelectionHeight;
        private Sensor pb_OriginalSensor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_ImageScale;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_OriginalSide;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_CropSide;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.PictureBox pb_CropSensor;
        private System.Windows.Forms.ToolStripMenuItem middleCrosslinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem middleCrosslinesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectionStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxAndDiagonlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxAndOrthoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem boxAndDiagonalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxAndMiddleOrthoAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_SelectionMidPosX;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_SelectionMidPosY;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem openFromBufferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendToClipboardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem sendToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    }
}

