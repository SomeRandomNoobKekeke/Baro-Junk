using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ConfigModelTest : UTestPack
  {
    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      string lastKey = "";
      object lastValue = null;

      ConfigModel model = new ConfigModel(config);
      model.OnPropChanged((key, value) =>
      {
        lastKey = key;
        lastValue = value;
      });

      model.Get("IntProp").Value = 123;
      Tests.Add(new UTest(config.IntProp, 123));
      Tests.Add(new UTest(lastKey, "IntProp"));
      Tests.Add(new UTest(lastValue, 123));

      model.Get("NestedConfigB.IntProp").Value = 321;
      Tests.Add(new UTest(config.NestedConfigB.IntProp, 321));
      Tests.Add(new UTest(lastKey, "NestedConfigB.IntProp"));
      Tests.Add(new UTest(lastValue, 321));

      model.Get("NestedConfigB").Get("NestedConfigC").Get("FloatProp").Value = 123.0f;
      Tests.Add(new UTest(config.NestedConfigB.NestedConfigC.FloatProp, 123.0f));
      Tests.Add(new UTest(lastKey, "NestedConfigB.NestedConfigC.FloatProp"));
      Tests.Add(new UTest(lastValue, 123.0f));
    }

  }
}