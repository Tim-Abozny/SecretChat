using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace courseProject
{
    /// <summary>
    /// Interaction logic for ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : Window
    {
        public ServerWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 1234);
            listener.Start();
            //Console.WriteLine("Server started");
            Info.Text = "Server started\n";


            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                //Console.WriteLine("Client connected");
                Info.Text = "Client connected\n";

                NetworkStream stream = client.GetStream();

                byte[] encryptedData = new byte[1024];
                int bytes = stream.Read(encryptedData, 0, encryptedData.Length);

                byte[] decryptedData = ProtectedData.Unprotect(
                    encryptedData,
                    null,
                    DataProtectionScope.LocalMachine);

                string message = Encoding.UTF8.GetString(decryptedData, 0, bytes);
                //Console.WriteLine("Received message: {0}", message);
                Info.Text = "Received message: " + message + "\n";

                byte[] responseBytes = Encoding.UTF8.GetBytes("Message received");
                byte[] responseEncrypted = ProtectedData.Protect(
                    responseBytes,
                    null,
                    DataProtectionScope.LocalMachine);

                stream.Write(responseEncrypted, 0, responseEncrypted.Length);

                client.Close();
                //Console.WriteLine("Client disconnected");
                Info.Text = "Client disconnected\n";

            }
        }
    }
}
