using System;
using System.Collections.Generic;

namespace Models
{
    public class FullParts
    {
        public static Action FileNameReceived = delegate {  };

        public static string filename
        {
            get => _filename;
            set
            {
                _filename = value;
                FileNameReceived.Invoke();
            }
        } 
        private static string _filename;

        public static FullParts fullParts = new FullParts();
        
        public List<byte[]> images;
        public int height;
        public int width;

        public FullParts()
        {
            images = new List<byte[]>();
        }
    }
}