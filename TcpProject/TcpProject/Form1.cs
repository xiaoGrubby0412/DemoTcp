using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //显示当前时间
                string str = "The current time: ";
                str += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                textBox1.AppendText(str + Environment.NewLine);

                //做好连接准备
                int port = 2000;
                string host = "127.0.0.1";
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //开始连接
                str = "Connect to server...";
                textBox1.AppendText(str + Environment.NewLine);
                c.Connect(ipe);

                //发送消息
                string sendStr = textBox2.Text;
                str = "The message content: " + sendStr;
                textBox1.AppendText(str + Environment.NewLine);
                byte[] bs = Encoding.UTF8.GetBytes(sendStr);
                str = "Send the message to the server...";
                textBox1.AppendText(str + Environment.NewLine);
                c.Send(bs, bs.Length, 0);


                //接收服务器端的反馈信息
                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = c.Receive(recvBytes, recvBytes.Length, 0); //从服务器端接收返回信息
                recvStr += Encoding.UTF8.GetString(recvBytes, 0, bytes);
                str = "The server feedback: " + recvStr; //显示服务器返回信息
                textBox1.AppendText(str + Environment.NewLine);

                c.Close();
               
            }
            catch (ArgumentNullException f)
            {
                string str = "ArgumentNullException: " + f.ToString();
                textBox1.AppendText(str + Environment.NewLine);
            }
            catch (SocketException f)
            {
                string str = "ArgumentNullException: " + f.ToString();
                textBox1.AppendText(str + Environment.NewLine);
            }

            textBox1.AppendText("" + Environment.NewLine);
            textBox2.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
