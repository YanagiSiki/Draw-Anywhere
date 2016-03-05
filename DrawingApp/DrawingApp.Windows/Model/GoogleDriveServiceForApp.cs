using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Download;
using Google.Apis.Drive.v2.Data;
using System.IO;
using Windows.Storage;
using System.Threading;
using Windows.Storage.Streams;

namespace GoogleDriveForApp.GoogleDriveForApp
{
    class GoogleDriveServiceForApp
    {
        private readonly string[] _scopes = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private DriveService _service;
        private const int KB = 0x400;
        private const int DOWNLOAD_CHUNK_SIZE = 256 * KB;
        private int _timeStamp;
        private string _applicationName;
        private string _clientSecretFileName;
        private UserCredential _credential;

        /// <summary>
        /// 創造一個Google Drive Service
        /// </summary>
        /// <param name="applicationName">應用程式名稱</param>
        /// <param name="clientSecretFileName">ClientSecret檔案名稱</param>
        public GoogleDriveServiceForApp(string applicationName, string clientSecretFileName)
        {
            _applicationName = applicationName;
            _clientSecretFileName = clientSecretFileName;
            this.CreateNewService(applicationName, clientSecretFileName);
        }

        /// <summary>
        /// 新創一Service
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="clientSecretFileName"></param>
        private async void CreateNewService(string applicationName, string clientSecretFileName)
        {
            const string USER = "user";
            const string ASSETS_URI = "ms-appx:///Assets/";
            string credentialURI = ASSETS_URI + clientSecretFileName;
            UserCredential credential = null;
            Uri credentialFileURI = new Uri(credentialURI);

            try
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(credentialFileURI,
                    new[] { Uri.EscapeUriString(DriveService.Scope.Drive) },
                    USER,
                    CancellationToken.None);
            }
            catch
            {
                throw;
            }

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            _credential = credential;
            DateTime now = DateTime.Now;
            _timeStamp = UNIXNowTimeStamp;
            _service = service;
        }

        private int UNIXNowTimeStamp
        {
            get
            {
                const int UNIX_START_YEAR = 1970;
                DateTime unixStartTime = new DateTime(UNIX_START_YEAR, 1, 1);
                return Convert.ToInt32((DateTime.Now.Subtract(unixStartTime).TotalSeconds));
            }
        }

        //Check and refresh the credential if credential is out-of-date
        private void CheckCredentialTimeStamp()
        {
            const int ONE_HOUR_SECOND = 3600;
            int nowTimeStamp = UNIXNowTimeStamp;

            if ((nowTimeStamp - _timeStamp) > ONE_HOUR_SECOND)
                this.CreateNewService(_applicationName, _clientSecretFileName);
        }

        /// <summary>
        /// 查詢Google Drive 根目錄的檔案
        /// </summary>
        /// <returns>Google Drive File List</returns>
        public List<Google.Apis.Drive.v2.Data.File> ListRootFileAndFolder()
        {
            List<Google.Apis.Drive.v2.Data.File> returnList = new List<Google.Apis.Drive.v2.Data.File>();
            const string ROOT_QUERY_STRING = "'root' in parents";

            try
            {
                returnList = ListFileAndFolderWithQueryString(ROOT_QUERY_STRING);
            }
            catch (Google.GoogleApiException exception)
            {
                throw exception;
            }

            return returnList;
        }

        /// <summary>
        /// 使用QueryString 查詢檔案 回傳一List
        /// </summary>
        /// <param name="queryString">QueryString，使用方法: https://developers.google.com/drive/web/search-parameters </param>
        /// <returns>含有Google Drive File 格式的 List</returns>
        private List<Google.Apis.Drive.v2.Data.File> ListFileAndFolderWithQueryString(string queryString)
        {
            List<Google.Apis.Drive.v2.Data.File> returnList = new List<Google.Apis.Drive.v2.Data.File>();
            this.CheckCredentialTimeStamp();
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.Q = queryString;
            do
            {
                try
                {
                    FileList fileList = listRequest.Execute();
                    returnList.AddRange(fileList.Items);
                    listRequest.PageToken = fileList.NextPageToken;
                }
                catch (Google.GoogleApiException exception)
                {
                    listRequest.PageToken = null;
                    throw exception;
                }
            } while (!String.IsNullOrEmpty(listRequest.PageToken));

            return returnList;
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="uploadFileName">上傳的檔案名稱 </param>
        /// <param name="contentType">上傳的檔案種類，請參考MIME Type : http://www.iana.org/assignments/media-types/media-types.xhtml </param>
        /// <param name="uploadProgressEventHandler"> 上傳進度改變時呼叫的函式</param>
        /// <param name="responseReceivedEventHandler">收到回應時呼叫的函式 </param>
        /// <returns>上傳成功，回傳上傳成功的 Google Drive 格式之File</returns>
        public async Task<Google.Apis.Drive.v2.Data.File> UploadFile(string uploadFileName, string contentType, Action<IUploadProgress> uploadProgressEventHandler = null, Action<Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler = null)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(uploadFileName);
            Stream uploadStream = await file.OpenStreamForReadAsync();
            string title = SubstractFileName(uploadFileName);

            this.CheckCredentialTimeStamp();
            Google.Apis.Drive.v2.Data.File fileToInsert = new Google.Apis.Drive.v2.Data.File
            {
                Title = title
            };

            FilesResource.InsertMediaUpload insertRequest = _service.Files.Insert(fileToInsert, uploadStream, contentType);
            insertRequest.ChunkSize = FilesResource.InsertMediaUpload.MinimumChunkSize * 2;
            AddUploadEventHandler(uploadProgressEventHandler, responseReceivedEventHandler, insertRequest);

            try
            {
                insertRequest.Upload();
            }
            catch (Google.GoogleApiException exception)
            {
                throw exception;
            }
            finally
            {
                uploadStream.Dispose();
            }

            return insertRequest.ResponseBody;
        }

