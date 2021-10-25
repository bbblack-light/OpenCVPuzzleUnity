using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using Models;
using Newtonsoft.Json;
using SFB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainLoadPanelComponent : MonoBehaviour
{
    [SerializeField] private Button Button;
    [SerializeField] private TMP_InputField RowsText;
    [SerializeField] private TMP_InputField ColsText;
    [SerializeField] private GameObject PropertyPanel;
    [SerializeField] private GameObject PleaseWait;

    private TcpClient _client;
    private NetworkStream _stream;
    public ImageParameters imageParameters;

    public void Initialize()
    {
        gameObject.SetActive(true);
        PropertyPanel.SetActive(true);
        PleaseWait.SetActive(false);
        
        Button.onClick.AddListener(Call);
        
        MySprites.SpritesLoaded += SpritesLoaded;
    }

    private void SpritesLoaded()
    {
        PleaseWait.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Call()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        Debug.Log(paths[0]);

        try
        {
            Send(paths[0]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void Send(String path)
    {
        imageParameters = new ImageParameters()
        {
            path = path
        };

        Int32.TryParse(RowsText.text, out imageParameters.rows);
        Int32.TryParse(ColsText.text, out imageParameters.cols);

        if (imageParameters.rows == 0 || imageParameters.cols == 0)
        {
            Debug.Log("rows or cols must be > 0");
            return;
        }
        if (imageParameters.rows > 30 || imageParameters.cols > 30)
        {
            Debug.Log("rows or cols must be <= 30");
            return;
        }
        PropertyPanel.SetActive(false);
        PleaseWait.SetActive(true);
        
        _client = new TcpClient("127.0.0.1", 11000);
        _stream = _client.GetStream();
        string message = JsonConvert.SerializeObject(imageParameters);
        byte[] sendData = Encoding.ASCII.GetBytes(message);
        _stream.Write(sendData, 0, sendData.Length);
        StartCoroutine(Receive());
    }

    private IEnumerator Receive()
    {
        
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (_client.Available == 0) continue;

            var receiveData = new byte[1024];
            _stream.Read(receiveData, 0, receiveData.Length);
            string fileName = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length)
                .Replace("\0", string.Empty);
            if (!fileName.Contains("            "))
            {
                FullParts.cols = imageParameters.cols;
                FullParts.rows = imageParameters.rows;
                FullParts.Filename = fileName; 
                _stream.Close();
                StopCoroutine(Receive());
                break;
            }
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(Receive());
        MySprites.SpritesLoaded -= SpritesLoaded;
    }
}