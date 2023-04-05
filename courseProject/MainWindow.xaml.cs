using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace courseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartServer(object sender, RoutedEventArgs e)
        {
            ServerWindow server = new ServerWindow();
            server.Show();
        }
        private void StartServerButton_MouseEnter(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                StartServerEffects.ShadowDepth = -i;
                StartServerEffects.BlurRadius = i;
            }
            StartServerEffects.Color = Color.FromRgb(92, 227, 80);
        }

        private void StartServerButton_MouseLeave(object sender, MouseEventArgs e)
        {
            for (int i = 20; i > 0; i--)
            {
                StartServerEffects.ShadowDepth = -i;
                StartServerEffects.BlurRadius = i;
            }
            StartServerEffects.Color = Color.FromRgb(0, 0, 0);
        }
        private void StartClient(object sender, RoutedEventArgs e)
        {
            ClientWindow client = new ClientWindow();
            client.Show();
        }

        private void StartClientButton_MouseEnter(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                StartClientEffects.ShadowDepth = -i;
                StartClientEffects.BlurRadius = i;
            }
            StartClientEffects.Color = Color.FromRgb(92, 227, 80);
        }

        private void StartClientButton_MouseLeave(object sender, MouseEventArgs e)
        {
            for (int i = 20; i > 0; i--)
            {
                StartClientEffects.ShadowDepth = -i;
                StartClientEffects.BlurRadius = i;
            }
            StartClientEffects.Color = Color.FromRgb(0, 0, 0);
        }
    }
}
