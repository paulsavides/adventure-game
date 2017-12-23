using System.Collections.Generic;

namespace Pisces.GameEngine.GameObjects
{
  public class World
  {
    public List<string> Intro { get; set; }
    public Dictionary<string, string> Defaults { get; set; }
    public Dictionary<string, Room> Rooms { get; set; }
    public Dictionary<string, Item> Items { get; set; }
  }
}
