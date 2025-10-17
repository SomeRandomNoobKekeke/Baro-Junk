using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class IConfigLikeTest : ConfigTest
  {
    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      IConfiglike configlikea = new ConfiglikeObject(config);
      IConfiglike configlikeb = configlikea.GetConfig("NestedConfigB");
      IConfiglike configlikec = configlikeb.GetConfig("NestedConfigC");

      Tests.Add(new UTest(configlikea.Target, config));
      Tests.Add(new UTest(configlikeb.Target, config.NestedConfigB));
      Tests.Add(new UTest(configlikec.Target, config.NestedConfigB.NestedConfigC));
    }
  }
}