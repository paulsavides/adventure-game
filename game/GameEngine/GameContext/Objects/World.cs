using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pisces.GameEngine.GameContext.Objects
{
  internal class World
  {
    [JsonProperty()]
    internal List<string> Intro { get; set; }

    [JsonProperty()]
    internal Dictionary<string, string> Defaults { get; set; }

    [JsonProperty()]
    internal Dictionary<string, Room> Rooms { get; set; }

    [JsonProperty()]
    internal Dictionary<string, Item> Items { get; set; }
  }
}
