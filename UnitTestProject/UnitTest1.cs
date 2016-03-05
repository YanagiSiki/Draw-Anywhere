using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        Model _target = new Model();
        const string CIRCLE_TYPE = "Circle";
        const string LINE_TYPE = "Line";
        const string RECTANGLE_TYPE = "Rectangle";
        const string SMILE_TYPE = "Smile";


        [TestMethod]
        public void InitializeModel()
        {
            Model model = new Model();
            _target = model;
        }

        [TestMethod]
        public void Pointer()
        {
            _target.PressPointer(-10, -10, CIRCLE_TYPE);
            _target.PressPointer(20, 20, CIRCLE_TYPE);
            _target.ReleasePointer(30, 50, CIRCLE_TYPE);            
            Assert.AreEqual(1, _target.GetShapeList.Count);
            _target.Undo();
            _target.Redo();
            //
            _target.PressPointer(120, 70, RECTANGLE_TYPE);
            _target.ReleasePointer(240, 100, RECTANGLE_TYPE);            
            Assert.AreEqual(2, _target.GetShapeList.Count);
            //
            _target.PressPointer(360, 180, SMILE_TYPE);
            Assert.AreEqual(3, _target.GetShapeList.Count);
            //
            _target.Clear();
            _target.Undo();
            _target.Redo();
            Assert.AreEqual(0, _target.GetShapeList.Count);
            _target.DeleteTarget();
            _target.Undo();
            _target.Redo();


        }

         [TestMethod]
        public void ReadData()
        {
            List<string> fileData = new List<string>();            
            //add Circle
            fileData.Add("Rectangle;394;420;65;183;0;0;0;255");
            fileData.Add("Circle;506;173;352;66;0;0;0;255");
            fileData.Add("Rectangle;142;131;97;252;0;0;0;255");
            fileData.Add("Circle;590;362;294;126;255;0;0;255");
            fileData.Add("Circle;362;277;120;80;0;0;0;255");
            //read data to list
            _target.ReadImageData(fileData);
            Assert.AreEqual(5, _target.GetShapeList.Count);

        }

         [TestMethod]
         public void MoveCommand()
         {
             //Enable
             Assert.AreEqual(false, _target.IsRedoEnabled);
             Assert.AreEqual(false, _target.IsUndoEnabled);
             //Data
             List<string> fileData = new List<string>();
             //add Data
             fileData.Add("Circle;30;20;80;80;0;0;0;255");
             fileData.Add("Smile;100;100;80;80;255;0;0;255");
             fileData.Add("Rectangle;200;200;50;50;0;0;0;255");
             _target.ReadImageData(fileData);
             //
             _target.PressPointer(140, 140, SMILE_TYPE);
             _target.PressPointer(140, 140, SMILE_TYPE);
             _target.MovePointer(1000, 1000, SMILE_TYPE);
             _target.ReleasePointer(1000, 1000, SMILE_TYPE);
             _target.Undo();
             _target.Redo();
             //
             _target.PressPointer(50, 50, CIRCLE_TYPE);
             _target.PressPointer(50, 50, CIRCLE_TYPE);
             _target.MovePointer(1200, 1200, CIRCLE_TYPE);
             _target.ReleasePointer(1200, 1200, CIRCLE_TYPE);
             _target.Undo();
             _target.Redo();
             //
             _target.PressPointer(50, 50, CIRCLE_TYPE);
             _target.PressPointer(225, 225, RECTANGLE_TYPE);
             _target.MovePointer(800, 800, RECTANGLE_TYPE);
             _target.ReleasePointer(800, 800, RECTANGLE_TYPE);
             _target.Undo();
             _target.Redo();
             //
             for (int i = 0; i < _target.GetShapeList.Count; i++)
             {
                 _target.GetShapeList[i].GetShapeInformation();
             }
             //
         }

         [TestMethod]
         public void ChangeSizeCircleCommand()
         {
             List<string> fileData = new List<string>();
             fileData.Add("Circle;30;20;80;80;0;0;0;255");
             _target.ReadImageData(fileData);


             _target.PressPointer(35, 25, CIRCLE_TYPE);
             _target.Draw(new Adaptor());
             _target.MovePointer(25, 15, CIRCLE_TYPE);
             _target.ReleasePointer(25, 15, CIRCLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();
             
             _target.PressPointer(105, 25, CIRCLE_TYPE);
             _target.MovePointer(150, 15, CIRCLE_TYPE);
             _target.ReleasePointer(150, 15, CIRCLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(35, 95, CIRCLE_TYPE);
             _target.MovePointer(25, 150, CIRCLE_TYPE);
             _target.ReleasePointer(25, 150, CIRCLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(105, 95, CIRCLE_TYPE);
             _target.MovePointer(150, 150, CIRCLE_TYPE);
             _target.ReleasePointer(150, 150, CIRCLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

         }

         [TestMethod]
         public void ChangeSizeRectangleCommand()
         {
             List<string> fileData = new List<string>();
             fileData.Add("Rectangle;30;20;80;80;0;0;0;255");
             _target.ReadImageData(fileData);


             _target.PressPointer(35, 25, RECTANGLE_TYPE);
             _target.Draw(new Adaptor());
             _target.MovePointer(25, 15, RECTANGLE_TYPE);
             _target.ReleasePointer(25, 15, RECTANGLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(105, 25, RECTANGLE_TYPE);
             _target.MovePointer(150, 15, RECTANGLE_TYPE);
             _target.ReleasePointer(150, 15, RECTANGLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(35, 95, RECTANGLE_TYPE);
             _target.MovePointer(25, 150, RECTANGLE_TYPE);
             _target.ReleasePointer(25, 150, RECTANGLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(105, 95, RECTANGLE_TYPE);
             _target.MovePointer(150, 150, RECTANGLE_TYPE);
             _target.ReleasePointer(150, 150, RECTANGLE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

         }

         
         [TestMethod]
         public void ChangeSizeSmileCommand()
         {
             List<string> fileData = new List<string>();
             fileData.Add("Smile;30;20");
             _target.ReadImageData(fileData);


             _target.PressPointer(35, 25, SMILE_TYPE);
             _target.Draw(new Adaptor());
             _target.MovePointer(25, 15, SMILE_TYPE);
             _target.ReleasePointer(25, 15, SMILE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(225, 25, SMILE_TYPE);
             _target.MovePointer(500, 15, SMILE_TYPE);
             _target.ReleasePointer(500, 15, SMILE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(35, 215, SMILE_TYPE);
             _target.MovePointer(25, 500, SMILE_TYPE);
             _target.ReleasePointer(25, 500, SMILE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

             _target.PressPointer(225, 215, SMILE_TYPE);
             _target.MovePointer(500, 500, SMILE_TYPE);
             _target.ReleasePointer(500, 500, SMILE_TYPE);
             _target.Undo();
             _target.Redo();
             _target.Undo();

         }


         [TestMethod]
         public void ChangeColorCommand()
         {
             List<string> fileData = new List<string>();
             fileData.Add("Rectangle;30;20;80;80;0;0;0;255");
             _target.ReadImageData(fileData);


             _target.PressPointer(35, 25, RECTANGLE_TYPE);
             _target.ChangeTargetColor();
             _target.Undo();
             _target.Redo();
             _target.Undo();
             

         }
    }
}
