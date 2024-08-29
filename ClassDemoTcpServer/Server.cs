using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoTcpServer
{
    internal class Server
    {
        private const int PORT = 7;

        public void Start()
        {
            // definerer server med port nummer
            TcpListener server = new TcpListener(PORT);
            server.Start();
            Console.WriteLine("Server startet på port " + PORT);

            // venter på en klient 
            TcpClient socket = server.AcceptTcpClient();


            // åbner for tekst strenge
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());

            // læser linje fra nettet
            string l = sr.ReadLine();

            Console.WriteLine("Modtaget: " + l);
            // skriver linje tilbage
            sw.WriteLine(l);
            sw.Flush();

        }
    }
}
