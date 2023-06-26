using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace MaricastanaClothingStore.UTILIDADES
{
    public class ConversorImagen
    {
        //IMAGEN DE LA BASE DE DATOS AL CAMPO
        public static byte[] bitmapImageToBytes(BitmapImage image)
        {
            if (image == null) { return null; }
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

        //IMAGEN DEL CAMPO A LA BASE DE DATOS
        public static BitmapImage ToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
