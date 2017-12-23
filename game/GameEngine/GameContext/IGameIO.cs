using System;
using System.Collections.Generic;
using System.Text;

namespace Pisces.GameEngine.GameContext
{
  public interface IGameIO
  {
    void Prompt();
    void Write(string str);
    void Error(string str);
    string Read();
  }
}
