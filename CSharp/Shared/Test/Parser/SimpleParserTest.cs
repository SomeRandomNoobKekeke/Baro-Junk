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
  //TODO finish
  public class SimpleParserTest : ParserTest
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
      SimpleParser parser = new SimpleParser();

      Tests.Add(new UTest(parser.Parse(null, typeof(object)), SimpleResult.Success(null)));
      Tests.Add(new UTest(parser.Parse("123", typeof(int)), SimpleResult.Success(123)));
      Tests.Add(new UTest(parser.Parse("123", typeof(float)), SimpleResult.Success(123.0f)));
      Tests.Add(new UTest(parser.Parse("123", typeof(string)), SimpleResult.Success("123")));
      Tests.Add(new UTest(parser.Parse("[34,312]", typeof(Vector2)), SimpleResult.Success(new Vector2(34, 312))));
      Tests.Add(new UTest(parser.Parse("qwer", typeof(SomeClass)), SimpleResult.Success(new SomeClass() { Value = "qwer" })));
    }

  }
}