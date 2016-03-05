using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{    
    class MoveCommand : ICommand
    {
        ShapeType _shape;
        Model _model;
        int _moveX;
        int _moveY;
        public MoveCommand(Model model, ShapeType shape, int positionX ,int positionY)
        {
            _shape = shape;
            _model = model;
            _moveX = positionX - shape.PositionX;
            _moveY = positionY - shape.PositionY;
        }

        public void Execute()//執行
        {
            _model.MoveShape(_shape, _moveX, _moveY);
        }

        public void ExecutePrevious()//復原
        {
            _model.MoveShape(_shape, -_moveX, -_moveY);
        }
    }
}
