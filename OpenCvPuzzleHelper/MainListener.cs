using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DefaultNamespace;
using Emgu.CV;
using Emgu.CV.Structure;
using Newtonsoft.Json;

namespace OpenCvPuzzleHelper
{
    class MainListener
    {
        
        public static UdpClient server = new UdpClient(11000);
        public static byte[] bytes = new byte[1024];
        
        static void Main(string[] args)
        {
            ImageParameters parameters = new ImageParameters()
            {
                Path = "C:\\Users\\Елена\\Desktop\\therg wolf deathloop.jpg",
                Rows = 2,
                Cols = 1
            };
            
            ImageProcessing.CutImage(parameters);

            //TODO 11 October 2021 г.: return for listen server
            // StartListen();
        }
        
        private static void StartListen()
        {
            byte[] sendData;
            Console.WriteLine("Server started.\nWaiting for images...");
            try
            {
                while (true)
                {
                    IPEndPoint senderIp = new IPEndPoint(IPAddress.Any, 11000);
                    
                    var receiveData = server.Receive(ref senderIp);
                    var message = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length);

                    ImageParameters imageParameters = JsonConvert.DeserializeObject<ImageParameters>(message);

                    Console.WriteLine("Image parameters is ", message);

                    //TODO 11 October 2021 г.: make a send data
                    // sendData = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new Container(players)));
                    // server.Send(sendData, sendData.Length, senderIP);
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