using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{   
    class ClearCommand : ICommand
    {
        List<ShapeType> _shapes;
        Model _model;
        public ClearCommand(Model model, List<ShapeType> shapes)
        {
            _shapes = new List<ShapeType>(shapes.ToArray());
            _model = model;
        }

        public void Execute()//執行
        {
            _model.DeleteShape(_shapes);            
        }

        public void ExecutePrevious()//復原
        {
            _model.DrawShape(_shapes);
        }
    }
}
