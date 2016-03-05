using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    interface ShapeType //抽象類別
    {
        //畫圖
        void Draw(IGraphics graphics);

        //取得圖形的詳細資訊
        string GetShapeInformation();

        //取得圖形的類型
        string _shapeType 
        { 
            get; 
        }

        //X座標
        int PositionX
        {
            get;
            set;
        }

        //Y座標
        int PositionY
        {
            get;
            set;
        }

        int Width
        {
            get;
            set;
        }

        int Height
        {
            get;
            set;
        }

        //是否選取圖形
        bool IsInShape(int positionX, int positionY);

        RedGreenBlueAlpha redGreenBlueAlpha
        {
            get;
            set;
        }

        //點選哪個角落
        int FindPosition(int positionX, int positionY);

        //重新定義大小
        void Resize(int position, int positionX, int positionY);
    }
}
