using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingForm
{
    public partial class Form1 : Form
    {
        //
        string _shapeType = "";
        const string CIRCLE_TYPE = "Circle";
        const string LINE_TYPE = "Line";
        const string RECTANGLE_TYPE = "Rectangle";
        const string SMILE_TYPE = "Smile";
        //
        DrawingModel.Model _model;
        PresentationModel.PresentationModel _presentationModel;
        Panel _canvas = new DoubleBufferedPanel();
        
        public Form1()
        {
            InitializeComponent();
            Initialize();            
            // prepare canvas
            //
            _canvas.Dock = DockStyle.Fill;
            _canvas.BackColor = System.Drawing.Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoved;
            _canvas.Paint += HandleCanvasPaint;
            Controls.Add(_canvas);                        
            //
            _model = new DrawingModel.Model();
            // prepare presentation model and model
            //
            _presentationModel = new PresentationModel.PresentationModel(_model, _canvas);
            _model._modelChanged += HandleModelChanged;
            _model._redGreenBlueAlpha = new DrawingModel.RedGreenBlueAlpha(0,0,0,255);
            _presentationModel.DownloadImageData();
            //
            RefreshUI();
        }

        public void Initialize()//初始化
        {
            this.Size = new Size(1366, 600);
            InitializeMenuStrip();
            InitializeToolStrip();            
        }

        public void InitializeMenuStrip()//初始化 MenuStrip
        {
            _saveToolStripMenuItem.Click += HandleSaveButtonClick;
            _exitToolStripMenuItem.Click += ClickExit;
        }

        public void InitializeToolStrip()//初始化 ToolStrip
        {
            _circleToolStripButton.Click += ClickCircle;
            _rectangleToolStripButton.Click += ClickRectangle;            
            _smileToolStripButton.Click += ClickSmile;
            _redoToolStripButton.Click += ClickRedo;
            _undoToolStripButton.Click += ClickUndo;
            _clearToolStripButton.Click += HandleClearButtonClick;
            _deleteToolStripButton.Click += ClickDeleteToolStripButton;
            _saveToolStripButton.Click += HandleSaveButtonClick;
            _colorToolStripButton.Click += ClickColorToolStripButton;
        }

        public void HandleSaveButtonClick(object sender, EventArgs e)//按下儲存Button
        {
            _presentationModel.WriteImageData();
            _presentationModel.SaveImage();
            _presentationModel.UploadImageData();
            _presentationModel.UploadImage();
            RefreshUI();
        }

        public void HandleClearButtonClick(object sender, System.EventArgs e)//按下清空Button
        {
            _model.Clear();            
        }

        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)//按下Canvas
        {
            _model.PressPointer(e.X, e.Y, _shapeType);
            RefreshUI();
        }

        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)//放開Canvas
        {
            _model.ReleasePointer(e.X, e.Y, _shapeType);
            RefreshUI();
        }

        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)//移動Canvas
        {
            _model.MovePointer(e.X, e.Y, _shapeType);
            RefreshUI();
        }

        public void HandleCanvasPaint(object sender, System.Windows.Forms.PaintEventArgs e)//Canvas畫畫
        {            
            _presentationModel.Draw(e.Graphics);
        }

        public void HandleModelChanged()//model變化
        {
            RefreshUI();
            Invalidate(true);
        }

        public void ClickExit(object sender, EventArgs e)//離開程式
        {            
            this.Close();
        }

        public void ClickCircle(object sender, EventArgs e)//選擇圓形
        {
            _shapeType = CIRCLE_TYPE;
        }

        public void ClickRectangle(object sender, EventArgs e)//選擇方形
        {
            _shapeType = RECTANGLE_TYPE;
        }        
        
        public void ClickSmile(object sender, EventArgs e)//選擇笑臉
        {
            _shapeType = SMILE_TYPE;  
        }

        public void ClickUndo(object sender, EventArgs e)//undo
        {
            _model.Undo();
            RefreshUI();
        }

        public void ClickRedo(object sender, EventArgs e)//redo
        {
            _model.Redo();
            RefreshUI();
        }

        public void ClickDeleteToolStripButton(object sender, EventArgs e)//點選刪除
        {
            _model.DeleteTarget();
            RefreshUI();
        }

        public void ClickColorToolStripButton(object sender, EventArgs e)//選擇顏色
        {
            ColorDialog myDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            myDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            myDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.            

            // Update the text box color if the user clicks OK 
            if (myDialog.ShowDialog() == DialogResult.OK)
                _model._redGreenBlueAlpha = new DrawingModel.RedGreenBlueAlpha(myDialog.Color.R, myDialog.Color.G, myDialog.Color.B, myDialog.Color.A);
            _model.ChangeTargetColor();
        }

        void RefreshUI()// 更新redo與undo是否為enabled
        {
            _redoToolStripButton.Enabled = _model.IsRedoEnabled;
            _undoToolStripButton.Enabled = _model.IsUndoEnabled;
            _deleteToolStripButton.Enabled = _model.IsDeleteEnabled;
            Invalidate();
        }
    }
}