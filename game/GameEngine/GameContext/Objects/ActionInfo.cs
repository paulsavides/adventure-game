using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pisces.GameEngine.GameContext.Objects
{
  internal class ActionInfo
  {
    [JsonProperty()]
    internal List<string> LookTerms { get; set; }

    [JsonProperty()]
    internal List<string> PickupTerms { get; set; }
  }
}