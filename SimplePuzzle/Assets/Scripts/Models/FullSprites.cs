using System;
using System.Collections.Generic;

namespace Models
{
    public class FullSprites
    {
        public static Action FileNameReceived = delegate {  };
        
        public static FullSprites fullSprites = new FullSprites();
        
        public List<FullSprites> images;

        public FullSprites()
        {
            images = new List<FullSprites>();
        }

        public static void LoadSprites()
        {
            if (FullParts.filename == String.Empty) return;

            //TODO 13 October 2021 г.: make a loading sprites from byte
        }
    }
}