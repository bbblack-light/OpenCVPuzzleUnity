using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{
    public class MySprites
    {
        public static Action SpritesLoaded = delegate {  };
        
        public static MySprites Instance => _instance ?? (_instance = new MySprites());
        private static MySprites _instance;
        
        public readonly List<Sprite> Images;
        public int Height;
        public int Width;

        public MySprites()
        {
            Images = new List<Sprite>();
        }

        public void LoadSprites()
        {
            Images.Clear();
            
            if (FullParts.Filename == string.Empty) return;
            if (!File.Exists(FullParts.Filename)) return;

            Debug.Log(FullParts.Filename);
            var fileBytes = File.ReadAllBytes(FullParts.Filename);
            var fileString =  Encoding.ASCII.GetString(fileBytes);
            
            var parts = JsonConvert.DeserializeObject<FullParts>(fileString);
            Debug.Log(parts.height + " " + parts.width + " " + parts.images.Count) ;

            foreach (var image in parts.images)
            {
                var tex = new Texture2D(parts.width,parts.height);
                tex.LoadImage(image);
                var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width/2.0f, tex.height/2.0f));
                
                Images.Add(sprite);
            }

            Height = parts.height;
            Width = parts.width;
            
            SpritesLoaded.Invoke();
        }
    }
}