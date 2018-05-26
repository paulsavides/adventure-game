namespace Pisces.GameEngine.GameContext
{
  // ReSharper disable once InconsistentNaming
  public interface IGameIO
  {
    void Prompt();
    void Write(string str);
    void Error(string str);
    string Read();
  }
}
