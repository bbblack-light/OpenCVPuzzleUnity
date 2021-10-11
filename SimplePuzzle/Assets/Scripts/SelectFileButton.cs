using System;
using System.Collections;
using System.ComponentModel;
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
            //for send
            Client.Connect("localhost", 11000);

            Send(paths[0]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public UdpClient Client;

    private void Send(String path)
    {
        var imageParameters = new ImageParameters()
        {
            path = path,
            cols = 2,
            rows = 1
        };
        string message = JsonConvert.SerializeObject(imageParameters);
        byte[] sendData = Encoding.ASCII.GetBytes(message);
        Client.Send(sendData, sendData.Length);
        StartCoroutine(Receive());
        // PlayersUpdater.Instance.players = (JsonUtility.FromJson<Container>(receiveString)).PlayerInfos;
    }

    private IEnumerator Receive()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            
            //for receive
            IPEndPoint senderIp = new IPEndPoint(IPAddress.Any, 12000);
            
            if (Client.Available == 0) continue;
            
            var receiveData = Client.Receive(ref senderIp);
            string receiveString = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length);
            Debug.Log(receiveString);
            StopCoroutine(Receive());
        }
    }
}