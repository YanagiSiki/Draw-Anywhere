using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{   
    class DeleteCommand : ICommand
    {
        ShapeType _shape;
        Model _model;
        public DeleteCommand(Model model, ShapeType shape)
        {
            _shape = shape;
            _model = model;
        }

        public void Execute()//執行
        {
            _model.DeleteShape(_shape);            
        }

        public void ExecutePrevious()//復原
        {
            _model.DrawShape(_shape);
        }
    }
}
