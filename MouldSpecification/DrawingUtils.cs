using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Utils
{
    public class DrawingUtils
    {
        public enum ButtonOp
        {
            Disable = 0,   //no symbol showing (click ignored)
            Expand = 1,    //showing up arrow symbol
            Collapse = 2,  //showing down arrow symbol
            Browse = 3,    //showing file browser symbol
            Refresh = 4,   //showing refresh form symbol
            Document = 5,  //document symbol
            Deployment = 6,//deployment symbol
            FilterOn = 7,  //set filter
            FilterOff = 8, //reset filter
            SortAZ_On = 9,    //sort ascending -toggled on
            SortZA_On = 10,    //sort descending - toggled on
            SortAZ_Off = 11,    //sort ascending -toggled on
            SortZA_Off = 12,    //sort descending - toggled on
            plus = 13,
            minus = 14
        }


        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90

            // Other constants may be founded on pinvoke.net
        }

        public static Size ScreenRes()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();

            int Xdpi = GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSX);
            int Ydpi = GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSY);

            g.ReleaseHdc();
            return new Size(Xdpi, Ydpi);
        }

        public static int p96W(int inputW)
        {
            Size ss = ScreenRes();
            return Convert.ToInt32(inputW * ss.Width / 96);
        }

        public static int p96H(int inputH)
        {
            Size ss = ScreenRes();
            return Convert.ToInt32(inputH * ss.Height / 96);
        }

        public static Bitmap GetImage(ButtonOp op, int picboxW, int picboxH)
        {
            try
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                Stream myStream = null;
                //string[] names = myAssembly.GetManifestResourceNames();
                //foreach (string name in names)
                //{
                //    //Console.WriteLine(name);
                //    MessageBox.Show(name);
                //}
                if (op == ButtonOp.Expand)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.DownArrow.png");
                }
                else if (op == ButtonOp.Collapse)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.UpArrow.png");
                }
                else if (op == ButtonOp.Browse)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.browser.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.Refresh)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.refresh.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.Document)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.document.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.Deployment)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.Deployment.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.FilterOn)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.filter_green.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.FilterOff)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.filter_orange.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.SortAZ_On)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.sortAZ_green.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.SortZA_On)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.sortZA_green.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.SortAZ_Off)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.sortAZ_orange.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }
                else if (op == ButtonOp.SortZA_Off)
                {
                    myStream = myAssembly.GetManifestResourceStream("MouldSpecification.sortZA_Orange.png");
                    //myStream = myAssembly.GetManifestResourceStream("InjectionMouldBIN.image.folders50.png");
                }

                Bitmap image = new Bitmap(myStream);
                return RescaleImage(image, picboxW, picboxH);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public static Bitmap EmptyImage()
        {
            byte[] emptyImage = new byte[]
            {
                0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x01, 0x00, 0x01, 0x00, 0x80, 0x01, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
                0x00, 0x00, 0x2C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x02, 0x02, 0x44, 0x01
            };

            Stream myStream = new MemoryStream(emptyImage);
            Bitmap image = new Bitmap(myStream);
            return image;
        }

        public static Bitmap GetImage(string filePath, int picboxW, int picboxH)
        {
            try
            {
                Stream myStream = null;

                byte[] contents = File.ReadAllBytes(@filePath);
                //MemoryStream ms = new MemoryStream(contents);
                myStream = new MemoryStream(contents);
                //Image img = Image.FromStream(ms);
                Bitmap image = new Bitmap(myStream);
                return RescaleImage(image, picboxW, picboxH);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        public static Bitmap RescaleImage(Bitmap image)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            //float ratioX = (float)16 / (float)originalWidth;
            //float ratioY = (float)16 / (float)originalHeight;
            float ratioX = (float)16 / (float)originalWidth;
            float ratioY = (float)16 / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            float sourceRatio = (float)originalWidth / originalHeight;

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);
            Bitmap newImage = new Bitmap(newWidth, newHeight); //, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static Bitmap RescaleImage(Bitmap image, int PicBoxW, int PicBoxH)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            //float ratioX = (float)16 / (float)originalWidth;
            //float ratioY = (float)16 / (float)originalHeight;
            float ratioX = (float)PicBoxW / (float)originalWidth;
            float ratioY = (float)PicBoxH / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            float sourceRatio = (float)originalWidth / originalHeight;

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);
            Bitmap newImage = new Bitmap(newWidth, newHeight); //, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }
}
