using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private MainLoadPanelComponent _mainLoadPanel;
    [SerializeField] private SpritesViewComponent _spritesViewComponent;

    private void Start()
    {
        _mainLoadPanel.Initialize();
        _spritesViewComponent.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
