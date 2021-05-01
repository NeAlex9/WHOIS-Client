using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    public class TCP
    {
        public string GetData(NetworkStream stream)
        {
            StringBuilder response = new StringBuilder();
            byte[] data = new byte[256];
            do
            {
                int bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.ASCII.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            return response.ToString();
        }

        public void SendData(NetworkStream stream, string message)
        {
            message = message + "\r\n";
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void tcp()
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", 43);    //whois.iana.org
                NetworkStream stream = client.GetStream();
                string message = "googe.com";  
                SendData(stream, message);
                Console.WriteLine("Client: " + message + "\n");
                string serverSend = GetData(stream);
                Console.WriteLine("Server: " + serverSend);
                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            TCP tcp = new TCP();
            tcp.tcp();
            Console.WriteLine(" Запрос завершен...");
            Console.ReadLine();

        }
    }
}
