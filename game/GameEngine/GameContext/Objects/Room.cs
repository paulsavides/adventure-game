using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pisces.GameEngine.GameContext.Objects
{
  internal class Room
  {
    [JsonProperty()]
    internal string Title { get; set; }

    [JsonProperty()]
    internal string Description { get; set; }

    [JsonProperty()]
    internal Dictionary<string, Exit> Exits { get; set; }

    [JsonProperty()]
    internal Dictionary<string, ActionInfo> Items {get;set;}
  }
}
