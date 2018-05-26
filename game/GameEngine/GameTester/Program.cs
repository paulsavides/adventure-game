using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using Pisces.GameEngine.GameContext;
using System.Security.Cryptography;

namespace Pisces.GameEngine.GameTester
{
  class Program
  {
    static string FilePath = @"..\..\game.json";
    static int Main(string[] args)
    {
      DoGameStuff();
      return 1;
      //TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

      //server.Start();
      //Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection...");

      //TcpClient client = server.AcceptTcpClient();
      //Console.WriteLine("a client connected");

      //NetworkStream stream = client.GetStream();

      //while (true)
      //{
      //  while (!stream.DataAvailable) ;

      //  Byte[] bytes = new Byte[client.Available];

      //  stream.Read(bytes, 0, bytes.Length);

      //  string data = Encoding.UTF8.GetString(bytes);

      //  if (new Regex("^GET").IsMatch(data))
      //  {
      //    Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
      //      + "Connection: Upgrade" + Environment.NewLine
      //      + "Upgrade: websocket" + Environment.NewLine
      //      + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
      //        SHA1.Create().ComputeHash(
      //          Encoding.UTF8.GetBytes(
      //              new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
      //            )
      //        )
      //      ) + Environment.NewLine
      //      + Environment.NewLine);

      //    stream.Write(response, 0, response.Length);
      //  }
      //  else
      //  {

      //  }
      //}



      //// done
      //return 0;
    }


    private static void DoGameStuff()
    {
      // build context
      var ctx = GameContextFactory.CreateContext(FilePath);

      // run it
      ctx.RunGameLoop();

    }
  }
}
