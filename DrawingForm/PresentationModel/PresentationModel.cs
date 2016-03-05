using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;
using System.Windows.Forms;
using System.Drawing;
using GoogleDriveUploader.GoogleDrive;
using System.IO;
using System.Text.RegularExpressions;

namespace DrawingForm.PresentationModel
{
    class PresentationModel
    {
        Model _model;
        Bitmap _bitmap;
        //以下為檔案管理
        string _projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\";
        private string _uploadFileName;
        private string _downloadFileName;
        GoogleDriveService _service;
        const string APPLICATION_NAME = "DrawAnywhere";
        const string CLIENT_SECRET_FILE_NAME = "clientSecret.json";
        const string IMAGE_DATA_NAME = "ImageData.txt";
        const string IMAGE_NAME = "Image.jpg";
        const string SIGN = ";";
        const string NO_CONNECTION = "No connection ! ";

        public PresentationModel(Model model, Control canvas)
        {
            this._model = model;
        }
        public void Draw(System.Drawing.Graphics graphics)//將graphics轉型為WindowsFormsGraphicsAdaptor
        {
            WindowsFormsGraphicsAdaptor windowsGraphic = new WindowsFormsGraphicsAdaptor(graphics);
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            _model.Draw(windowsGraphic);
            _bitmap = windowsGraphic.GetBitmap();

        }

        public void WriteImageData()//儲存ImageData
        {
            if (!File.Exists(_projectPath + IMAGE_DATA_NAME))
            {
                File.Create(_projectPath + IMAGE_DATA_NAME).Dispose();
            }
            using (StreamWriter sw = new StreamWriter(_projectPath + IMAGE_DATA_NAME))
            {
                int index = 0;
                for (index = 0; index < _model.GetShapeList.Count; index++)
                {
                    sw.WriteLine(_model.GetShapeList[index].GetShapeInformation());// 寫入文字    
                }
                sw.Close();// 關閉串流
            }            
        }

        public void DownloadImageData()//下載ImageData
        {
            _downloadFileName = IMAGE_DATA_NAME;
            _service = new GoogleDriveService(APPLICATION_NAME, CLIENT_SECRET_FILE_NAME);
            List<Google.Apis.Drive.v2.Data.File> fileList = _service.ListRootFileAndFolder();
            Google.Apis.Drive.v2.Data.File fileToDownload = fileList.Find(item =>
            {
                return item.Title == _downloadFileName;
            });
            Console.WriteLine(fileToDownload);
            if (fileToDownload == null)
                MessageBox.Show(NO_CONNECTION);
            else 
            {
                _service.DownloadFile(fileToDownload, _projectPath);
                ReadImageData();
            }                            
        }

        public void ReadImageData()//載入ImageData
        {
            try
            {
                List<string> fileData = new List<string>();
                using (StreamReader sr = new StreamReader(_projectPath + IMAGE_DATA_NAME))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        fileData.Add(line);
                    }
                    _model.ReadImageData(fileData);
                }
            }
            catch 
            {
            }            
        }

        public void UploadImage()//上傳Image
        {
            _uploadFileName = IMAGE_NAME;
            const string CONTENT_TYPE = "image/jpeg";
            UploadFile(_uploadFileName, CONTENT_TYPE);
        }

        public void UploadImageData()//上傳ImageData
        {
            _uploadFileName = IMAGE_DATA_NAME;
            const string CONTENT_TYPE = "image/jpeg";
            UploadFile(_uploadFileName, CONTENT_TYPE);
        }

        public void UploadFile(string updateFileName, string contentType)//上傳
        {
            _service = new GoogleDriveService(APPLICATION_NAME, CLIENT_SECRET_FILE_NAME);
            List<Google.Apis.Drive.v2.Data.File> fileList = _service.ListRootFileAndFolder();
            Google.Apis.Drive.v2.Data.File foundFile = fileList.Find(item =>
            {
                return item.Title == updateFileName;
            });
            if (foundFile == null)
            {
                _service.UploadFile(_projectPath + _uploadFileName, contentType);
            }
            else
            {
                _service.UpdateFile(_projectPath + updateFileName, foundFile.Id, contentType);
            }
        }

        public void SaveImage()//儲存Image
        {
            _bitmap.Save(_projectPath + IMAGE_NAME);
        }
    }
}
