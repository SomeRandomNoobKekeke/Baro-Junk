using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ConfiglikeObjectTest : ConfiglikeTest
  {
    public class ConfiglikeObjectGarbageInputTest : ConfiglikeObjectTest
    {
      public override void CreateTests()
      {

      }
    }

    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      IConfiglike configlikea = new ConfiglikeObject(config);
      IConfiglike configlikeb = configlikea.GetPropAsConfig("NestedConfigB");
      IConfiglike configlikec = configlikeb.GetPropAsConfig("NestedConfigC");

      Tests.Add(new UTest(configlikea.Target, config));
      Tests.Add(new UTest(configlikeb.Target, config.NestedConfigB));
      Tests.Add(new UTest(configlikec.Target, config.NestedConfigB.NestedConfigC));
    }
  }
}