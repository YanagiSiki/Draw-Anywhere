using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class CreateShape
    {
        
        public Circle CreateCircle(double pointerX, double pointerY, double circleWidth, double circleHeight, RedGreenBlueAlpha redGreenBlueAlpha)//創造圓形，加入Shape陣列
        {
            Circle circle = new Circle();
            circle.PositionX = (int)pointerX;
            circle.PositionY = (int)pointerY;
            circle.Width = (int)circleWidth;
            circle.Height = (int)circleHeight;
            circle.redGreenBlueAlpha = redGreenBlueAlpha;
            return circle;
            //_shapes.Add(circle);
        }

        public Rectangle CreateRectangle(double pointerX, double pointerY, double rectangleWidth, double rectangleHeight, RedGreenBlueAlpha redGreenBlueAlpha)//創造圓形，加入Shape陣列
        {
            Rectangle rectangle = new Rectangle();
            rectangle.PositionX = (int)pointerX;
            rectangle.PositionY = (int)pointerY;
            rectangle.Width = (int)rectangleWidth;
            rectangle.Height = (int)rectangleHeight;
            rectangle.redGreenBlueAlpha = redGreenBlueAlpha;
            return rectangle;
            //_shapes.Add(circle);
        }

        public Smile CreateSmile(double pointerX, double pointerY)//創造線，加入Shape陣列
        {
            Smile smile = new Smile();
            smile.PositionX = (int)pointerX;
            smile.PositionY = (int)pointerY;
            return smile;
            //_shapes.Add(hint);
        }

    }
}