        //Add Upload Event Handler
        private static void AddUploadEventHandler(Action<IUploadProgress> uploadProgressEventHandler, Action<Google.Apis.Drive.v2.Data.File> responseReceivedEventHandler, FilesResource.InsertMediaUpload insertRequest)
        {
            if (uploadProgressEventHandler != null)
                insertRequest.ProgressChanged += uploadProgressEventHandler;

            if (responseReceivedEventHandler != null)
                insertRequest.ResponseReceived += responseReceivedEventHandler;
        }

        //如檔案名稱帶有路徑，將路徑去除，回傳檔案名稱
        private static string SubstractFileName(string uploadFileName)
        {
            const char SPLASH = '\\';
            string title = "";

            if (uploadFileName.LastIndexOf(SPLASH) != -1)
                title = uploadFileName.Substring(uploadFileName.LastIndexOf(SPLASH) + 1);
            else
                title = uploadFileName;
            return title;
        }

        /// <summary>
        /// 下載檔案
        /// </summary>
        /// <param name="fileToDownload">欲下載的檔案(Google Drive File) 一般會從List取得</param>
        /// <param name="downloadPath">下載到路徑(於WindowsApp不可使用)</param>
        /// <param name="downloadProgressChangedEventHandler">當下載進度改變時，呼叫這個函式</param>
        public async void DownloadFile(Google.Apis.Drive.v2.Data.File fileToDownload, Action<IDownloadProgress> downloadProgressChangedEventHandler = null)
        {
            CheckCredentialTimeStamp();
            if (!String.IsNullOrEmpty(fileToDownload.DownloadUrl))
            {
                try
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileToDownload.Title, CreationCollisionOption.ReplaceExisting);
                    Task<byte[]> downloadByte = _service.HttpClient.GetByteArrayAsync(fileToDownload.DownloadUrl);
                    byte[] byteArray = downloadByte.Result;
                    await FileIO.WriteBytesAsync(file, byteArray);
                }
                catch (Google.GoogleApiException exception)
                {
                    throw exception;
                }
            }
        }

        /// <summary>
        /// 刪除符合FileID的檔案
        /// </summary>
        /// <param name="fileId">欲刪除檔案的FileID</param>
        public void DeleteFile(string fileId)
        {
            CheckCredentialTimeStamp();
            try
            {
                _service.Files.Delete(fileId).Execute();
            }
            catch (Google.GoogleApiException exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 更新指定FileID的檔案
        /// </summary>
        /// <param name="fileName">欲上傳至Google Drive並覆蓋在Google Drive上舊版檔案的檔案位置 </param>
        /// <param name="fileId">存在於Google Drive 舊版檔案的FileID </param>
        /// <param name="contentType">MIME Type</param>
        /// <returns>如更新成功，回傳更新後的Google Drive File</returns>
        public async Task<Google.Apis.Drive.v2.Data.File> UpdateFile(string fileName, string fileId, string contentType)
        {
            CheckCredentialTimeStamp();
            try
            {
                Google.Apis.Drive.v2.Data.File file = _service.Files.Get(fileId).Execute();
                StorageFile fileToUpdate = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                Stream stream = await fileToUpdate.OpenStreamForReadAsync();
                FilesResource.UpdateMediaUpload request = _service.Files.Update(file, fileId, stream, contentType);
                request.NewRevision = true;
                request.Upload();

                Google.Apis.Drive.v2.Data.File updatedFile = request.ResponseBody;
                return updatedFile;
            }
            catch (Google.GoogleApiException exception)
            {
                throw exception;
            }
        }
    }
}
