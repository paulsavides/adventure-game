using System.Net.Sockets;
using System.Net;
using System;
using Pisces.GameEngine.GameContext;

namespace Pisces.GameEngine.SocketGameIO
{
  class WebSocketGameIO : IGameIO
  {
    public WebSocketGameIO()
    {
      TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

      server.Start();
      Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection...");

      TcpClient client = server.AcceptTcpClient();
      Console.WriteLine("a client connected");
    }

    public void Error(string str)
    {
      throw new System.NotImplementedException();
    }

    public void Prompt()
    {
      throw new System.NotImplementedException();
    }

    public string Read()
    {
      throw new System.NotImplementedException();
    }

    public void Write(string str)
    {
      throw new System.NotImplementedException();
    }
  }
}
