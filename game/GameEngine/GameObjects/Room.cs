using System.Collections.Generic;

namespace Pisces.GameEngine.GameObjects
{
  public class Room
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public Dictionary<string, Exit> Exits { get; set; }
    public Dictionary<string, ActionInfo> Items {get;set;}
  }
}
