using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ReactiveEntryTest : ConfigTest
  {
    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();
      ReactiveCore core = new ReactiveCore(new ConfiglikeObject(config));

      string lastKey = null;
      object lastValue = null;

      core.PropChanged += (key, value) => (lastKey, lastValue) = (key, value);

      ReactiveEntry entry1 = core.Locator.GetEntry("IntProp");
      ReactiveEntry entry2 = core.Locator.GetEntry("NestedConfigB.IntProp");
      ReactiveEntry entry3 = core.Locator.GetEntry("NestedConfigB.NestedNullConfigC");
      ReactiveEntry entry4 = core.Locator.GetEntry("NestedConfigB").Locator.GetEntry("FloatProp");

      Mod.Logger.Log(entry4.DebugLog);

      Tests.Add(new UTest(entry1.Value, 2));
      Tests.Add(new UTest(entry2.Value, 4));
      Tests.Add(new UTest(entry3.Value, null));
      Tests.Add(new UTest(entry4.Value, 5.0f));

      entry1.Value = 3;
      Tests.Add(new UTest(lastKey, "IntProp"));
      Tests.Add(new UTest(lastValue, 3));
      Tests.Add(new UTest(entry1.Value, 3));
      Tests.Add(new UTest(config.IntProp, 3));

      entry2.Value = 5;
      Tests.Add(new UTest(lastKey, "NestedConfigB.IntProp"));
      Tests.Add(new UTest(lastValue, 5));
      Tests.Add(new UTest(entry2.Value, 5));
      Tests.Add(new UTest(config.NestedConfigB.IntProp, 5));

      entry3.Value = new ExampleConfigs.ConfigC();
      Tests.Add(new UTest(lastKey, "NestedConfigB.NestedNullConfigC"));
      Tests.Add(new UTest(entry3.Value is not null, true));
      Tests.Add(new UTest(config.NestedConfigB.NestedNullConfigC is not null, true));

      entry4.Value = 6.0f;
      Tests.Add(new UTest(lastKey, "NestedConfigB.FloatProp"));
      Tests.Add(new UTest(lastValue, 6.0f));
      Tests.Add(new UTest(entry4.Value, 6.0f));
      Tests.Add(new UTest(config.NestedConfigB.FloatProp, 6.0f));
    }
  }
}