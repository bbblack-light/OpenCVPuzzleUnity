using Emgu.CV;
using Emgu.CV.Structure;

namespace OpenCvPuzzleHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "C:\\Users\\Елена\\Desktop\\fqefqf.png";
            Image<Bgr, byte> image = new Image<Bgr, byte>(fileName);
            CvInvoke.Imshow("image", image);
            CvInvoke.WaitKey(0);
        }
    }
}