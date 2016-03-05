using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class CommandManager
    {
        const string ERROR = "Cannot Undo exception\n";
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();

        public void Execute(ICommand command)//執行
        {
            _undo.Push(command);// push command 進 undo stack
            command.Execute();            
            _redo.Clear();// 清除redo stack
        }

        public void Undo()//undo
        {
            if (_undo.Count <= 0)
            {
                throw new Exception(ERROR);
            }
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.ExecutePrevious();
        }

        public void Redo()//redo
        {
            if (_redo.Count <= 0) 
            {
                throw new Exception(ERROR);
            }
            ICommand command = _redo.Pop();
            _undo.Push(command);
            command.Execute();
        }

        public bool IsRedoEnabled//redo是否能按
        {
            get
            {
                return _redo.Count != 0;
            }
        }

        public bool IsUndoEnabled//undo是否能按
        {
            get
            {
                return _undo.Count != 0;
            }
        }
    }
}
