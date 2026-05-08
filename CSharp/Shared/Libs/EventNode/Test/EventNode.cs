using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class EventNodeTest : UTestPack
  {
    public object Bruh { get; } = new();

    public EventNode<string> node1 { get; } = new();
    public EventNode<string, object> node2 { get; } = new()
    {

    };



    public override void CreateTests()
    {
      node2.Route(node1, (string s) => node2.Event.Raise(s, 123));




      Mod.Logger.Log(123);
    }


  }
}