using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DrawingModel;
using GoogleDriveForApp.GoogleDriveForApp;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Popups;

namespace DrawingApp.PresentationModel
{
    class PresentationModel
    {
        Model _model;
        IGraphics _graphics;
        Canvas _canvas;
        //以下為檔案管理        
        
        const string APPLICATION_NAME = "DrawAnywhere";
        const string CLIENT_SECRET_FILE_NAME = "clientSecret.json";
        const string IMAGE_DATA_NAME = "ImageData.txt";
        const string IMAGE_NAME = "Image.jpg";
        const string SIGN = ";";
        const string CHANGE_LINE = "\r\n";
        const string NO_DATA = "No Data ! ";
        const string ERROR = "Error";
        const string OK = "OK";
        GoogleDriveServiceForApp _service = new GoogleDriveServiceForApp(APPLICATION_NAME, CLIENT_SECRET_FILE_NAME);

        public PresentationModel(Model model, Canvas canvas)
        {
            this._model = model;
            _graphics = new WindowsStoreGraphicsAdaptor(canvas);
            _canvas = canvas;
        }

        public void Draw()//將graphics轉型為WindowsStoreGraphicsAdaptor
        {
            // 重複使用igraphics物件            
            _model.Draw(_graphics);
        }
       
        public async void WriteImageData()//儲存ImageData
        {
            StreamWriter writer = new StreamWriter(await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(IMAGE_DATA_NAME, CreationCollisionOption.ReplaceExisting));            
            foreach (ShapeType shape in _model.GetShapeList)
            {
                writer.WriteLine(shape.GetShapeInformation());                
            }            
            await writer.FlushAsync();                        
            writer.Dispose();
            UploadImageData();
        }

        public async void ReadImageData()//載入ImageData
        {
            const char CHANGE_LINE_SIGN = '\n';            
            try
            {
                StorageFile sampleFile = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(IMAGE_DATA_NAME);
                string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
                List<string> fileData = text.Split(CHANGE_LINE_SIGN).ToList();
                _model.ReadImageData(fileData);
            }
            catch 
            {
                ShowErrorMessage();
            }      
        }

        public void DownloadImageData()//下載ImageData
        {
            List<Google.Apis.Drive.v2.Data.File> fileList = _service.ListRootFileAndFolder();
            Google.Apis.Drive.v2.Data.File fileToDownload = fileList.Find(item =>
            {
                return item.Title == IMAGE_DATA_NAME;
            });
            if (fileToDownload == null)
            {
                ShowErrorMessage();
            }
            else
            {
                _service.DownloadFile(fileToDownload);
            }                    
        }

        private async void ShowErrorMessage()//show 錯誤訊息
        {
            MessageDialog dialog = new MessageDialog(NO_DATA, ERROR);
            UICommand okButton = new UICommand(OK);
            dialog.Commands.Add(okButton);
            await dialog.ShowAsync();
        }

        public async void SaveImage()//儲存Image
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(_canvas);
            IBuffer pixelBuffer = await bitmap.GetPixelsAsync();
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(IMAGE_NAME, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)bitmap.PixelWidth,
                    (uint)bitmap.PixelHeight,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    pixelBuffer.ToArray()
                );
                await encoder.FlushAsync();
                stream.Dispose();
            }
            UploadImage();
        }

        public void UploadImageData()//上傳ImageData
        {
            const string CONTENT_TYPE = "text/plain";
            UploadFile(IMAGE_DATA_NAME, CONTENT_TYPE);            
        }

        public void UploadImage()//上傳Image
        {
            const string CONTENT_TYPE = "image/jpeg";
            UploadFile(IMAGE_NAME, CONTENT_TYPE);            
        }		
		
        public async void UploadFile(string updateFileName, string contentType)//上傳
        {
            List<Google.Apis.Drive.v2.Data.File> fileList = _service.ListRootFileAndFolder();
            Google.Apis.Drive.v2.Data.File foundFile = fileList.Find(item => 
            { 
                return item.Title == updateFileName; 
            });
            if (foundFile == null)
            {
                await _service.UploadFile(updateFileName, contentType);
            }
            else
            {
                await _service.UpdateFile(updateFileName, foundFile.Id, contentType);
            }
        }

    }
}