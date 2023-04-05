using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace courseProject
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TcpClient client = new TcpClient("localhost", 1234);
            //Console.WriteLine("Connected to server");
            Info.Text = "Connected to server\n";

            NetworkStream stream = client.GetStream();

            string message = "Hello, server!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] encryptedData = ProtectedData.Protect(
                messageBytes,
                null,
                DataProtectionScope.LocalMachine);

            stream.Write(encryptedData, 0, encryptedData.Length);

            byte[] responseEncrypted = new byte[1024];
            int bytes = stream.Read(responseEncrypted, 0, responseEncrypted.Length);

            byte[] responseBytes = ProtectedData.Unprotect(
                responseEncrypted,
                null,
                DataProtectionScope.LocalMachine);

            string responseMessage = Encoding.UTF8.GetString(responseBytes, 0, bytes);
            //Console.WriteLine("Received response: {0}", responseMessage);
            Info.Text = "Received response: " + responseMessage + "\n";

            client.Close();
            //Console.WriteLine("Disconnected from server");
            Info.Text = "Disconnected from server\n";
        }
    }
}
