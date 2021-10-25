using System;
using Models;
using UnityEngine;

public class SpritesLoading : MonoBehaviour
{
    private void OnEnable()
    {
        FullParts.FileNameReceived += FileNameReceived;
    }

    private void FileNameReceived()
    {
        MySprites.Instance.LoadSprites();
    }

    private void OnDestroy()
    {
        FullParts.FileNameReceived -= FileNameReceived;
    }
}
