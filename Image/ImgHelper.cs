using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Timeline.Framework.Drawing.Image
{
    
    public static class ImgHelper
    {
        /// <summary>
        /// 从uri获取图片流，可以是本地或者网络，可能会抛出异常
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static IRandomAccessStream GetImgStream(Uri uri)
        {
            var stream = new InMemoryRandomAccessStream();
            try
            {
                var s = StorageFile.GetFileFromApplicationUriAsync(uri).AsTask();
                s.Wait();
                LoadFromFile(s.Result,stream);
            }
            catch (Exception )
                when (uri != null && !string.IsNullOrEmpty(uri.AbsolutePath))
            {
                var http = new HttpClient();
                var buff = http.GetBufferAsync(uri).AsTask();
                buff.Wait();
                stream.WriteAsync(buff.Result).AsTask().Wait();
                http.Dispose();
            }
            return stream;
        }
        /// <summary>
        /// 从路径获取数据流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IRandomAccessStream InsertImg(string path)
        {
            var stream = new InMemoryRandomAccessStream();
            var s = StorageFile.GetFileFromPathAsync(path).AsTask();
            s.Wait();
            LoadFromFile(s.Result,stream);
            return stream;
        }

        private static void LoadFromFile(StorageFile file,IRandomAccessStream s)
        {
            var stream = file.OpenAsync(FileAccessMode.Read).AsTask();
            stream.Wait();
            RandomAccessStream.CopyAsync(stream.Result, s).AsTask().Wait();
            stream.Result.Dispose();
        }

    }
}
