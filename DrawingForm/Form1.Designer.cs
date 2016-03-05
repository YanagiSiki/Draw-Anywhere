namespace DrawingForm
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._toolStripToolStrip = new System.Windows.Forms.ToolStrip();
            this._rectangleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._circleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._smileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._undoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._redoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._fileMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this._saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._colorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._toolStripToolStrip.SuspendLayout();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStripToolStrip
            // 
            this._toolStripToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._toolStripToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._rectangleToolStripButton,
            this._circleToolStripButton,
            this._smileToolStripButton,
            this._toolStripSeparator1,
            this._deleteToolStripButton,
            this._clearToolStripButton,
            this._toolStripSeparator2,
            this._undoToolStripButton,
            this._redoToolStripButton,
            this._toolStripSeparator3,
            this._colorToolStripButton,
            this._saveToolStripButton});
            this._toolStripToolStrip.Location = new System.Drawing.Point(0, 24);
            this._toolStripToolStrip.Name = "_toolStripToolStrip";
            this._toolStripToolStrip.Size = new System.Drawing.Size(666, 25);
            this._toolStripToolStrip.TabIndex = 3;
            this._toolStripToolStrip.Text = "toolStrip1";
            // 
            // _rectangleToolStripButton
            // 
            this._rectangleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_rectangleToolStripButton.Image")));
            this._rectangleToolStripButton.ImageTransparentColor = System.Drawing.Color.LimeGreen;
            this._rectangleToolStripButton.Name = "_rectangleToolStripButton";
            this._rectangleToolStripButton.Size = new System.Drawing.Size(84, 22);
            this._rectangleToolStripButton.Text = "Rectangle";
            // 
            // _circleToolStripButton
            // 
            this._circleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_circleToolStripButton.Image")));
            this._circleToolStripButton.ImageTransparentColor = System.Drawing.Color.LimeGreen;
            this._circleToolStripButton.Name = "_circleToolStripButton";
            this._circleToolStripButton.Size = new System.Drawing.Size(58, 22);
            this._circleToolStripButton.Text = "Circle";
            // 
            // _smileToolStripButton
            // 
            this._smileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_smileToolStripButton.Image")));
            this._smileToolStripButton.ImageTransparentColor = System.Drawing.Color.LimeGreen;
            this._smileToolStripButton.Name = "_smileToolStripButton";
            this._smileToolStripButton.Size = new System.Drawing.Size(58, 22);
            this._smileToolStripButton.Text = "Smile";
            // 
            // toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _deleteToolStripButton
            // 
            this._deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_deleteToolStripButton.Image")));
            this._deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._deleteToolStripButton.Name = "_deleteToolStripButton";
            this._deleteToolStripButton.Size = new System.Drawing.Size(64, 22);
            this._deleteToolStripButton.Text = "Delete";
            // 
            // _clearToolStripButton
            // 
            this._clearToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_clearToolStripButton.Image")));
            this._clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearToolStripButton.Name = "_clearToolStripButton";
            this._clearToolStripButton.Size = new System.Drawing.Size(56, 22);
            this._clearToolStripButton.Text = "Clear";
            // 
            // toolStripSeparator2
            // 
            this._toolStripSeparator2.Name = "toolStripSeparator2";
            this._toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _undoToolStripButton
            // 
            this._undoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_undoToolStripButton.Image")));
            this._undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._undoToolStripButton.Name = "_undoToolStripButton";
            this._undoToolStripButton.Size = new System.Drawing.Size(57, 22);
            this._undoToolStripButton.Text = "undo";
            // 
            // _redoToolStripButton
            // 
            this._redoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_redoToolStripButton.Image")));
            this._redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._redoToolStripButton.Name = "_redoToolStripButton";
            this._redoToolStripButton.Size = new System.Drawing.Size(58, 22);
            this._redoToolStripButton.Text = "Redo";
            // 
            // toolStripSeparator3
            // 
            this._toolStripSeparator3.Name = "toolStripSeparator3";
            this._toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _saveToolStripButton
            // 
            this._saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveToolStripButton.Image")));
            this._saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveToolStripButton.Name = "_saveToolStripButton";
            this._saveToolStripButton.Size = new System.Drawing.Size(112, 22);
            this._saveToolStripButton.Text = "SaveToGoogle";
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuStrip});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(739, 24);
            this._menuStrip.TabIndex = 2;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _fileMenuStrip
            // 
            this._fileMenuStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveToolStripMenuItem,
            this._exitToolStripMenuItem});
            this._fileMenuStrip.Name = "_fileMenuStrip";
            this._fileMenuStrip.Size = new System.Drawing.Size(74, 20);
            this._fileMenuStrip.Text = "File Menu";
            // 
            // _saveToolStripMenuItem
            // 
            this._saveToolStripMenuItem.Name = "_saveToolStripMenuItem";
            this._saveToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this._saveToolStripMenuItem.Text = "SaveToGoogle";
            // 
            // _exitToolStripMenuItem
            // 
            this._exitToolStripMenuItem.Name = "_exitToolStripMenuItem";
            this._exitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this._exitToolStripMenuItem.Text = "Exit";
            // 
            // _colorToolStripButton
            // 
            this._colorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_colorToolStripButton.Image")));
            this._colorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._colorToolStripButton.Name = "_colorToolStripButton";
            this._colorToolStripButton.Size = new System.Drawing.Size(58, 22);
            this._colorToolStripButton.Text = "Color";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 407);
            this.Controls.Add(this._toolStripToolStrip);
            this.Controls.Add(this._menuStrip);
            this.Name = "Form1";
            this.Text = "Form1";
            this._toolStripToolStrip.ResumeLayout(false);
            this._toolStripToolStrip.PerformLayout();
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStripToolStrip;
        private System.Windows.Forms.ToolStripButton _circleToolStripButton;
        private System.Windows.Forms.ToolStripButton _clearToolStripButton;
        private System.Windows.Forms.ToolStripButton _saveToolStripButton;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _fileMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _rectangleToolStripButton;
        private System.Windows.Forms.ToolStripButton _redoToolStripButton;
        private System.Windows.Forms.ToolStripButton _undoToolStripButton;
        private System.Windows.Forms.ToolStripButton _smileToolStripButton;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _deleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _colorToolStripButton;
    }
}

