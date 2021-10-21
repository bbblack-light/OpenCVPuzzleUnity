using System;
using System.Collections.Generic;

namespace Models
{
    public class FullParts
    {
        public static Action FileNameReceived = delegate {  };

        public static string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                FileNameReceived.Invoke();
            }
        } 
        private static string _filename = string.Empty;
        public static int cols;
        public static int rows;
        
        public List<OneByteImage> images;
        public int height;
        public int width;

        public FullParts()
        {
            images = new List<OneByteImage>();
        }
    }

    [Serializable]
    public class OneByteImage
    {
        public byte[] array;
        public int number;
    }
}