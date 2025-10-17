using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ConfigEntryTest : ConfigTest
  {
    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      ConfigEntry entry1 = new ConfigEntry(new ConfiglikeObject(config), "IntProp");
      ConfigEntry entry2 = new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp");

      Tests.Add(new UTest(entry1.Value, 2));
      Tests.Add(new UTest(entry2.Value, 4));
      entry1.Value = 3;
      entry2.Value = 5;
      Tests.Add(new UTest(entry1.Value, 3));
      Tests.Add(new UTest(entry2.Value, 5));
      Tests.Add(new UTest(config.IntProp, 3));
      Tests.Add(new UTest(config.NestedConfigB.IntProp, 5));
    }
  }
}