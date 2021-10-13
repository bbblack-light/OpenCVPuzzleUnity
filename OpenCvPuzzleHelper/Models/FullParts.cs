using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;

namespace DefaultNamespace
{
    [Serializable]
    public class FullParts
    {
        public string filename;
        public List<byte[]> images;
        public int height;
        public int width;
    }
}