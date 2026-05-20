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
            // 1.Create the Socket and Bind
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 5090);
            Socket welcomingSocket = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp);
            welcomingSocket.Bind(ipEndPoint);
            welcomingSocket.Listen(10); // 2.Start Listening
            Console.WriteLine("Server is listening on port 5090...");

            while (true)
            {
                // 3.Accept Clients in a Loop
                Socket handlingSocket = welcomingSocket.Accept();
                IPEndPoint clientIpEndPoint = (IPEndPoint)handlingSocket.RemoteEndPoint;
                Console.WriteLine("Client connected: " + clientIpEndPoint.Address.ToString());

                // 4.Spawn a Thread per Client
                Thread thread = new Thread(() => HandleClient(handlingSocket));
                thread.Start();
            }
        }

        static void HandleClient(Socket clientSocket)
        {
            // Handle client communication here
            try
            {
                // 1. Receive file size (8 bytes for long)
                byte[] len = ReadExactly(clientSocket, 8);
                long fileSize = BitConverter.ToInt64(len, 0);
                Console.WriteLine("Incoming file size: " + fileSize + " bytes");

                // 2.Receive the file bytes
                byte[] fileData = ReadExactly(clientSocket, (int)fileSize);
                Console.WriteLine("File received successfully.");

                // 3.Compress the file bytes
                byte[] compressedData = Compress(fileData);
                Console.WriteLine("File compressed: " + compressedData.Length + " bytes");

                // 4.Send the compressed size (8 bytes)
                byte[] compressedSize = BitConverter.GetBytes((long)compressedData.Length);
                clientSocket.Send(compressedSize);

                // 5.Send the compressed file bytes
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
