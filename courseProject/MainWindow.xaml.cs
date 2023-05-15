using System.Diagnostics;
using System.IO;
using System.Threading;
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
        private void StartServer(object sender, RoutedEventArgs e)
        {
            string path = @"C:\AboznyStorage\study\courseWork\Server\bin\Debug\net6.0\Server.exe";
            string processName = Path.GetFileNameWithoutExtension(path);
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    StartServerEffects.ShadowDepth = -i;
                    StartServerEffects.BlurRadius = i;
                }
                StartServerEffects.Color = Color.FromRgb(92, 227, 80);
            }
            else
            {
                Thread thread = new Thread(() =>
                {
                    Process process = new Process();
                    process.StartInfo.FileName = path;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                });

                thread.Start();
            }
        }
        private void StartClient(object sender, RoutedEventArgs e)
        {
            string path = @"C:\AboznyStorage\study\courseWork\Client\bin\Debug\net6.0\Client.exe";
            Thread thread = new Thread(() =>
            {
                Process process = new Process();
                process.StartInfo.FileName = path;
                process.StartInfo.UseShellExecute = false;
                process.Start();
            });

            thread.Start();
        }
        private void ShutdownServer(object sender, RoutedEventArgs e)
        {
            string processName = "Server";
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                process.Kill();
            }

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

        private void ShutdownButton_MouseEnter(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                ShutdownButtonEffects.ShadowDepth = -i;
                ShutdownButtonEffects.BlurRadius = i;
            }
            ShutdownButtonEffects.Color = Color.FromRgb(227, 80, 80);
        }

        private void ShutdownButton_MouseLeave(object sender, MouseEventArgs e)
        {
            for (int i = 20; i > 0; i--)
            {
                ShutdownButtonEffects.ShadowDepth = -i;
                ShutdownButtonEffects.BlurRadius = i;
            }
            ShutdownButtonEffects.Color = Color.FromRgb(0, 0, 0);
        }
    }
}
