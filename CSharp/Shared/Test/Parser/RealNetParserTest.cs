using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Barotrauma.Networking;
namespace BaroJunk
{
  /// <summary>
  /// this is NetParserTest but in real multipler on real net messages
  /// </summary>
  public class RealNetParserTest : ParserTest
  {
    public class SomeClass
    {
      public string Value;

      public static SomeClass Parse(string raw) => new SomeClass()
      {
        Value = raw,
      };

      public void NetEncode(IWriteMessage msg)
      {
        NetParser.EncodeTable[typeof(string)].Invoke(msg, Value);
      }

      public static SomeClass NetDecode(IReadMessage msg)
      {
        return new SomeClass()
        {
          Value = (string)NetParser.DecodeTable[typeof(string)].Invoke(msg)
        };
      }

      public override bool Equals(object obj)
      {
        if (obj is not SomeClass other) return false;
        return Value == other.Value;
      }

      public override string ToString() => $"[{Value}]";
    }

    public List<object> input = new List<object>()
    {
      123, "bebebe", 3.0f, false, "bruh", new SomeClass(){ Value = "juju" },
    };


#if CLIENT
    public void InitProjSpecific()
    {
      NetParser parser = new NetParser();

      GameMain.LuaCs.Networking.Send(GameMain.LuaCs.Networking.Start("RealNetParserTest_start"));

      GameMain.LuaCs.Networking.Receive("RealNetParserTest_response", (object[] args) =>
      {
        IReadMessage msg = args[0] as IReadMessage;

        List<object> output = new List<object>();
        foreach (Type T in input.Select(o => o.GetType()))
        {
          output.Add(parser.Decode(msg, T).Result);
        }

        UListTest result = new UListTest(output, input);
        //FEATURE UTestPack is not flexible enough to do async tests, yet
        result.Log();
      });
    }
#endif

#if SERVER
    public void InitProjSpecific()
    {
      NetParser parser = new NetParser();

      GameMain.LuaCs.Networking.Receive("RealNetParserTest_start", (object[] args) =>
      {
        IReadMessage msg = args[0] as IReadMessage;
        Client client = args[1] as Client;

        IWriteMessage outMsg = GameMain.LuaCs.Networking.Start("RealNetParserTest_response");

        foreach (object o in input)
        {
          parser.Encode(outMsg, o);
        }

        GameMain.LuaCs.Networking.Send(outMsg, client.Connection);
      });
    }
#endif

    public override void CreateTests()
    {
      Tests.Add(new UTest(GameMain.IsMultiplayer, true));
      if (Tests.Last().Passed)
      {
        InitProjSpecific();
      }

    }

  }
}