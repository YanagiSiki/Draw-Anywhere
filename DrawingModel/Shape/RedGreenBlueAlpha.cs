﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class RedGreenBlueAlpha
    {
        public RedGreenBlueAlpha(int red,int green, int blue, int alpha) 
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public int Red
        {
            get;
            set;
        }

        public int Green
        {
            get;
            set;
        }

        public int Blue
        {
            get;
            set;
        }

        public int Alpha
        {
            get;
            set;
        }
    }
}
