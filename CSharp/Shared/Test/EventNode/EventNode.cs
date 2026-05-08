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
    public override void CreateTests()
    {
      EventNode<string, object> node1 = new();
      EventNode<string, object> node2 = new();

      node2.AddGate<string>();

      node1.Map(node2);
    }


  }
}