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
  public class NetParserTest : ParserTest
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

    public override void CreateTests()
    {
      NetParser parser = new NetParser();
      FakeReadWriteMessage msg = new FakeReadWriteMessage();

      List<object> input = new List<object>()
      {
        123, "bebebe", 3.0f, false, "bruh", new SomeClass(){ Value = "juju" },
      };

      foreach (object o in input)
      {
        parser.Encode(msg, o);
      }

      List<object> output = new List<object>();
      foreach (Type T in input.Select(o => o.GetType()))
      {
        output.Add(parser.Decode(msg, T).Result);
      }

      Tests.Add(new UListTest(output, input));
    }

  }
}