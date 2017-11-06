using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Encoder = System.Text.Encoder;

namespace Camps.CommonLib.ExtentionMethods
{

    public class VideoStream
    {
        private readonly Stream _file;

        public VideoStream(Stream file)
        {
            _file = file;
            WriteToStream = async  (outputStream, content, context) =>
            {
                try
                {
                    var buffer = new byte[65536];


                    var length = (int)_file.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = _file.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }

                }
                catch (HttpException ex)
                {
                    return;
                }
                finally
                {
                    outputStream.Close();
                }

            };
        }

        public Action<Stream, HttpContent, TransportContext> WriteToStream;

        public async void WriteToStream1(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];


                var length = (int)_file.Length;
                var bytesRead = 1;

                while (length > 0 && bytesRead > 0)
                {
                    bytesRead = _file.Read(buffer, 0, Math.Min(length, buffer.Length));
                    await outputStream.WriteAsync(buffer, 0, bytesRead);
                    length -= bytesRead;
                }

            }
            catch (HttpException ex)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }
    }

    public static class FormatType
    {
        public static bool IsImage(this string extention)
        {
            switch (extention.ToLower())
            {
                case "image/jpg":
                case "image/png":
                case "image/gif":
                case "image/jpeg":
                case "image/bmp":
                    return true;
            }
            return false;
        }
        public static bool IsVideo(this string extention)
        {
            switch (extention.ToLower())
            {
                case "video/x-ms-wmv":
                case "video/mp4":
                    return true;
            }
            return false;
        }
        public static bool IsAudio(this string extention)
        {
            switch (extention.ToLower())
            {
                case "audio/mp3":

                    return true;
                    break;
                    
            }
            return false;
        }
    }

    public static class ImageHandler
    {

        public static Image GetThumbnail(byte[] arrayBytes, string contentType)
        {
            var assembly = Assembly.GetExecutingAssembly();
            if (contentType.IsImage())
            {
                var ms2 = arrayBytes == null ? new MemoryStream(new byte[1]) : new MemoryStream(arrayBytes);
                return Image.FromStream(ms2);
            }
            if (contentType.IsAudio())
            {
                const string resourceName = "Camps.CommonLib.Contents.sound.png";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    return Image.FromStream(stream);
                }
            } 

            const string resourceNVideo = "Camps.CommonLib.Contents.video.png";

            using (Stream stream = assembly.GetManifestResourceStream(resourceNVideo))
            {
                return Image.FromStream(stream);
            }
        }
        public static Image GetImage(byte[] arrayBytes)
        {
            var ms2 = arrayBytes == null ? new MemoryStream(new byte[1]) : new MemoryStream(arrayBytes);

            return Image.FromStream(ms2);
        }

    
        public static Stream ToStream(this byte[] arrayBytes)
        {
            Stream  ms2 = arrayBytes == null ? new MemoryStream(new byte[1]) : new MemoryStream(arrayBytes);

            return ms2;
        }
        public static byte[] ImageToArray(this Image image, ImageFormat imageFormat = null)
        {
            var imgFormat = imageFormat ?? ImageFormat.Png;
            MemoryStream ms = new MemoryStream();
            image.Save(ms, imgFormat);
            return ms.ToArray();
        }

        public static Image ResizeImage(this Image imgToResize, Size size)
        {

            //Get the image current width

            int sourceWidth = imgToResize.Width;

            //Get the image current height

            int sourceHeight = imgToResize.Height;



            float nPercent = 0;

            float nPercentW = 0;

            float nPercentH = 0;

            //Calulate  width with new desired size

            nPercentW = ((float)size.Width / (float)sourceWidth);

            //Calculate height with new desired size

            nPercentH = ((float)size.Height / (float)sourceHeight);



            if (nPercentH < nPercentW)

                nPercent = nPercentH;

            else

                nPercent = nPercentW;

            //New Width

            int destWidth = (int)(sourceWidth * nPercent);

            //New Height

            int destHeight = (int)(sourceHeight * nPercent);



            Bitmap b = new Bitmap(destWidth, destHeight);

            Graphics g = Graphics.FromImage((System.Drawing.Image)b);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw image with new width and height

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);

            g.Dispose();

            return (System.Drawing.Image)b;

        }

    }
}
