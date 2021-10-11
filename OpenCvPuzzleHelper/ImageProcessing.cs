using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace DefaultNamespace
{
    public class ImageProcessing
    {
        public static void CutImage(ImageParameters parameters)
        {
            if (File.Exists(parameters.Path))
            {
                var mats = new List<Mat>(); // List of rois
                var images = new List<Image<Bgr,byte>>(); // List of extracted image parts

                var image = new Image<Bgr, byte>(parameters.Path);
                
                var height = image.Size.Height / parameters.Rows;
                var weight = image.Size.Width / parameters.Cols;
                
                for (int i = 0; i < parameters.Cols; i++)
                {
                    for (int j = 0; j < parameters.Rows; j++)
                    {
                        Mat roi = new Mat(image.Mat, new Rectangle(weight * i, height * j, weight, height));
                        images.Add(roi.ToImage<Bgr, byte>());
                        CvInvoke.Imshow("image", roi);
                        CvInvoke.WaitKey(0);
                    }
                }
            }
        }
    }
}