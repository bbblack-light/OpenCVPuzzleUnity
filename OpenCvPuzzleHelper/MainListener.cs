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
        //for receive
        public static UdpClient Server = new UdpClient(11000);
        public static byte[] bytes = new byte[1024];
        
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
            //For returning images
            Server.Connect("localhost", 12000);
            byte[] sendData;
            Console.WriteLine("Server started.\nWaiting for images...");
            try
            {
                while (true)
                {
                    //For receive parameters
                    IPEndPoint senderIp = new IPEndPoint(IPAddress.Any, 11000);
                    
                    var receiveData = Server.Receive(ref senderIp);
                    var message = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length);

                    ImageParameters imageParameters = JsonConvert.DeserializeObject<ImageParameters>(message);

                    Console.WriteLine(message);
                    
                    var res = ImageProcessing.CutImage(imageParameters);
                    var resString = JsonConvert.SerializeObject(res);
                    sendData = Encoding.ASCII.GetBytes(resString);
                    
                    Console.WriteLine(resString.Length);
                    
                    Server.Send(sendData, sendData.Length);
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