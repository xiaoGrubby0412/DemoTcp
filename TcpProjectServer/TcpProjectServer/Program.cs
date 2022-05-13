using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpProjectServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            int port = 2000;
            string host = "127.0.0.1";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(ipe);

            while (true)
            {
                i++;
                try 
                {
                    Console.WriteLine("\t-----------------------------------");
                    Console.Write("Perform operations {0} :", i);
                    s.Listen(0);//开始监听
                    Console.WriteLine("1. wait for connect...");

                    //实例化一个新的socket端口
                    Socket temp = s.Accept();
                    Console.WriteLine("2. Get a connect");

                    //接收客户端发的消息并做解码处理
                    string recvStr = "";
                    byte[] recvBytes = new byte[1024];
                    int bytes;
                    bytes = temp.Receive(recvBytes, recvBytes.Length, 0);//从客户端接收信息
                    recvStr = Encoding.UTF8.GetString(recvBytes, 0, bytes);
                    Console.WriteLine("3. Server Get Message:{0}", recvStr);

                    //返回给客户端连接成功的消息
                    string sendStr = "Ok!Client send message successfull";
                    byte[] bs = Encoding.UTF8.GetBytes(sendStr);
                    temp.Send(bs, bs.Length, 0); //返回客户端成功消息

                    //关闭端口
                    temp.Close();
                    Console.WriteLine("4. Completed...");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("");
                    //s.Close(); //关闭 socket (由于在死循环中 所以不用写 但如果是单个接收 实例 socket 并完成任务后 需关闭)

                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
            }
        }
    }
}
