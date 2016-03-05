using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Smile : ShapeType
    {
        const string SMILE_TYPE = "Smile";
        const string SIGN = ";";
        const int SIZE = 200;
        const int TWO = 2;
        const int LEFT_ON = 1;
        const int LEFT_DOWN = 2;
        const int RIGHT_ON = 3;
        const int RIGHT_DOWN = 4;

        public int PositionX
        {
            get;
            set;
        }
        public int PositionY
        {
            get;
            set;
        }

        public int Width
        {
            get 
            {
                return SIZE;
            }
            set
            {
                //Width = SIZE;
            }
        }

        public int Height
        {
            get
            {
                return SIZE;
            }
            set
            {
                //Height = SIZE;
            }
        }

        public void Draw(IGraphics graphics) //將圓形畫出來
        {
            graphics.DrawSmile(PositionX, PositionY);
        }

        public string _shapeType
        {
            get
            {
                return SMILE_TYPE;
            }
        }

        public string GetShapeInformation()//回傳由Shape座標、Type所組成的字串
        {
            return _shapeType + SIGN + PositionX + SIGN + PositionY;
        }

        public bool IsInShape(int positionX, int positionY)//回傳由Shape座標、Type所組成的字串
        {
            //positionX += SIZE / TWO;
            //positionY += SIZE / TWO;
            if (positionX > PositionX && positionX < PositionX + SIZE && positionY > PositionY && positionY < PositionY + SIZE)
                return true;
            return false;
        }

        public RedGreenBlueAlpha redGreenBlueAlpha
        {
            get;
            set;
        }

        public int FindPosition(int positionX, int positionY)//點選哪個角落
        {
            const int DISTANCE = 4;
            if (Math.Abs(positionX - PositionX) < DISTANCE && Math.Abs(positionY - PositionY) < DISTANCE)
            {                
                return LEFT_ON;
            }
            else if (Math.Abs(positionX - (PositionX + Width)) < DISTANCE && Math.Abs(positionY - PositionY) < DISTANCE)
            {                
                return RIGHT_ON;
            }
            else if (Math.Abs(positionX - PositionX) < DISTANCE && Math.Abs(positionY - (PositionY + Height)) < DISTANCE)
            {                
                return LEFT_DOWN;
            }
            else if (Math.Abs(positionX - (PositionX + Width)) < DISTANCE && Math.Abs(positionY - (PositionY + Height)) < DISTANCE)
            {                
                return RIGHT_DOWN;
            }
            else 
            return 0;
        }

        public void Resize(int position, int positionX, int positionY)//重新定義大小

        {
            switch (position)
            {
                case LEFT_ON:
                    Width = Width + PositionX - positionX;
                    Height = Height + PositionY - positionY;
                    PositionX = positionX;
                    PositionY = positionY;
                    break;
                case RIGHT_ON:
                    Width = positionX - PositionX;
                    Height = Height + PositionY - positionY;
                    PositionY = positionY;
                    break;
                case LEFT_DOWN:
                    Width = Width + PositionX - positionX;
                    Height = positionY - PositionY;
                    PositionX = positionX;
                    break;
                case RIGHT_DOWN:
                    Width = positionX - PositionX;
                    Height = positionY - PositionY;
                    break;
            }
        }
    }
}
