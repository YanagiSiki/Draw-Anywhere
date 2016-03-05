using DrawingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    class Adaptor : IGraphics
    {
        public void ClearAll()//清空頁面
        {
        }

        public void DrawSmile(int smilePositionX, int smilePositionY)//畫笑臉
        {            
        }

        public void DrawCircle(int circlePositionX, int circlePositionY, int circleWidth, int circleHeight, RedGreenBlueAlpha redGreenBlueAlpha) //畫圓形
        {              
        }

        public void DrawRectangle(int rectanglePositionX, int rectanglePositionY, int rectangleWidth, int rectangleHeight, RedGreenBlueAlpha redGreenBlueAlpha) //畫圓形
        {            
        }

        public void DrawFrame(int framePositionX, int framePositionY, int frameWidth, int frameHeight) //畫圓形;
        {            
        }        
    }
}
