using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageComponent : MonoBehaviour
{
    private Button _button;
    private Image _image;
    private UnityAction _action;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    public void SetListener(UnityAction action)
    {
        _action = action;
        _button.onClick.AddListener(_action);
    }

    public void SetImage(Sprite sprite)
    {
        if (_button == null || _image == null)
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }
        _image.sprite = sprite;
    }

    public Sprite GetImage()
    {
        return _image.sprite;
    }

    private void OnDestroy()
    {
        if (_action != null) _button.onClick.RemoveListener(_action);
    }
}
