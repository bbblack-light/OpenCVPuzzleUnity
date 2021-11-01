using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DefaultNamespace;
using Newtonsoft.Json;

namespace OpenCvPuzzleHelper
{
    class MainListener
    {
        private static FullParts fullParts;

        private static TcpListener Server;
        private static NetworkStream stream;
        private static List<string> filenames = new List<string>();
        static void Main(string[] args)
        {
            
            StartListen();
        }

        private static void StartListen()
        {
            FileStream file;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            Server = new TcpListener(localAddr, 11000);
            Server.Start();
            Console.WriteLine("Server started.\nWaiting for images...");
            try
            {
                while (true)
                {
                    TcpClient client = Server.AcceptTcpClient();
                    stream = client.GetStream();
                    
                    var receiveData = new byte[1024];
                    stream.Read(receiveData, 0, receiveData.Length);
                    var message = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length);
                    Console.WriteLine("пришли параметры: " + message);
                    if (message.Contains("path"))
                    {
                        ImageParameters imageParameters = JsonConvert.DeserializeObject<ImageParameters>(message);
                        fullParts = ImageProcessing.CutImage(imageParameters);
                        var resString = JsonConvert.SerializeObject(fullParts);
                        if (File.Exists("my.txt"))
                        {
                            File.Delete("my.txt");
                        }
                        
                        file = File.Create("my.txt");
                        file.Write(Encoding.ASCII.GetBytes(resString));
                        Console.WriteLine(file.Name);
                        var filenameBytes = Encoding.ASCII.GetBytes(file.Name);
                        stream.Write(filenameBytes, 0, filenameBytes.Length);
                        file.Close();
                    }
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Server closed");
            }
        }
    }
}