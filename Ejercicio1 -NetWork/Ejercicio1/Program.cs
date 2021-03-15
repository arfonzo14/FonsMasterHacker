using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Ejercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ie;
            bool flag = true;
            bool pEnlaceOK;
            int i = 135;
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) 
            {

                do
                {
                    pEnlaceOK = true;
                    ie = new IPEndPoint(IPAddress.Any, i);
                    try
                    {
                        s.Bind(ie);
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        pEnlaceOK = false;
                        i++;
                    }
                } while (!pEnlaceOK);

                Console.WriteLine("Puerta de enlace "+i);

                s.Listen(10);
                while (flag)
                {
                    Socket sClient = s.Accept();
                    IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;
                    Console.WriteLine("Client connected:{0} at port {1}", ieClient.Address, ieClient.Port);

                    using (NetworkStream ns = new NetworkStream(sClient))
                    using (StreamReader sr = new StreamReader(ns))
                    using (StreamWriter sw = new StreamWriter(ns))
                    {

                        string welcome = "Welcome";
                        sw.WriteLine(welcome);
                        sw.Flush();

                        string msg;

                        try
                        {
                            msg = sr.ReadLine();
                            Console.WriteLine(msg != null ? msg : "Client disconnected");
                            switch (msg)
                            {
                                case "HORA":
                                    msg = String.Format("Hora: {0}/{1}/{2}\r\n", DateTime.Now.TimeOfDay.Hours,
                                                                        DateTime.Now.TimeOfDay.Minutes,
                                                                        DateTime.Now.TimeOfDay.Seconds);
                                    break;
                                case "FECHA":
                                    msg = String.Format("Fecha: {0}/{1}/{2}\r\n", DateTime.Now.Day,
                                                                       DateTime.Now.DayOfWeek,
                                                                       DateTime.Now.Year);
                                    break;
                                case "TODO":
                                    msg = String.Format("Fecha y hora: {0}\r\n ", DateTime.Now);
                                    break;
                                case "APAGAR":
                                    msg = "Closing server\r\n";
                                    flag = false;
                                    break;
                            }
                            sw.WriteLine(msg);
                            sw.Flush();
                        }
                        catch (IOException e)
                        {
                            break;
                        }
                        sClient.Close();
                    }
                    Console.WriteLine("Connection closed\r\n");
                }
            }
        }
    }
}
