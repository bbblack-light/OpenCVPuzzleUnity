﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace DefaultNamespace
{
    public class ImageProcessing
    {
        public static readonly int FixHeight = 1280 / 4 * 3;
        public static readonly int FixWidth = 1920 / 4 * 3;
        public static FullParts CutImage(ImageParameters parameters)
        {
            if (File.Exists(parameters.path))
            {
                var mats = new List<Mat>(); // List of rois
                var images = new List<byte[]>(); // List of extracted image parts

                var image = new Image<Bgr, byte>(parameters.path);
                
                Console.Write("Resized from " + image.Size.Height + "x" + image.Size.Width);
                while (image.Size.Height > FixHeight && image.Size.Width > FixWidth)
                {
                    image = image.Resize(0.95, Inter.Area);
                }
                Console.WriteLine(" to " + image.Size.Height + "x" + image.Size.Width);
                
                var height = image.Size.Height / parameters.rows;
                var weight = image.Size.Width / parameters.cols;
                
                for (int i = 0; i < parameters.cols; i++)
                {
                    for (int j = 0; j < parameters.rows; j++)
                    {
                        Mat roi = new Mat(image.Mat, new Rectangle(weight * i, height * j, weight, height));
                        images.Add(roi.ToImage<Bgr, byte>().Bytes);
                        // CvInvoke.Imshow("image", roi);
                        // CvInvoke.WaitKey(0);
                    }
                }

                return new FullParts()
                {
                    filename = Path.GetFileNameWithoutExtension(parameters.path),
                    images = images,
                    height = height,
                    width = weight
                };
            }

            return new FullParts();
        }
    }
}