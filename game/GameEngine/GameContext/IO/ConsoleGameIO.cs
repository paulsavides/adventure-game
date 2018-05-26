using System;
using System.Threading;

namespace Pisces.GameEngine.GameContext.IO
{
  // ReSharper disable once InconsistentNaming
  internal class ConsoleGameIO : IGameIO
  {
    private long _last;
    private const long Timeout = 200;

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
      _last = DateTime.Now.Ticks;
    }

    public void Wait()
    {
      long now = DateTime.Now.Ticks;
      long elapsed = now - _last;

      if (elapsed < Timeout)
      {
        Thread.Sleep((int)(Timeout - elapsed));
      }
    }
  }
}
