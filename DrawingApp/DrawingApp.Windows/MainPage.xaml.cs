using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 http://go.microsoft.com/fwlink/?LinkId=234238

namespace DrawingApp
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const int CANVAS_WIDTH = 1366;
        const int CANVAS_HEIGHT = 600;
        const int TWO_FIVE_FIVE = 255;
        //
        string _shapeType = "";
        const string CIRCLE_TYPE = "Circle";
        const string LINE_TYPE = "Line";
        const string RECTANGLE_TYPE = "Rectangle";
        const string SMILE_TYPE = "Smile";
        //
        DrawingModel.Model _model;
        PresentationModel.PresentationModel _presentationModel;        

        public MainPage()
        {
            this.InitializeComponent();

            Initialize();
            _model = new DrawingModel.Model();
            _presentationModel = new PresentationModel.PresentationModel(_model, _canvas);
            _canvas.PointerPressed += HandleCanvasPressed;
            _canvas.PointerReleased += HandleCanvasReleased;
            _canvas.PointerMoved += HandleCanvasMoved;
            _model._redGreenBlueAlpha = new DrawingModel.RedGreenBlueAlpha(0, 0, 0, TWO_FIVE_FIVE);
            _model._modelChanged += HandleModelChanged;
            RefreshUI();
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.The Parameter
        /// property is typically used to configure the page.</param>
        /// 
        public void Initialize()//初始化
        {
            _canvas.Width = CANVAS_WIDTH;
            _canvas.Height = CANVAS_HEIGHT;
            InitializeToolStrip();
        }

        public void InitializeToolStrip()//初始化 ToolStrip
        {
            CircleAppBarButton.Click += ClickCircle;
            RectangleAppBarButton.Click += ClickRectangle;
            SmileAppBarButton.Click += ClickSmile;
            UndoAppBarButton.Click += UndoClick;
            RedoAppBarButton.Click += RedoClick;
            ClearAppBarButton.Click += HandleClearButtonClick;
            DeleteAppBarButton.Click += DeleteAppBarButtonClick;
            SaveToGoogleAppBarButton.Click += HandleSaveToGoogleToolStripButtonClick;
            DownloadAppBarButton.Click += DownloadAppBarButtonClick;
            LoadAppBarButton.Click += LoadAppBarButtonClick;
            _setColorButton.Click += SetColorButtonClick;
        }

        public void SetColorButtonClick(object sender, RoutedEventArgs e)//設定顏色
        {
            _model._redGreenBlueAlpha = new DrawingModel.RedGreenBlueAlpha(byte.Parse(_red.Text), byte.Parse(_green.Text), byte.Parse(_blue.Text), TWO_FIVE_FIVE);
            _colorBlank.Background = (new SolidColorBrush(Color.FromArgb(TWO_FIVE_FIVE, byte.Parse(_red.Text), byte.Parse(_green.Text), byte.Parse(_blue.Text))));
            _model.ChangeTargetColor();
        }        
        
        public void LoadAppBarButtonClick(object sender, RoutedEventArgs e)//載入已存檔案
        {
            _presentationModel.ReadImageData();
            _presentationModel.Draw();
        }

        public void DownloadAppBarButtonClick(object sender, RoutedEventArgs e)//下載檔案
        {
            _presentationModel.DownloadImageData();
        }

        public void HandleSaveToGoogleToolStripButtonClick(object sender, RoutedEventArgs e)//按下儲存Button
        {
            _presentationModel.SaveImage();
            _presentationModel.WriteImageData();            

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)//I don't know.
        {
        }

        private void HandleClearButtonClick(object sender, RoutedEventArgs e)//按下清空Button
        {
            _model.Clear();
            RefreshUI();
        }

        public void DeleteAppBarButtonClick(object sender, RoutedEventArgs e)//按下刪除
        {
            _model.DeleteTarget();
            RefreshUI();
        }

        public void HandleCanvasPressed(object sender, PointerRoutedEventArgs e)//按下Canvas
        {
            _model.PressPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, _shapeType);
            RefreshUI();
        }

        public void HandleCanvasReleased(object sender, PointerRoutedEventArgs e)//放開Canvas
        {
            _model.ReleasePointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, _shapeType);
            RefreshUI();
        }

        public void HandleCanvasMoved(object sender, PointerRoutedEventArgs e)//移動Canvas
        {
            _model.MovePointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y, _shapeType);
            RefreshUI();
        }

        public void HandleModelChanged()//Canvas畫畫
        {
            _presentationModel.Draw();
        }

        void RedoClick(object sender, RoutedEventArgs e)//redo
        {
            _model.Redo();
            RefreshUI();
        }

        void UndoClick(object sender, RoutedEventArgs e)//undo
        {
            _model.Undo();
            RefreshUI();
        }

        public void LineClick(object sender, RoutedEventArgs e)//選擇線
        {
            _shapeType = LINE_TYPE;
        }

        public void ClickCircle(object sender, RoutedEventArgs e)//選擇圓形
        {
            _shapeType = CIRCLE_TYPE;
        }

        public void ClickSmile(object sender, RoutedEventArgs e)//選擇笑臉
        {
            _shapeType = SMILE_TYPE;  
        }

        public void ClickRectangle(object sender, RoutedEventArgs e)//選擇方形
        {
            _shapeType = RECTANGLE_TYPE; 
        }

        void RefreshUI()// 更新redo與undo是否為enabled
        {
            RedoAppBarButton.IsEnabled = _model.IsRedoEnabled;
            UndoAppBarButton.IsEnabled = _model.IsUndoEnabled;
        }
    }
}
