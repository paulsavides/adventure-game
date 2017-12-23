using Pisces.GameEngine.GameContext.Objects;
using Newtonsoft.Json;
using System.IO;
using System;
using Pisces.GameEngine.GameContext.IO;
using Pisces.GameEngine.GameContext.Constants;
using System.Collections.Generic;
using Pisces.GameEngine.GameContext.Utilities;
using System.Threading;

namespace Pisces.GameEngine.GameContext
{
  internal class Context : IGameContext
  {
    private World _gameWorld;
    private GameState _gameState;

    private IGameIO _gameIO;

    private string _error;
    private bool _isError;

    private List<string> _tokens;


    internal Context(string gameFilePath)
    {
      Init(gameFilePath, null, null); // default new game
    }

    internal void Init(string gameFilePath, string gameStateFilePath, IGameIO gameIO)
    {
      try
      {
        _gameWorld = JsonConvert.DeserializeObject<World>(File.ReadAllText(gameFilePath));
        if (string.IsNullOrEmpty(gameStateFilePath))
        {
          _gameState = new GameState
          {
            CurrentRoom = _gameWorld.Defaults[StateKeys.EntryRoom],
            PastIntro = false,
            Inventory = new HashSet<string>(),
            UnlockedExits = new Dictionary<string, Dictionary<string, bool>>()
          };
        }

        if (gameIO == null)
        {
          _gameIO = new ConsoleGameIO();
        }

      }
      catch (Exception e)
      {
        //TODO: LOG
      }
    }

    // Core Game Loop
    public void RunGameLoop()
    {
      if (!SanityCheck())
      {
        return;
      }

      if (!_gameState.PastIntro)
      {
        RunIntro();
        _gameState.PastIntro = true;
      }

      DescribeCurrentState();
      _gameIO.Prompt();

      while (ReadNext())
      {
        ProcessTokens();
        //DescribeCurrentState();
        _gameIO.Prompt();
      }
    }

    private void ProcessTokens()
    {
      if (_tokens == null)
      {
        DescribeCurrentState();
        return;
      }

      foreach (var token in _tokens)
      {
        switch (token)
        {
          case ReservedWords.Look:
            ProcessLook();
            return;
          case ReservedWords.Go:
            ProcessGo();
            return;
          case ReservedWords.Search:
            ProcessSearch();
            return;
          default:
            break;
        }
      }

      DescribeCurrentState();
    }

    private void ProcessSearch()
    {
      var room = CurrentRoom();

      if (room.Items == null)
      {
        DescribeCurrentState();
        return;
      }

      foreach (var item in room.Items)
      {
        foreach (var term in item.Value.LookTerms)
        {
          if (_tokens.Contains(term))
          {
            if (InInventory(item.Key))
            {
              _gameIO.Write(_gameWorld.Items[item.Key].Description);
              return;
            }

            AddToInventory(item.Key);
            _gameIO.Write(_gameWorld.Items[item.Key].Found);
            return;
          }
        }
      }

      DescribeCurrentState();
    }

    private bool InInventory(string itemId)
    {
      if (_gameState.Inventory == null)
      {
        _gameState.Inventory = new HashSet<string>();
      }

      return _gameState.Inventory.Contains(itemId);
    }

    private void AddToInventory(string itemId)
    {
      if (_gameState.Inventory == null)
      {
        _gameState.Inventory = new HashSet<string>();
      }

      if (!InInventory(itemId))
      {
        _gameState.Inventory.Add(itemId);
      }
    }

    private void ProcessGo()
    {
      var room = CurrentRoom();

      if (room.Exits != null)
      {
        foreach (var exit in room.Exits)
        {
          if (_tokens.Contains(exit.Key))
          {
            if (CanExit(room, exit.Value))
            {
              if (exit.Value.Lock != null && !HasExited(_gameState.CurrentRoom, exit.Key))
              {
                UnlockExit(_gameState.CurrentRoom, exit.Key, exit.Value);
              }

              _gameState.CurrentRoom = exit.Value.RoomId;
              _gameIO.Write(CurrentRoom().Description);
            }
            else
            {
              _gameIO.Write(exit.Value.Fail);
            }
          }
        }
      }
    }

    private void UnlockExit(string roomId, string exitName, Exit exit)
    {
      if (!_gameState.UnlockedExits.ContainsKey(roomId))
      {
        _gameState.UnlockedExits.Add(roomId, new Dictionary<string, bool>());
      }

      _gameState.UnlockedExits[roomId].Add(exitName, true);
      _gameIO.Write(exit.Success);
      _gameIO.Write("");
    }

    private bool HasExited(string roomId, string exitName)
    {
      if (!_gameState.UnlockedExits.ContainsKey(roomId))
      {
        return false;
      }

      if (_gameState.UnlockedExits[roomId].ContainsKey(exitName))
      {
        return true;
      }

      return false;
    }

    private bool CanExit(Room room, Exit exit)
    {
      if (string.IsNullOrEmpty(exit.Lock))
      {
        return true;
      }

      if (_gameState.Inventory.Contains(exit.Lock))
      {
        return true;
      }

      return false;
    }


    private void ProcessLook()
    {
      // precedence, will look items first then exits second
      var room = CurrentRoom();

      if (room.Items != null)
      {
        foreach (var item in room.Items)
        {
          foreach (var lookTerm in item.Value.LookTerms)
          {
            if (_tokens.Contains(lookTerm))
            {
              _gameIO.Write(_gameWorld.Items[item.Key].Description);
              return;
            }
          }
        }
      }

      if (room.Exits != null)
      {
        foreach (var exit in room.Exits)
        {
          if (_tokens.Contains(exit.Key))
          {
            _gameIO.Write(exit.Value.Description);
            return;
          }
        }
      }


      _gameIO.Write(room.Description);

    }

    private Room CurrentRoom()
    {
      if (_gameState.CurrentRoom == null || !_gameWorld.Rooms.ContainsKey(_gameState.CurrentRoom))
      {
        _gameState.CurrentRoom = _gameWorld.Defaults[StateKeys.EntryRoom];
      }

      return _gameWorld.Rooms[_gameState.CurrentRoom];
    }

    private void DescribeCurrentState()
    {
      if (_gameState.CurrentRoom == null || !_gameWorld.Rooms.ContainsKey(_gameState.CurrentRoom))
      {
        _gameState.CurrentRoom = _gameWorld.Defaults[StateKeys.EntryRoom];
      }

      _gameIO.Write(CurrentRoom().Description);
    }

    private bool ReadNext()
    {
      var str = _gameIO.Read();
      if (string.IsNullOrEmpty(str))
      {
        _tokens = null;
        return true; // it's probably alright
      }

      _tokens = Parser.Tokenize(str);
      if (_tokens.Count > 0 && _tokens[0] == ReservedWords.Quit)
      {
        _gameIO.Write("see ya");
        return false;
      }
      return true;
    }

    private void RunIntro()
    {
      foreach (string str in _gameWorld.Intro)
      {
        _gameIO.Write(str);
      }
    }

    private bool SanityCheck()
    {
      if (_gameWorld == null || _gameState == null || _gameIO == null)
      {
        Error("Game world not propertly initialized.");
        return false;
      }
      return true;
    }

    private void Error(string error)
    {
      if (_gameIO == null)
      {
        _gameIO = new ConsoleGameIO();
      }
      _gameIO.Error(error);
    }
  }
}
