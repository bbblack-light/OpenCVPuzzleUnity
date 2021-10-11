using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;

namespace DefaultNamespace
{
    [Serializable]
    public class Response
    {
        public List<Image<Bgr, byte>> images;
        public int height;
        public int weight;
    }
}