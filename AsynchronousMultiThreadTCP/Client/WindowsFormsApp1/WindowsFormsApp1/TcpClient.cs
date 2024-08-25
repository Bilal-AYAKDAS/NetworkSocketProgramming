using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class TcpClient : Form
    {
        private static TextBox newText;
        private static ListBox results;
        private static Socket client;
        private static byte[] data = new byte[1024];

        public TcpClient()
        {
            InitializeComponent();
        }
    }
}
