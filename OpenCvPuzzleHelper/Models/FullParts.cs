using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    [Serializable]
    public class FullParts
    {
        public string filename;
        public List<OneImageParams> images;
        public int height;
        public int width;
    }
}