using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO.Compression;

namespace CompressionServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 5090);
            Socket welcomingSocket = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp);
            welcomingSocket.Bind(ipEndPoint);
            welcomingSocket.Listen(10); 
            Console.WriteLine("Server is listening on port 5090...");

            while (true)
            {
                Socket handlingSocket = welcomingSocket.Accept();
                IPEndPoint clientIpEndPoint = (IPEndPoint)handlingSocket.RemoteEndPoint;
                Console.WriteLine("Client connected: " + clientIpEndPoint.Address.ToString());
                Thread thread = new Thread(() => HandleClient(handlingSocket));
                thread.Start();
            }
        }

        static void HandleClient(Socket clientSocket)
        {
            try
            {
                byte[] len = ReadExactly(clientSocket, 8);
                long fileSize = BitConverter.ToInt64(len, 0);
                Console.WriteLine("Incoming file size: " + fileSize + " bytes");

                byte[] fileData = ReadExactly(clientSocket, (int)fileSize);
                Console.WriteLine("File received successfully.");

                byte[] compressedData = Compress(fileData);
                Console.WriteLine("File compressed: " + compressedData.Length + " bytes");

                byte[] compressedSize = BitConverter.GetBytes((long)compressedData.Length);
                clientSocket.Send(compressedSize);

                clientSocket.Send(compressedData);
                Console.WriteLine("Compressed file sent to client.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error handling client: " + e.Message);
            }
            finally
            {
                clientSocket.Close();
                Console.WriteLine("Client disconnected.");
            }
        }

        static byte[] ReadExactly(Socket socket, int count)
        {
            byte[] buffer = new byte[count];
            int totalReceived = 0;
            while (totalReceived < count)
            {
                int received = socket.Receive(buffer, totalReceived, count - totalReceived, SocketFlags.None);
                if (received == 0) throw new Exception("Connection closed by client.");
                totalReceived += received;
            }
            return buffer;
        }

        static byte[] Compress(byte[] data)
        {
            using(MemoryStream output = new MemoryStream())
            {
                using (GZipStream gs = new GZipStream(output, CompressionMode.Compress))
                {
                    gs.Write(data, 0, data.Length);
                }
                return output.ToArray();
            }
        }
    }
}
