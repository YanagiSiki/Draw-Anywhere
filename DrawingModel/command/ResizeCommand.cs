using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class ResizeCommand : ICommand
    {
        const int LEFT_ON = 1;
        const int LEFT_DOWN = 2;
        const int RIGHT_ON = 3;
        const int RIGHT_DOWN = 4;
        ShapeType _shape;
        Model _model;
        int _positionX;
        int _positionY;
        int _temporaryX;
        int _temporaryY;
        int _position;

        public ResizeCommand(Model model, ShapeType shape, int positionX, int positionY, int position)
        {
            _shape = shape;
            _model = model;
            _positionX = positionX;
            _positionY = positionY;
            _position = position;
            Record(position);
        }

        public void Record(int position)//執行
        {
            switch (position)
            {
                case LEFT_ON:
                    _temporaryX = _shape.PositionX;
                    _temporaryY = _shape.PositionY;
                    break;
                case RIGHT_ON:
                    _temporaryX = _shape.PositionX + _shape.Width;
                    _temporaryY = _shape.PositionY;
                    break;
                case LEFT_DOWN:
                    _temporaryX = _shape.PositionX;
                    _temporaryY = _shape.PositionY + _shape.Height;
                    break;
                case RIGHT_DOWN:
                    _temporaryX = _shape.PositionX + _shape.Width;
                    _temporaryY = _shape.PositionY + _shape.Height;
                    break;
            }
        
        }

        public void Execute()//執行
        {
            _model.ResizeTarget(_shape, _position, _positionX, _positionY);
        }

        public void ExecutePrevious()//復原
        {
            _model.ResizeTarget(_shape, _position, _temporaryX, _temporaryY);
        }
    }
}
