using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace BaroJunk
{
  public class DeepReactivityTest : ConfigTest
  {
    public UListTest CallOrderTest()
    {
      ExampleConfigs.ConfigA config = new();
      config.Settings().DeeplyReactive = true;

      List<string> WhatHappened = new();

      config.OnPropChanged((key, value)
        => WhatHappened.Add($"prop on configA changed [{key}] = [{value}]"));

      config.NestedConfigB.OnPropChanged((key, value)
        => WhatHappened.Add($"prop on configB changed [{key}] = [{value}]"));

      config.NestedConfigB.NestedConfigC.OnPropChanged((key, value)
         => WhatHappened.Add($"prop on configC changed [{key}] = [{value}]"));

      config.ReactiveSetValue("NestedConfigB.NestedConfigC.IntProp", 23);

      return new UListTest(
        WhatHappened, new List<string>(){
          "prop on configA changed [NestedConfigB.NestedConfigC.IntProp] = [23]",
          "prop on configB changed [NestedConfigC.IntProp] = [23]",
          "prop on configC changed [IntProp] = [23]",
        }
      );
    }

    public UTest DeepUpdateTest()
    {
      ExampleConfigs.ConfigA config = new();
      config.Settings().DeeplyReactive = true;

      List<string> WhatHappened = new();

      foreach (ConfigEntry entry in config.GetAllEntriesRec())
      {
        if (entry.IsConfig)
        {
          entry.ValueAsConfig.Core?.OnUpdated(() => WhatHappened.Add($"[{entry.Host.Name}.{entry.ValueAsConfig.Name}] updated"));
        }
      }

      config.GetReactiveCore().RaiseUpdated();

      return new UListTest(
        WhatHappened, new List<string>(){
          "[ConfigA.ConfigB] updated",
          "[ConfigA.EmptyConfig] updated",
          "[ConfigB.ConfigC] updated",
          "[ConfigB.EmptyConfig] updated"
        }
      );
    }


    public UListTest GarbageInputTest()
    {
      ExampleConfigs.ConfigA config = new();
      config.Settings().DeeplyReactive = true;

      List<string> WhatHappened = new();

      config.ReactiveSetValue("NestedConfigB.bruh.IntProp", 23);
      config.ReactiveSetValue("  .  .IntProp", 23);
      config.ReactiveSetValue("bruh", 23);

      return new UListTest(
        WhatHappened, new List<string>(){
          "nothing funny",
        }
      );
    }
  }
}