using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Points
    {
        public Points(int positionX, int positionY)
        {
            X = positionX;
            Y = positionY;
        }
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
    }
}
