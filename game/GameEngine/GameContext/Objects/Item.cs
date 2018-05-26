using Newtonsoft.Json;

namespace Pisces.GameEngine.GameContext.Objects
{
  internal class Item
  {
    [JsonProperty]
    internal string Description { get; set; }

    [JsonProperty]
    internal string Found { get; set; }
  }
}