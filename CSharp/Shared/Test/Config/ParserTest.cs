using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace BaroJunk
{
  public class ParserTest : ConfigTest
  {
    public class SomeClass
    {
      public string Value;

      public static SomeClass Parse(string raw) => new SomeClass()
      {
        Value = raw,
      };

      public override bool Equals(object obj)
      {
        if (obj is not SomeClass other) return false;
        return Value == other.Value;
      }
    }

    public override void CreateTests()
    {
      Tests.Add(new UTest(Parser.Parse<object>(null), SimpleResult.Success(null)));
      Tests.Add(new UTest(Parser.Parse<int>("123"), SimpleResult.Success(123)));
      Tests.Add(new UTest(Parser.Parse<float>("123"), SimpleResult.Success(123.0f)));
      Tests.Add(new UTest(Parser.Parse<string>("123"), SimpleResult.Success("123")));
      Tests.Add(new UTest(Parser.Parse<Vector2>("[34,312]"), SimpleResult.Success(new Vector2(34, 312))));
      Tests.Add(new UTest(Parser.Parse<SomeClass>("qwer"), SimpleResult.Success(new SomeClass() { Value = "qwer" })));
    }

  }
}