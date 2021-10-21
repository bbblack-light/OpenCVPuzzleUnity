using Models;
using UnityEngine;
using UnityEngine.UI;

public class SpritesViewComponent : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private GridLayoutGroup completedGrid;
    [SerializeField] private ImageComponent imagePrefab;

    private ImageComponent firstImage;
    private ImageComponent secondImage;
    private void Awake()
    {
        gameObject.SetActive(false);
        
        MySprites.SpritesLoaded +=SpritesLoaded;
    }

    private void SpritesLoaded()
    {
        grid.cellSize = new Vector2(MySprites.Instance.Width, MySprites.Instance.Height);
        grid.constraintCount = FullParts.cols;
        
        completedGrid.cellSize = new Vector2(MySprites.Instance.Width, MySprites.Instance.Height);
        completedGrid.constraintCount = FullParts.cols;
        
        for (int i = 0; i < MySprites.Instance.Images.Count; i++)
        {
            var a = Instantiate(imagePrefab, grid.gameObject.transform);
            a.SetImage(MySprites.Instance.Images[i].sprite);
            a.SetListener(() =>
            {
                if (firstImage == null)
                {
                    firstImage = a;
                    return;
                }

                if (secondImage == null)
                {
                    secondImage = a;
                }

                var firstSprite = firstImage.GetImage();
                firstImage.SetImage(secondImage.GetImage());
                secondImage.SetImage(firstSprite);

                secondImage = null;
                firstImage = null;
            });
        }

        for (int i = 0; i < MySprites.Instance.SortedImages.Count; i++)
        {
            var a = Instantiate(imagePrefab, completedGrid.gameObject.transform);
            a.SetImage(MySprites.Instance.SortedImages[i].sprite);
        }
        gameObject.SetActive(true);
    }
    
    private void OnDestroy()
    {
        MySprites.SpritesLoaded -= SpritesLoaded;
    }
}
