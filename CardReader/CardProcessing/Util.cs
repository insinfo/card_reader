using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace CardProcessing
{
    class Util
    {
        // converte um Bitmap do windows forms para um WPF BitmaImage
        public static System.Windows.Media.ImageSource BitmapToImageSource(System.Drawing.Bitmap bmp)
        {
            //System.Windows.Controls.Image image;
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            return bi;
        }
        //exibe aforge UnmanagedImage em um WPF Image
        public static void ShowAforgeImg(System.Windows.Controls.Image imageView, AForge.Imaging.UnmanagedImage inImg)
        {
            System.Drawing.Bitmap outImage = inImg.ToManagedImage();
            imageView.Source = BitmapToImageSource(outImage);
        }

        public static void ReplaceColor(Bitmap bmp, Color _colorOld, Color _colorNew, int _tolerance)
        {
            var bmap = new LockBitmap(bmp);
            bmap.LockBits();

            Color c;
            int iR_Min, iR_Max;
            int iG_Min, iG_Max;
            int iB_Min, iB_Max;

            //Defining Tolerance
            //R
            iR_Min = Math.Max((int)_colorOld.R - _tolerance, 0);
            iR_Max = Math.Min((int)_colorOld.R + _tolerance, 255);

            //G
            iG_Min = Math.Max((int)_colorOld.G - _tolerance, 0);
            iG_Max = Math.Min((int)_colorOld.G + _tolerance, 255);

            //B
            iB_Min = Math.Max((int)_colorOld.B - _tolerance, 0);
            iB_Max = Math.Min((int)_colorOld.B + _tolerance, 255);


            for (int x = 0; x < bmap.Width; x++)
            {
                for (int y = 0; y < bmap.Height; y++)
                {
                    c = bmap.GetPixel(x, y);


                    //Determinig Color Match
                    if (
                        (c.R >= iR_Min && c.R <= iR_Max) &&
                        (c.G >= iG_Min && c.G <= iG_Max) &&
                        (c.B >= iB_Min && c.B <= iB_Max)
                    )
                        if (_colorNew == Color.Transparent)
                            bmap.SetPixel(x, y, Color.FromArgb(0,
                              _colorNew.R,
                              _colorNew.G,
                              _colorNew.B));
                        else
                            bmap.SetPixel(x, y, Color.FromArgb(c.A,
                              _colorNew.R,
                              _colorNew.G,
                              _colorNew.B));
                }
            }

            bmap.UnlockBits();
        }

        public static T DeepCopy<T>(T other)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }

  
}
