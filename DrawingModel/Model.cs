using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace DrawingModel
{

    class Model
    {
        CommandManager _commandManager = new CommandManager();
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();        
   
        //
        List<ShapeType> _shapes = new List<ShapeType>();
        ShapeType _target = null;
        const string CIRCLE_TYPE = "Circle";
        const string LINE_TYPE = "Line";
        const string RECTANGLE_TYPE = "Rectangle";
        const string SMILE_TYPE = "Smile";
        CreateShape _createShape = new CreateShape();
        //
        Frame _frame = new Frame();
        //        
        const int ZERO = 0;
        const int ONE = 1;
        const int TWO = 2;
        const int THREE = 3;
        const int FOUR = 4;
        const int FIVE = 5;
        const int SIX = 6;
        const int SEVEN = 7;
        const int EIGHT = 8;
        const int NINE = 9;
        //
        Points _leftUp;
        Points _rightDown;                
        //
        int _active = 0;
        const int NOTHING = 0;
        const int SELECTING = 1;
        const int MOVING = 2;
        const int RESIZING = 3;
        //
        int _position = 0;
        const int LEFT_ON = 1;
        const int LEFT_DOWN = 2;
        const int RIGHT_ON = 3;
        const int RIGHT_DOWN = 4;
        //

        public RedGreenBlueAlpha _redGreenBlueAlpha
        {
            get;
            set;
        }        

        public List<ShapeType> GetShapeList
        {
            get
            {
                return _shapes;
            }
        }

        public void PressPointer(double positionX, double positionY, string shapeType)//按下Pointer執行
        {
            _leftUp = new Points((int)positionX, (int)positionY);
            _rightDown = new Points((int)positionX, (int)positionY);            
            PressInShape(positionX, positionY);
            if (_active == NOTHING)
            {                
                switch (shapeType)
                {
                    case SMILE_TYPE:
                        _commandManager.Execute(new DrawCommand(this, _createShape.CreateSmile(_rightDown.X, _rightDown.Y)));
                        break;
                }                
            }
            NotifyModelChanged();                   
        }

        public void MovePointer(double positionX, double positionY, string shapeType)//移動Pointer執行
        {
            _rightDown = new Points((int)positionX, (int)positionY);
            if (_active == SELECTING && _position == 0) //選圖片，無角落
            {
                _active = MOVING;                
            }
            if (_active == SELECTING && _position != 0) //選圖片，無角落
            {
                _active = RESIZING;
                
            }
            NotifyModelChanged();
        }

        public void ReleasePointer(double positionX, double positionY, string shapeType)//放開Pointer執行
        {
            _rightDown = new Points((int)positionX, (int)positionY);
            if (_active == NOTHING)
            {                
                if (_rightDown.X > _leftUp.X && _rightDown.Y > _leftUp.Y)
                {
                    switch (shapeType)
                    {
                        case CIRCLE_TYPE:
                            _commandManager.Execute(new DrawCommand(this, _createShape.CreateCircle(_leftUp.X, _leftUp.Y, _rightDown.X - _leftUp.X, _rightDown.Y - _leftUp.Y, _redGreenBlueAlpha)));
                            break;                        
                        case RECTANGLE_TYPE:
                            _commandManager.Execute(new DrawCommand(this, _createShape.CreateRectangle(_leftUp.X, _leftUp.Y, _rightDown.X - _leftUp.X, _rightDown.Y - _leftUp.Y, _redGreenBlueAlpha)));
                            break;
                    }
                }                
            }
            else if (_active == MOVING)
            {                
                _commandManager.Execute(new MoveCommand(this, _target, (int)positionX, (int)positionY));
            }
            else if (_active == RESIZING)
            {
                _commandManager.Execute(new ResizeCommand(this, _target, (int)positionX, (int)positionY, _position));
            }
            NotifyModelChanged();      
        }

        public void ResizeTarget(ShapeType shape, int position, int positionX, int positionY)//重新定義圖形大小
        {
            shape.Resize(position, positionX, positionY);
        }

        public void PressInShape(double positionX, double positionY)//點選圖形時
        {
            foreach (ShapeType shape in _shapes)
            {
                if (shape.IsInShape((int)positionX, (int)positionY))
                {
                    _target = shape;
                    _position = _target.FindPosition((int)positionX, (int)positionY);
                    _active = SELECTING;
                    break;
                }
                else 
                {
                    _target = null;
                    _active = NOTHING;
                }                   
            }            
        }        

        public void DrawShape(ShapeType shape)//執行畫圖
        {
            _shapes.Add(shape);           
        }

        public void DrawShape(List<ShapeType> shapes)//執行畫圖(全部)
        {
            foreach (ShapeType shape in shapes)
            {
                _shapes.Add(shape);
            }            
        }

        public void DeleteShape(ShapeType shape)//刪除圖形
        {
            _shapes.Remove(shape);            
        }

        public void DeleteShape(List<ShapeType> shapes)//刪除圖形(全部)
        {
            _shapes.Clear();
        }

        public void MoveShape(ShapeType shape, int positionX, int positionY)//移動圖形
        {
            shape.PositionX += positionX;
            shape.PositionY += positionY;
        }

        public void ChangeTargetColor()//移動圖形
        {
            if (_target != null) 
            _commandManager.Execute(new ChangeColorCommand(this, _target, _redGreenBlueAlpha));            
        }       

        public void Undo()//undo
        {
            _commandManager.Undo();
            _target = null;
            NotifyModelChanged();      
        }

        public void Redo()//redo
        {
            _commandManager.Redo();
            _target = null;
            NotifyModelChanged();      
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _commandManager.IsRedoEnabled;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _commandManager.IsUndoEnabled;
            }
        }

        public bool IsDeleteEnabled
        {
            get
            {
                if (_target != null)
                return true;
                else
                return false;
            }
        }

        public void Clear()//清空頁面
        {            
            _commandManager.Execute(new ClearCommand(this, _shapes));
            _target = null;    
            NotifyModelChanged();
        }

        public void DeleteTarget()//清空頁面
        {            
            _commandManager.Execute(new DeleteCommand(this, _target));
            _target = null;
            NotifyModelChanged();
        }

        public void Draw(IGraphics graphics)//把所有圖形畫上去
        {
            graphics.ClearAll();

            foreach (ShapeType shape in _shapes)
            {
                shape.Draw(graphics);
            }
            if (_target != null) 
            {
                _frame.PositionX = _target.PositionX;
                _frame.PositionY = _target.PositionY;
                _frame.Width = _target.Width;
                _frame.Height = _target.Height;
                _frame.Draw(graphics);
            }
                
        }

        void NotifyModelChanged()//model更新
        {            
            if (_modelChanged != null)
                _modelChanged();            
        }

        public void ReadImageData(List<string> fileData)//儲存ImageData
        {
            const string SIGN = ";";
            int index;
            for (index = 0; index < fileData.Count; index++)
            {
                string[] loadStringTemporary = Regex.Split(fileData[index], SIGN, RegexOptions.IgnoreCase);
                switch (loadStringTemporary[ZERO])
                {
                    case CIRCLE_TYPE:
                        _shapes.Add(_createShape.CreateCircle(double.Parse(loadStringTemporary[ONE]), double.Parse(loadStringTemporary[TWO]), double.Parse(loadStringTemporary[THREE]), double.Parse(loadStringTemporary[FOUR]), new RedGreenBlueAlpha(int.Parse(loadStringTemporary[FIVE]),int.Parse(loadStringTemporary[SIX]),int.Parse(loadStringTemporary[SEVEN]),int.Parse(loadStringTemporary[EIGHT]))));
                        break;                    
                    case RECTANGLE_TYPE:
                        _shapes.Add(_createShape.CreateRectangle(double.Parse(loadStringTemporary[ONE]), double.Parse(loadStringTemporary[TWO]), double.Parse(loadStringTemporary[THREE]), double.Parse(loadStringTemporary[FOUR]), new RedGreenBlueAlpha(int.Parse(loadStringTemporary[FIVE]), int.Parse(loadStringTemporary[SIX]), int.Parse(loadStringTemporary[SEVEN]), int.Parse(loadStringTemporary[EIGHT]))));
                        break;
                    case SMILE_TYPE:
                        _shapes.Add(_createShape.CreateSmile(double.Parse(loadStringTemporary[ONE]), double.Parse(loadStringTemporary[TWO])));
                        break;
                }
            }
        }
    }
}