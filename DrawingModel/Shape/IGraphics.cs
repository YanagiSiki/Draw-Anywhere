using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    interface IGraphics //IG抽象類別
    {
        //清空
        void ClearAll();

        //畫圓形
        void DrawCircle(int circlePositionX, int circlePositionY, int circleWidth, int circleHeight, RedGreenBlueAlpha redGreenBlueAlpha);

        //畫方形
        void DrawRectangle(int rectanglePositionX, int rectanglePositionY, int rectangleWidth, int rectangleHeight, RedGreenBlueAlpha redGreenBlueAlpha);

        //畫笑臉
        void DrawSmile(int smilePositionX, int smilePositionY);

        //畫外框
        void DrawFrame(int framePositionX, int framePositionY, int frameWidth, int frameHeight);
    }
}