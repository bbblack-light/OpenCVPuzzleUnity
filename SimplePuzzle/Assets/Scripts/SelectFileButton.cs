using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Models;
using Newtonsoft.Json;
using SFB;
using UnityEngine;
using UnityEngine.UI;

public class SelectFileButton : MonoBehaviour
{
    private Button _button;
    public TcpClient Client;
    public NetworkStream stream;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Call);
        
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
        Client = new TcpClient("127.0.0.1", 11000);
        stream = Client.GetStream();
        var imageParameters = new ImageParameters()
        {
            path = path,
            cols = 30,
            rows = 30
        };
        string message = JsonConvert.SerializeObject(imageParameters);
        byte[] sendData = Encoding.ASCII.GetBytes(message);
        stream.Write(sendData, 0, sendData.Length);
        StartCoroutine(Receive());
        // PlayersUpdater.Instance.players = (JsonUtility.FromJson<Container>(receiveString)).PlayerInfos;
    }

    private IEnumerator Receive()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            
            //for receive
            IPEndPoint senderIp = new IPEndPoint(IPAddress.Any, 12000);
            
            if (Client.Available == 0) continue;
            
            //For receive parameters
            var receiveData = new byte[1024];
            stream.Read(receiveData, 0, receiveData.Length);
            
            string fileName = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length);
            if (!fileName.Contains("            "))
            {
                FullParts.filename = fileName;
                Debug.Log(fileName);
                stream.Close();
                StopCoroutine(Receive());
                break;
            }
        }
    }
}