namespace Pisces.GameEngine.GameContext
{
  public static class GameContextFactory
  {
    private static IGameContext _gc;
    public static IGameContext CreateContext(string gameFilePath)
    {
      if (_gc == null)
      {
        _gc = new Context(gameFilePath);
      }

      return _gc;
    }
  }
}
