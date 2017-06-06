namespace myPhotoEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pb_Original = new System.Windows.Forms.PictureBox();
            this.pb_Selection = new System.Windows.Forms.PictureBox();
            this.pb_Crop = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_X = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_Y = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_ImageScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_SelectionWidth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSSL_SelectionHeight = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Selection)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.pb_Original);
            this.splitContainer1.Panel1.Controls.Add(this.pb_Selection);
            this.splitContainer1.Panel1.DoubleClick += new System.EventHandler(this.splitContainer1_Panel1_DoubleClick);
            this.splitContainer1.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseClick);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pb_Crop);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(789, 373);
            this.splitContainer1.SplitterDistance = 418;
            this.splitContainer1.TabIndex = 0;
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
            // pb_Selection
            // 
            this.pb_Selection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_Selection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Selection.Location = new System.Drawing.Point(0, 0);
            this.pb_Selection.Name = "pb_Selection";
            this.pb_Selection.Size = new System.Drawing.Size(416, 371);
            this.pb_Selection.TabIndex = 1;
            this.pb_Selection.TabStop = false;
            this.pb_Selection.DoubleClick += new System.EventHandler(this.pb_Selection_DoubleClick);
            this.pb_Selection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_Selection_MouseClick);
            this.pb_Selection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_Selection_MouseDown);
            this.pb_Selection.MouseEnter += new System.EventHandler(this.pb_Selection_MouseEnter);
            this.pb_Selection.MouseLeave += new System.EventHandler(this.pb_Selection_MouseLeave);
            this.pb_Selection.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_Selection_MouseMove);
            this.pb_Selection.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_Selection_MouseUp);
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
            this.fileToolStripMenuItem});
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
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.saveAsToolStripMenuItem.Text = "SaveAs";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.closeToolStripMenuItem.Text = "Close";
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
            this.toolStripStatusLabel2,
            this.tSSL_SelectionWidth,
            this.toolStripStatusLabel4,
            this.tSSL_SelectionHeight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 397);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(789, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
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
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 421);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "MainForm";
            this.Text = "myPhotoEditor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Selection)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_X;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_Y;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_SelectionWidth;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_SelectionHeight;
        private System.Windows.Forms.PictureBox pb_Selection;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_ImageScale;
    }
}

