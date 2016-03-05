using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Line : ShapeType
    {
        const string LINE_TYPE = "Line";
        const string SIGN = ";";

        public double _linePositionXOne
        {
            get;
            set;
        }
        public double _linePositionYOne
        {
            get;
            set;
        }
        public double _linePositionXTwo
        {
            get;
            set;
        }
        public double _linePositionYTwo
        {
            get;
            set;
        }

        public void Draw(IGraphics graphics)//畫線
        {
            graphics.DrawLine(_linePositionXOne, _linePositionYOne, _linePositionXTwo, _linePositionYTwo);
        }

        public string _shapeType
        {
            get
            {
                return LINE_TYPE;
            }
        }

        public string GetShapeInformation()//回傳由Shape座標、Type所組成的字串
        {
            return LINE_TYPE + SIGN + _linePositionXOne + SIGN + _linePositionYOne + SIGN + _linePositionXTwo + SIGN + _linePositionYTwo;
        }

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
    }
}
