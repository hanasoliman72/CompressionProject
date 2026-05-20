using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
namespace CompressionClient
{
    public partial class Form1 : Form
    {
        Socket clientSocket;
        string filePath;
        byte[] fileData;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Close old socket if already connected
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            
            // 1. Connect to the Server
            try
            {
                IPAddress host = IPAddress.Parse(IPTextBox.Text);
                IPEndPoint endPoint = new IPEndPoint(host, int.Parse(portTextBox.Text));
                clientSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(endPoint);
                logBox.Text += "Connected to server: " + endPoint.ToString() + Environment.NewLine;
            }
            catch(Exception ex)
            {
                logBox.Text += "Connection error: " + ex.Message + Environment.NewLine;
            }
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            // 2. Browse and Load File
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                fileData = File.ReadAllBytes(filePath);
                logBox.Text += "File loaded: " + filePath + Environment.NewLine;
            }
        }

        private async void send_button_Click(object sender, EventArgs e)
        {
            // 3. Send File to Server
            try
            {
                await clientSocket.SendAsync(new ArraySegment<byte>(BitConverter.GetBytes((long)fileData.Length)), 
                    SocketFlags.None);
                await clientSocket.SendAsync(new ArraySegment<byte>(fileData), SocketFlags.None);
                logBox.Text += "File sent to the server" + Environment.NewLine;
            }
            catch(Exception ex)
            {
                logBox.Text += "Error: " + ex.Message + Environment.NewLine;
            }
        }

        private async void receive_button_Click(object sender, EventArgs e)
        {
            // Receive compressed file from the server
            try
            {
                // 1.Receive 8 bytes => compressed file size
                byte[] len = new byte[8];
                await ReceiveExactlyAsync(clientSocket, len);
                long compressedSize = BitConverter.ToInt64(len, 0);
                logBox.Text += "Compressed file size: " + compressedSize + " bytes" + Environment.NewLine;

                // 2.Receive exactly that many bytes → compressed data
                byte[] compressedData = new byte[compressedSize];
                await ReceiveExactlyAsync(clientSocket, compressedData);
                logBox.Text += "File received successfully." + Environment.NewLine;

                // 3.Save the file
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "GZip Files (*.gz)|*.gz";
                saveDialog.FileName = Path.GetFileName(filePath) + ".gz";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    await Task.Run(() =>
                    {
                        File.WriteAllBytes(saveDialog.FileName, compressedData);
                    });
                    logBox.Text += "File saved: " + saveDialog.FileName + Environment.NewLine;
                    logBox.Text += "Original size: " + fileData.Length + " bytes" + Environment.NewLine;
                    logBox.Text += "Compressed size: " + compressedSize + " bytes" + Environment.NewLine;
                }

                // After saving the file successfully
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                logBox.Text += "Connection closed." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                logBox.Text += "Error: " + ex.Message + Environment.NewLine;
            }
        }

        private async Task ReceiveExactlyAsync(Socket socket, byte[] buffer)
        {
            int totalReceived = 0;
            while (totalReceived < buffer.Length)
            {
                int received = await socket.ReceiveAsync(
                    new ArraySegment<byte>(buffer, totalReceived, buffer.Length - totalReceived),
                    SocketFlags.None);
                if (received == 0) throw new Exception("Connection closed by server.");
                totalReceived += received;
            }
        }
    }
}
