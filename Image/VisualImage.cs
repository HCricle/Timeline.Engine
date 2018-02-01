using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace Timeline.Framework.Image
{
    public static class VisualImage
    {
        /// <summary>
        /// 获取uri图片的数据，返回一个元组，色素集合，pixel宽，高
        /// </summary>
        /// <param name="uri">图片源</param>
        /// <returns></returns>
        public async static Task<Tuple<Color[,], int, int>> GetImgAllPixels(Uri uri)
        {
            var bitmap = (await GetImgPixels(uri));
            var colors = bitmap.GetPixelColors().ToArray();
            var bounds = bitmap.Bounds;
            var fwcount = colors.Count() / bitmap.ConvertDipsToPixels((float)bounds.Width, CanvasDpiRounding.Ceiling);
            var fhcount = colors.Count() / bitmap.ConvertDipsToPixels((float)bounds.Height, CanvasDpiRounding.Ceiling);
            var res = new Color[fhcount,fwcount];
            for (int i = 0; i < fhcount; i++)
            {
                for (int j = 0; j < fwcount; j++)
                {
                    res[i, j] = colors[fwcount + fhcount * i];
                }
            }
            return new Tuple<Color[,], int, int>(res, fwcount, fhcount);
        }
        public async static Task<Tuple<Color[], int, int>> GetImgDataPixels(Uri uri)
        {
            var bitmap = (await GetImgPixels(uri));
            var bounds = bitmap.Bounds;
            var fw = bitmap.ConvertDipsToPixels((float)bounds.Width, CanvasDpiRounding.Ceiling);
            var fh = bitmap.ConvertDipsToPixels((float)bounds.Height, CanvasDpiRounding.Ceiling);

            return new Tuple<Color[], int, int>(bitmap.GetPixelColors(), fw, fh);
        }
        public async static Task<CanvasBitmap> GetImgPixels(Uri uri)
        {
            var device = CanvasDevice.GetSharedDevice();
            return await CanvasBitmap.LoadAsync(device, uri);
        }
    }
}
