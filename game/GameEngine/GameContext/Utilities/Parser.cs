using System.Collections.Generic;
using System.Linq;

namespace Pisces.GameEngine.GameContext.Utilities
{
  internal static class Parser
  {
    internal static List<string> Tokenize(string str)
    {
      List<int> toRemove = new List<int>();
      for (int i = 0; i < str.Length; i++)
      {
        if (ShouldRemove(str[i]))
        {
          toRemove.Add(i);
        }
      }

      for (int j = toRemove.Count - 1; j >= 0; j--)
      {
        str.Remove(j);
      }


      str = str.ToLower();
      return str.Split(' ').ToList();
    }

    private static bool ShouldRemove(char c)
    {
      return !IsWhiteSpace(c) && !IsLetter(c);
    }
    
    private static bool IsLetter(char c)
    {
      return char.IsLetter(c);
    }
    
    private static bool IsWhiteSpace(char c)
    {
      return char.IsWhiteSpace(c);
    }
  }
}
