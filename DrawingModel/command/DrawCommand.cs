using DrawingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawCommand : ICommand
    {
        ShapeType _shape;
        Model _model;

        public DrawCommand(Model model, ShapeType shape)
        {
            _shape = shape;
            _model = model;
        }

        public void Execute()//執行
        {
            _model.DrawShape(_shape);
        }

        public void ExecutePrevious()//復原
        {
            _model.DeleteShape(_shape);
        }
    }
    
}
