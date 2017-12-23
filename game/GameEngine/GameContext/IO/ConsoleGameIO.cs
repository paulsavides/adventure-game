using System;
using System.Threading;

namespace Pisces.GameEngine.GameContext.IO
{
  internal class ConsoleGameIO : IGameIO
  {
    private long last;
    private long timeout = 200;

    public void Prompt()
    {
      Console.WriteLine();
      Console.Write("> ");
    }

    public void Error(string str)
    {
      Console.WriteLine(str);
    }

    public string Read()
    {
      return Console.ReadLine();
    }

    public void Write(string str)
    {
      Wait();
      Console.WriteLine(str);
      last = DateTime.Now.Ticks;
    }

    public void Wait()
    {
      long now = DateTime.Now.Ticks;
      long elapsed = now - last;

      if (elapsed < timeout)
      {
        Thread.Sleep((int)(timeout - elapsed));
      }
    }
  }
}
