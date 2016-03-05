using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using DrawingModel;
using System.IO;

namespace DrawingForm.PresentationModel
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        Graphics _graphicTemporary;
        const int BALL_SIZE = 200;
        const int MOVE_SIZE = 100;
        const int FIFTY_FIVE = 55;
        const int FIFTY = 50;
        const int FORTY_FIVE = 45;
        const int TEN = 10;
        const int NINETY = 90;
        const int BITMAP_WIDTH = 1366;
        const int BITMAP_HEIGHT = 768;
        const int TWO = 2;
        const int ZERO = 0;
        Bitmap _bitmap;

        //以下為檔案管理
        string _projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            _bitmap = new Bitmap(BITMAP_WIDTH, BITMAP_HEIGHT);
            _graphicTemporary = Graphics.FromImage(_bitmap);
            this._graphics = graphics;
        }
        public void ClearAll()//清空頁面
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        public void DrawCircle(int circlePositionX, int circlePositionY, int circleWidth, int circleHeight, RedGreenBlueAlpha redGreenBlueAlpha) //畫圓形
        {
            _graphicTemporary.FillEllipse(new SolidBrush(Color.FromArgb(redGreenBlueAlpha.Red, redGreenBlueAlpha.Green, redGreenBlueAlpha.Blue)), circlePositionX, circlePositionY, circleWidth, circleHeight);
            _graphics.DrawImage(_bitmap, new Point(ZERO, ZERO));
            //Getbmp();            
        }

        public void DrawRectangle(int rectanglePositionX, int rectanglePositionY, int rectangleWidth, int rectangleHeight, RedGreenBlueAlpha redGreenBlueAlpha) //畫圓形
        {
            _graphicTemporary.FillRectangle(new SolidBrush(Color.FromArgb(redGreenBlueAlpha.Red,redGreenBlueAlpha.Green,redGreenBlueAlpha.Blue)), rectanglePositionX, rectanglePositionY, rectangleWidth, rectangleHeight);            
            _graphics.DrawImage(_bitmap, new Point(ZERO, ZERO));
            //Getbmp();
        }

        public void DrawSmile(int smilePositionX, int smilePositionY) //畫圓形;
        {
            _graphicTemporary.FillEllipse(Brushes.Yellow, smilePositionX - MOVE_SIZE + MOVE_SIZE, smilePositionY - MOVE_SIZE + MOVE_SIZE, BALL_SIZE, BALL_SIZE);
            _graphicTemporary.FillEllipse(Brushes.White, smilePositionX - FIFTY_FIVE + MOVE_SIZE, smilePositionY - FORTY_FIVE + MOVE_SIZE, TEN, TEN);
            _graphicTemporary.FillEllipse(Brushes.White, smilePositionX + FORTY_FIVE + MOVE_SIZE, smilePositionY - FORTY_FIVE + MOVE_SIZE, TEN, TEN);
            _graphicTemporary.DrawEllipse(Pens.Black, smilePositionX - FIFTY_FIVE + MOVE_SIZE, smilePositionY - FORTY_FIVE + MOVE_SIZE, TEN, TEN);
            _graphicTemporary.DrawEllipse(Pens.Black, smilePositionX + FORTY_FIVE + MOVE_SIZE, smilePositionY - FORTY_FIVE + MOVE_SIZE, TEN, TEN);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(smilePositionX - FIFTY + MOVE_SIZE, smilePositionY - FIFTY + MOVE_SIZE, MOVE_SIZE, MOVE_SIZE);
            _graphicTemporary.DrawArc(Pens.Black, rectangle, FORTY_FIVE, NINETY);
            _graphics.DrawImage(_bitmap, new Point(ZERO, ZERO));
            //Getbmp();
        }

        public void DrawFrame(int framePositionX, int framePositionY, int frameWidth, int frameHeight) //畫圓形;
        {
            _graphicTemporary.DrawRectangle(Pens.Red, framePositionX, framePositionY, frameWidth, frameHeight);
            _graphics.DrawImage(_bitmap, new Point(ZERO, ZERO));
            //Getbmp();
        }

        public Bitmap GetBitmap()//回傳Bitmap
        {
            return _bitmap;
        }
    }
}
