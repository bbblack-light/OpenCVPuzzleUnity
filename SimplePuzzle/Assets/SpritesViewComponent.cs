using Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpritesViewComponent : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup imageForComplete;
    [SerializeField] private GridLayoutGroup completedGrid;
    [SerializeField] private ImageComponent imagePrefab;

    private ImageComponent firstImage;
    private ImageComponent secondImage;
    
    public void Initialize()
    {
        gameObject.SetActive(false);
        completedGrid.gameObject.SetActive(false);
        MySprites.SpritesLoaded +=SpritesLoaded;
    }

    private void SpritesLoaded()
    {
        imageForComplete.cellSize = new Vector2(MySprites.Instance.Width, MySprites.Instance.Height);
        imageForComplete.constraintCount = FullParts.cols;
        
        completedGrid.cellSize = new Vector2(MySprites.Instance.Width, MySprites.Instance.Height);
        completedGrid.constraintCount = FullParts.cols;
        
        for (int i = 0; i < MySprites.Instance.Images.Count; i++)
        {
            var a = Instantiate(imagePrefab, imageForComplete.gameObject.transform);
            a.SetImage(MySprites.Instance.Images[i].sprite);
            a.SetListener(() =>
            {
                if (firstImage == null)
                {
                    firstImage = a;
                    firstImage.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    return;
                }

                if (secondImage == null)
                {
                    secondImage = a;
                    secondImage.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                }

                var firstSprite = firstImage.GetImage();
                firstImage.SetImage(secondImage.GetImage());
                secondImage.SetImage(firstSprite);

                secondImage.transform.localScale = Vector3.one;
                firstImage.transform.localScale = Vector3.one;
                
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventSystem.current.SetSelectedGameObject(null);
            completedGrid.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            completedGrid.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        MySprites.SpritesLoaded -= SpritesLoaded;
    }
}
