using Models;
using UnityEngine;
using UnityEngine.UI;

public class SpritesViewComponent : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Image imagePrefab;
    private void Awake()
    {
        gameObject.SetActive(false);
        
        MySprites.SpritesLoaded +=SpritesLoaded;
    }

    private void SpritesLoaded()
    {
        grid.cellSize = new Vector2(MySprites.Instance.Width, MySprites.Instance.Height);
        
        grid.constraintCount = FullParts.cols;
        
        for (int i = 0; i < MySprites.Instance.Images.Count; i++)
        {
            var a = Instantiate(imagePrefab, grid.gameObject.transform);
            a.sprite = MySprites.Instance.Images[i];
        }
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        MySprites.SpritesLoaded -= SpritesLoaded;
    }
}
