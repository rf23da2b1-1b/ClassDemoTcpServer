using ClassDemoTcpServer.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
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

            while (true)
            {
                // venter på en klient 
                TcpClient socket = server.AcceptTcpClient();

                Task.Run(
                    () =>
                    {
                        TcpClient tempsocket = socket;
                        DoOneClient(tempsocket);
                    }
                    );
                
            }
            //server.Stop();
        }

        private void DoOneClient(TcpClient socket)
        {
            Console.WriteLine($"Min egen (IP, port) = {socket.Client.LocalEndPoint}");
            Console.WriteLine($"Accepteret client (IP, port) = {socket.Client.RemoteEndPoint}");


            // åbner for tekst strenge
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());
            
            DoEkko(sr, sw);

            sr?.Close();
            sw?.Close();
        }

        private void DoEkko(StreamReader sr, StreamWriter sw)
        {
            // læser linje fra nettet
            string l = sr.ReadLine();
            Console.WriteLine("Modtaget: " + l);

            // skriver linje tilbage - stadig ekko
            sw.WriteLine(l);
            sw.Flush();
        }

        private void DoCount(StreamReader sr, StreamWriter sw)
        {
            // læser linje fra nettet
            string l = sr.ReadLine();
            Console.WriteLine("Modtaget: " + l);


            // hvis tælle ord
            string[] strings = l.Split();
            int antalord = strings.Length;

            // skriver linje tilbage - stadig ekko
            sw.WriteLine($"Antal ord er {antalord}");
            sw.Flush();
        }


        private PersonRepo _repo = new PersonRepo();
        private void DoCRUD(StreamReader sr, StreamWriter sw)
        {
            // læser linje fra nettet
            string l = sr.ReadLine();
            Console.WriteLine("Modtaget: " + l);

            string resultStr = "";
            switch (l.ToLower())
            {
                case "create":
                    {
                        resultStr = DoCreate(sr);
                        break;
                    }
                case "get all":
                    {
                        resultStr = DoGetAll(sr);
                        break;
                    }
                case "get":
                    {
                        resultStr = DoGet(sr);
                        break;
                    }
                case "delete":
                    {
                        resultStr = DoDelete(sr);
                        break;
                    }
                case "update":
                    {
                        resultStr = DoUpdate(sr);
                        break;
                    }
                default:
                    {
                        resultStr = $"Protokol element {l} er ikke understøttet";
                        break;
                    }
            }

            // skriver linje tilbage - stadig ekko
            sw.WriteLine(resultStr);
            sw.Flush();
        }

      

        private string DoGetAll(StreamReader sr)
        {
            return "Personer [" + string.Join(", ", _repo.GetAll()) + "]";
            
        }

        private string DoGet(StreamReader sr)
        {
            string idStr = sr.ReadLine();
            int id = int.Parse(idStr);

            try
            {
                return "Person: " + _repo.Get(id);
            }
            catch (KeyNotFoundException)
            {
                return $"Person med id {idStr} findes ikke";
            }
        }
        private string DoCreate(StreamReader sr)
        {
            string name = sr.ReadLine();
            string adr = sr.ReadLine();
            string mobile = sr.ReadLine();

            Person newPerson = _repo.Add(new Person(0, name, adr,mobile));
            return newPerson.ToString();
        }

        private string DoCreateJson(StreamReader sr)
        {
            string dataJason = sr.ReadLine();
            Person newPerson = JsonSerializer.Deserialize<Person>(dataJason);

            Person resPerson = _repo.Add(newPerson);
            return newPerson.ToString();
        }

        private string DoDelete(StreamReader sr)
        {
            throw new NotImplementedException();
        }

        private string DoUpdate(StreamReader sr)
        {
            throw new NotImplementedException();
        }




    }
}
