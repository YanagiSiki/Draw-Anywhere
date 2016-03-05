using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{    
    class ChangeColorCommand : ICommand
    {
        Model _model;
        ShapeType _shape;
        RedGreenBlueAlpha _previousRedGreenBlueAlpha;
        RedGreenBlueAlpha _redGreenBlueAlpha;

        public ChangeColorCommand(Model model,ShapeType shape, RedGreenBlueAlpha redGreenBlueAlpha)
        {
            _model = model;
            _shape = shape;
            _previousRedGreenBlueAlpha = shape.redGreenBlueAlpha;
            _redGreenBlueAlpha = redGreenBlueAlpha;
        }

        public void Execute()//執行
        {
            _shape.redGreenBlueAlpha = _redGreenBlueAlpha;         
        }

        public void ExecutePrevious()//復原
        {
            _shape.redGreenBlueAlpha = _previousRedGreenBlueAlpha;            
        }    
    }
}
