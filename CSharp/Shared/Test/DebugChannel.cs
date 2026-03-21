using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class DebugChannelTest : UTestPack
  {
    public static DebugChannel<string> Cringe = new()
    {
      Open = true,
      Name = "Cringe",
      ToText = (s) => $"user says: {s}",
    };

    public override void CreateTests()
    {
      DebugHub.OnMsg.Add((channel) =>
      {
        Logger.Default.Log($"{channel.Name} >> {channel.Msg}");
      });

      Cringe.Send("hi");

      base.CreateTests();
    }
  }
}