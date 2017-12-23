using Newtonsoft.Json;

namespace Pisces.GameEngine.GameContext.Objects
{
  internal class Exit
  {
    [JsonProperty()]
    internal string Description { get; set; }

    [JsonProperty()]
    internal string RoomId { get; set; }

    [JsonProperty()]
    internal string Lock { get; set; }

    [JsonProperty()]
    internal string Success { get; set; }

    [JsonProperty()]
    internal string Fail { get; set; }
  }
}