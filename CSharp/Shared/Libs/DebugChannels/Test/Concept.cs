using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class DebugNodeConceptTest : DebugNodeTest
  {
    public DebugNode<string> Node1 { get; } = new();
    public DebugNode<string> Node2 { get; } = new();
    public DebugNode<string, object> Node3 { get; } = new()
    {
      DebugMessageFactory = (s, o) => $"{s} {o} kek",
    };

    public UTest ConceptTest()
    {
      string result = "";

      Node2.Route(Node1);
      Node2.Map(Node3, (string s) => Node3.Send(s, 123));

      Node3.Pin.Add((e) => result = e.ToString());

      Node1.Send("bruh");

      return new UTest(result, "bruh 123 kek");
    }
  }
}