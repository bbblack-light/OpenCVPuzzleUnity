using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

namespace Models
{
    public class MySprites
    {
        public static Action SpritesLoaded = delegate {  };
        
        public static MySprites Instance => _instance ?? (_instance = new MySprites());
        private static MySprites _instance;
        
        public readonly List<OneImage> Images;
        public readonly List<OneImage> SortedImages;
        public int Height;
        public int Width;

        public MySprites()
        {
            Images = new List<OneImage>();
            SortedImages = new List<OneImage>();
        }

        public void LoadSprites()
        {
            Images.Clear();
            SortedImages.Clear();
            
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
                tex.LoadImage(image.array);
                var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width/2.0f, tex.height/2.0f));
                
                Images.Add(new OneImage()
                {
                    sprite = sprite,
                    number = image.number
                });
            }

            Height = parts.height;
            Width = parts.width;
            
            foreach (var oneImage in Images)
            {
                SortedImages.Add(oneImage);
            }

            Randomize(Images);
            SortedImages.Sort((x,y) => x.number > y.number ? 1 : -1);
            
            SpritesLoaded.Invoke();
        }
        
        public static void Randomize<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();
            Random rnd = new Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); //pick a random item from the master list
                randomizedList.Add(list[index]); //place it at the end of the randomized list
                list.RemoveAt(index);
            }
            list.Clear();
            foreach (var item in randomizedList)
            {
                list.Add(item);
            }
        }
    }
    
    public class OneImage
    {
        public int number;
        public Sprite sprite;
    }
}