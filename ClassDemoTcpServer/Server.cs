using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoTcpServer
{
    public class Server
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
            
            DoOneClient(socket);
            
            server.Stop();
        }

        private void DoOneClient(TcpClient socket)
        {
            Console.WriteLine($"Min egen (IP, port) = {socket.Client.LocalEndPoint}");
            Console.WriteLine($"Accepteret client (IP, port) = {socket.Client.RemoteEndPoint}");


            // åbner for tekst strenge
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());

            // læser linje fra nettet
            string l = sr.ReadLine();


            // hvis tælle ord
            string[] strings = l.Split();
            int antalord = strings.Length;

            Console.WriteLine("Modtaget: " + l);
            // skriver linje tilbage - stadig ekko
            sw.WriteLine(l);
            sw.Flush();

            sr?.Close();
            sw?.Close();
        }
    }
}
