using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using DrawingModel;
using Windows.Foundation;

namespace DrawingApp.PresentationModel
{
    class WindowsStoreGraphicsAdaptor : IGraphics
    {
        private const int BALL_SIZE = 200;
        private const int ONE_HUNDRED = 100;
        private const int ONE_HUNDRED_FORTY_FIVE = 145;        
        const int FIFTY_FIVE = 55;
        const int FIFTY = 50;
        const int FORTY_FIVE = 45;
        const int TEN = 10;
        const int THIRTY = 30;
        const int THREE_HUNDRED = 300;
        Canvas _canvas;

        public WindowsStoreGraphicsAdaptor(Canvas canvas)
        {
            this._canvas = canvas;
        }

        public void ClearAll()//清空頁面
        {
            _canvas.Children.Clear();
        }

        public void DrawCircle(int circlePositionX, int circlePositionY, int circleWidth, int circleHeight, RedGreenBlueAlpha redGreenBlueAlpha) //畫圓形
        {
            Windows.UI.Xaml.Shapes.Ellipse circle = new Windows.UI.Xaml.Shapes.Ellipse();
            InitializeShape(circle, circlePositionX, circlePositionY, circleWidth, circleHeight, new SolidColorBrush(Color.FromArgb((byte)redGreenBlueAlpha.Alpha, (byte)redGreenBlueAlpha.Red, (byte)redGreenBlueAlpha.Green, (byte)redGreenBlueAlpha.Blue)));
            _canvas.Children.Add(circle);
        }

        public void DrawRectangle(int rectanglePositionX, int rectanglePositionY, int rectangleWidth, int rectangleHeight, RedGreenBlueAlpha redGreenBlueAlpha) //畫圓形
        {
            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            InitializeShape(rectangle, rectanglePositionX, rectanglePositionY, rectangleWidth, rectangleHeight, new SolidColorBrush(Color.FromArgb((byte)redGreenBlueAlpha.Alpha, (byte)redGreenBlueAlpha.Red, (byte)redGreenBlueAlpha.Green, (byte)redGreenBlueAlpha.Blue)));
            _canvas.Children.Add(rectangle);
        }

        public void DrawSmile(int smilePositionX, int smilePositionY)//畫笑臉
        {
            _canvas.Children.Add(InitializeShape(new Windows.UI.Xaml.Shapes.Ellipse(), smilePositionX - ONE_HUNDRED, smilePositionY - ONE_HUNDRED, BALL_SIZE, BALL_SIZE, new SolidColorBrush(Colors.Yellow)));
            _canvas.Children.Add(InitializeShape(new Windows.UI.Xaml.Shapes.Ellipse(), smilePositionX + FORTY_FIVE - ONE_HUNDRED, smilePositionY + FIFTY_FIVE - ONE_HUNDRED, TEN, TEN, new SolidColorBrush(Colors.Black)));
            _canvas.Children.Add(InitializeShape(new Windows.UI.Xaml.Shapes.Ellipse(), smilePositionX + ONE_HUNDRED_FORTY_FIVE - ONE_HUNDRED, smilePositionY + FIFTY_FIVE - ONE_HUNDRED, TEN, TEN, new SolidColorBrush(Colors.Black)));
            _canvas.Children.Add(CreateArcSegment(smilePositionX, smilePositionY));             
        }

        public void DrawFrame(int framePositionX, int framePositionY, int frameWidth, int frameHeight) //畫圓形;
        {
            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            InitializeFrame(rectangle, framePositionX, framePositionY, frameWidth, frameHeight, new SolidColorBrush(Colors.Orange));
            _canvas.Children.Add(rectangle);
            //Getbmp();
        }

        private Path CreateArcSegment(int smilePositionX, int smilePositionY)//畫弧度
        {
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(smilePositionX - FIFTY, smilePositionY + FIFTY);

            ArcSegment arcSegment = new ArcSegment();
            arcSegment.Point = new Point(smilePositionX + FIFTY, smilePositionY + FIFTY);
            arcSegment.Size = new Size(FIFTY, ONE_HUNDRED);
            arcSegment.IsLargeArc = false;
            arcSegment.SweepDirection = SweepDirection.Counterclockwise;
            arcSegment.RotationAngle = ONE_HUNDRED - TEN;

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(arcSegment);

            pathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection pathFigureCollection = new PathFigureCollection();
            pathFigureCollection.Add(pathFigure);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures = pathFigureCollection;

            Path arcPath = new Path();
            arcPath.Stroke = new SolidColorBrush(Colors.Black);
            arcPath.StrokeThickness = 1;
            arcPath.Data = pathGeometry;
            return arcPath;
        }

        private Shape InitializeShape(Shape shape, int left, int top, int right, int bottom, SolidColorBrush fillColorBrush)//初始化所有圖形
        {
            shape.Margin = new Windows.UI.Xaml.Thickness(left, top, right, bottom);
            shape.Width = right;
            shape.Height = bottom;
            shape.Fill = fillColorBrush;
            return shape;
        }

        private Shape InitializeFrame(Shape shape, int left, int top, int right, int bottom, SolidColorBrush fillColorBrush)//初始化所有圖形
        {
            shape.Margin = new Windows.UI.Xaml.Thickness(left, top, right, bottom);
            shape.Width = right;
            shape.Height = bottom;
            shape.Stroke = fillColorBrush;
            return shape;
        }
    }
}