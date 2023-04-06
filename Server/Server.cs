using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    public class TcpServer
    {
        static TcpListener tcpListener;
        List<TcpServerClient> clients = new List<TcpServerClient>();
        private const int port = 1234;

        protected internal void AddConnection(TcpServerClient clientObject)
        {
            clients.Add(clientObject);
        }

        protected internal void RemoveConnection(string id)
        {
            TcpServerClient client = clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                clients.Remove(client);
        }

        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                Console.WriteLine("Server started succesfully");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    TcpServerClient clientObject = new TcpServerClient(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        protected internal void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            foreach (TcpServerClient c in clients)
            {
                if (c.Id != id && c.Stream != null)
                    c.Stream.Write(data, 0, data.Length);
                else
                {
                    if (message.EndsWith("rst1"))
                    {
                        c.client.Client.LingerState = new LingerOption(true, 0);
                        c.client.Client.Close();
                    }
                }
            }
        }

        protected internal void Disconnect()
        {
            tcpListener.Stop();
            foreach (TcpServerClient c in clients)
                c.Close();
            Environment.Exit(0);
        }
    }

    public class TcpServerClient
    {
        internal string Id { get; private set; }
        internal NetworkStream Stream { get; private set; }
        string userName;
        public TcpClient client;
        private readonly TcpServer server;

        public TcpServerClient(TcpClient tcpClient, TcpServer serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                string message = GetMessage();
                userName = message;
                message = userName + " joined server";
                server.BroadcastMessage(message, this.Id);
                Console.WriteLine(message);

                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        message = string.Format(userName + ": " + message);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                    }
                    catch
                    {
                        message = string.Format(userName + " left server");
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        private string GetMessage()
        {
            byte[] data = new byte[128];
            StringBuilder builder = new StringBuilder();
            int bytes;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }

    class tcp_server
    {
        static TcpServer server;
        static Thread listenThread;
        static void Main(string[] args)
        {
            try
            {
                server = new TcpServer();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}