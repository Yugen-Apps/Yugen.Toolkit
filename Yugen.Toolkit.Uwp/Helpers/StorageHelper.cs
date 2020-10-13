//using System;
//using System.Threading.Tasks;

//namespace Yugen.Toolkit.Uwp.Services
//{
//    public class StorageHelper<T>
//    {
//        private readonly ApplicationData _appData = ApplicationData.Current;
//        private readonly StorageType _storageType;
//        private readonly ILogger<StorageHelper<T>> _logger;

//        public StorageHelper(StorageType storageType, ILogger<StorageHelper<T>> logger)
//        {
//            _storageType = storageType;
//            _logger = logger;
//        }

//        public async Task DeleteAsync()
//        {
//            var fileName = FileName(Activator.CreateInstance<T>(), string.Empty);
//            await DeleteAsync(fileName);
//        }

//        public async Task<bool> DeleteAsync(string handle)
//        {
//            var fileName = FileName(Activator.CreateInstance<T>(), handle);
//            try
//            {
//                var folder = GetFolder(_storageType);

//                var file = await GetFileIfExistsAsync(folder, fileName);
//                if (file != null)
//                {
//                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
//                }

//                return true;
//            }
//            catch (Exception)
//            {
//                // Ignored
//            }

//            return false;
//        }

//        public async Task<bool> SaveAsync(T obj, string handle)
//        {
//            try
//            {
//                StorageFile file = null;
//                var fileName = FileName(Activator.CreateInstance<T>(), handle);
//                var folder = GetFolder(_storageType);
//                file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

//                var storageString = JsonConvert.SerializeObject(obj);
//                await FileIO.WriteTextAsync(file, storageString);

//                return true;
//            }
//            catch (Exception e)
//            {
//                _logger.LogExtError(e);
//            }

//            return false;
//        }

//        public async Task<bool> SaveAsync(T obj)
//        {
//            var fileName = FileName(obj, string.Empty);
//            var result = await SaveAsync(obj, fileName);
//            return result;
//        }

//        public async Task<T> LoadAsync(string handle)
//        {
//            var fileName = FileName(Activator.CreateInstance<T>(), handle);
//            try
//            {
//                StorageFile file = null;
//                var folder = GetFolder(_storageType);
//                file = await folder.GetFileAsync(fileName);

//                var data = await FileIO.ReadTextAsync(file);
//                return JsonConvert.DeserializeObject<T>(data);
//            }
//            catch (Exception e)
//            {
//                _logger.LogExtError(e);
//                return default;
//            }
//        }

//        public async Task<T> LoadAsync()
//        {
//            var fileName = FileName(Activator.CreateInstance<T>(), string.Empty);
//            return await LoadAsync(fileName);
//        }

//        private StorageFolder GetFolder(StorageType storageType)
//        {
//            StorageFolder folder;
//            switch (storageType)
//            {
//                case StorageType.Roaming:
//                    folder = _appData.RoamingFolder;
//                    break;

//                case StorageType.Local:
//                    folder = _appData.LocalFolder;
//                    break;

//                case StorageType.Temporary:
//                    folder = _appData.TemporaryFolder;
//                    break;

//                default:
//                    throw new Exception(string.Format("Unknown StorageType: {0}", storageType));
//            }
//            return folder;
//        }

//        private async Task<StorageFile> GetFileIfExistsAsync(StorageFolder folder, string fileName)
//        {
//            try
//            {
//                return await folder.GetFileAsync(fileName);
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        private string FileName(T obj, string handle)
//        {
//            return string.Concat(handle, string.Format("{0}", obj.GetType().ToString()));
//        }
//    }
//}
