using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void conectar(string comand)
        {
            IPEndPoint ie = null;
            string ipServer = txtIp.Text;
            int pEnlace;           
            bool p = Int32.TryParse(txtEnlace.Text,out pEnlace); 
            bool oK = true;
            if (ipServer != null && p) {
                string msg;
                try
                {
                ie = new IPEndPoint(IPAddress.Parse(ipServer), pEnlace);
                }
                catch (System.FormatException)
                {
                    txtRespuesta.Text = "IP or EnlaceDoor incorect, change the values ";
                    oK = false;
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    txtRespuesta.Text = "Range of the EnlaceDoor incorrect , change the values";
                    oK = false;
                }
                if (oK) {
                    txtRespuesta.Text += "Starting client\r\n";
                    using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        try
                        {
                            server.Connect(ie);
                        }
                        catch (SocketException e)
                        {
                            txtRespuesta.Text += String.Format("Error connection: {0}\nError code: {1}({2})\r\n",
                                                e.Message, (SocketError)e.ErrorCode, e.ErrorCode);
                            return;
                        }
                        using (NetworkStream ns = new NetworkStream(server))
                        using (StreamReader sr = new StreamReader(ns))
                        using (StreamWriter sw = new StreamWriter(ns))
                        {

                            msg = sr.ReadLine();
                            txtRespuesta.Text += msg + "\r\n";

                        
                            sw.WriteLine(comand);
                            sw.Flush();
                    

                            msg = sr.ReadLine();
                            txtRespuesta.AppendText(msg + "\r\n");

                            txtRespuesta.AppendText("Ending Conection\r\n");
                        }
                    }
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            conectar(((Button)sender).Text.ToUpper());
        }


    }
}
