using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System;

namespace WindowsFormsApp1
{
    partial class Srvr
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            Text = "Asynknorous Multithread TCP Server Program";
            Size = new Size(400, 380);

            Label label1 = new Label();
            label1.Parent = this;
            label1.Text = "Enter text string:";
            label1.AutoSize = true;
            label1.Location = new Point(10, 30);
            newText = new TextBox();
            newText.Parent = this;
            newText.Size = new Size(200, 2 * Font.Height);
            newText.Location = new Point(10, 55);
            results = new ListBox();
            results.Parent = this;
            results.Location = new Point(10, 85);
            results.Size = new Size(360, 18 * Font.Height);
            Button sendit = new Button();
            sendit.Parent = this;
            sendit.Text = "Send";
            sendit.Location = new Point(220, 52);
            sendit.Size = new Size(5 * Font.Height, 2 * Font.Height);
            sendit.Click += new EventHandler(ButtonSendOnClick);
            Button listen = new Button();
            listen.Parent = this;
            listen.Text = "Listen";
            listen.Location = new Point(295, 52);
            listen.Size = new Size(6 * Font.Height, 2 * Font.Height);
            listen.Click += new EventHandler(ButtonListenOnClick);
        }
        void ButtonListenOnClick(object obj, EventArgs ea)
        {
            if (serverSocket != null)
            {
                serverSocket.Close();
                serverSocket = null;
            }
            results.Items.Add("Listening for a client...");
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            newsock.Bind(iep);
            newsock.Listen(5);
            newsock.BeginAccept(new AsyncCallback(AcceptConn), newsock);
            results.Items.Add("Server started on port " + port);

        }

        void ButtonSendOnClick(object obj, EventArgs ea)
        {
        byte[] message = Encoding.ASCII.GetBytes(newText.Text);
            newText.Clear();
            serverSocket.BeginSend(message, 0, message.Length, 0,
            new AsyncCallback(SendData), serverSocket);
        }
        void AcceptConn(IAsyncResult iar)
        {
             Socket oldserver = (Socket)iar.AsyncState;
        Socket clientSocket = oldserver.EndAccept(iar);
        clientSockets.Add(clientSocket);
        results.Items.Add("Connection from: " + clientSocket.RemoteEndPoint.ToString());
        clientSocket.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveData), clientSocket);
        oldserver.BeginAccept(new AsyncCallback(AcceptConn), oldserver);
   
        }
        void Connected(IAsyncResult iar)
        {
            try
            {
                serverSocket.EndConnect(iar);
                results.Items.Add("Connected to: " + serverSocket.RemoteEndPoint.ToString());
                Thread receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();
            }
            catch (SocketException)
            {
                results.Items.Add("Error connecting");
            }
        }
        void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
        }
        void ReceiveData()
        {
            Socket clientSocket = (Socket)iar.AsyncState;
            int recv = clientSocket.EndReceive(iar);
            if (recv == 0)
            {
                clientSockets.Remove(clientSocket);
                clientSocket.Close();
                results.Items.Add("Client disconnected.");
                return;
            }

            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            results.Items.Add("Received: " + stringData);

            if (stringData == "bye")
            {
                byte[] message = Encoding.ASCII.GetBytes("bye");
                clientSocket.Send(message);
                clientSockets.Remove(clientSocket);
                clientSocket.Close();
                results.Items.Add("Connection stopped");
                return;
            }

            // Mesajı tüm bağlı istemcilere gönder
            foreach (var socket in clientSockets)
            {
                if (socket != clientSocket)
                {
                    socket.Send(data, recv, SocketFlags.None);
                }
            }

            clientSocket.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveData), clientSocket);
        }
    
        #endregion
    }
}

