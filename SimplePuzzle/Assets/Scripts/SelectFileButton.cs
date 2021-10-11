using SFB;
using UnityEngine;
using UnityEngine.UI;

public class SelectFileButton : MonoBehaviour
{
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Call);
    }

    private void Call()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        Debug.Log(paths[0]);
    }
}
