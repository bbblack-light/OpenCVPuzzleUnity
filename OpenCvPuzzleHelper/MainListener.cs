using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DefaultNamespace;
using Emgu.CV;
using Emgu.CV.Structure;
using Newtonsoft.Json;

namespace OpenCvPuzzleHelper
{
    class MainListener
    {
        private static FullParts fullParts;

        //for receive
        private static TcpListener Server;
        private static NetworkStream stream;
        private static List<string> filenames = new List<string>();
        static void Main(string[] args)
        {
            ImageParameters parameters = new ImageParameters()
            {
                path = "C:\\Users\\Елена\\Desktop\\therg wolf deathloop.jpg",
                rows = 2,
                cols = 1
            };

            // ImageProcessing.CutImage(parameters);

            //TODO 11 October 2021 г.: return for listen server
            StartListen();
        }

        private static void StartListen()
        {
            FileStream file;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = 8888;
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
                    // if (client.Available == 0) continue;
                    var message = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length);
                    Console.WriteLine("пришли параметры: " + message);
                    if (message.Contains("path"))
                    {
                        ImageParameters imageParameters = JsonConvert.DeserializeObject<ImageParameters>(message);
                        fullParts = ImageProcessing.CutImage(imageParameters);
                        var resString = JsonConvert.SerializeObject(fullParts);
                        if (File.Exists(fullParts.filename + ".txt"))
                        {
                            File.Delete(fullParts.filename + ".txt");
                        }
                        
                        file = File.Create(fullParts.filename + ".txt");
                        file.Write(Encoding.ASCII.GetBytes(resString));
                        Console.WriteLine(file.Name);
                        // filename = AppDomain.CurrentDomain.BaseDirectory + file.Name;
                        filenames.Add(file.Name);
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
                foreach (var filename in filenames)
                {
                    if (filename != String.Empty && File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                }
                Console.WriteLine("Server closed");
            }
        }
    }
}