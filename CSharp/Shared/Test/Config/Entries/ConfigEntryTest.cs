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
    public UTest EmptyTest => new UTest(ConfigEntry.Empty, new ConfigEntry(null, null));

    /// <summary>
    /// Locator should point to the value, not the config
    /// </summary>
    public List<UTest> LocatorTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new List<UTest>()
      {
        new UTest(
          new ConfigEntry(new ConfiglikeObject(config), "Bruh").Locator,
          new DirectEntryLocator(new ConfiglikeObject(null))
        ),

        new UTest(
          new ConfigEntry(new ConfiglikeObject(config), "IntProp").Locator,
          new DirectEntryLocator(new ConfiglikeObject(config.IntProp))
        ),

        new UTest(
          new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB").Locator,
          new DirectEntryLocator(new ConfiglikeObject(config.NestedConfigB))
        ),

        new UTest(
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedConfigC").Locator,
          new DirectEntryLocator(new ConfiglikeObject(config.NestedConfigB.NestedConfigC))
        ),
      };
    }

    public List<UTest> HostTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new List<UTest>()
      {
        new UTest(
          new ConfigEntry(null, "bruh").Host,
          null
        ),

        new UTest(
          new ConfigEntry(new ConfiglikeObject(config), "IntProp").Host,
          new ConfiglikeObject(config)
        ),

        new UTest(
          new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB").Host,
          new ConfiglikeObject(config)
        ),

        new UTest(
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedConfigC").Host,
          new ConfiglikeObject(config.NestedConfigB)
        ),
      };
    }

    public List<UTest> IsConfigTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new List<UTest>()
      {
        new UTest(new ConfigEntry(null, null).IsConfig, false),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "bruh").IsConfig, false),
        new UTest(new ConfigEntry(null, "bruh").IsConfig, false),
        new UTest(ConfigEntry.Empty.IsConfig, false),

        new UTest(new ConfigEntry(new ConfiglikeObject(config), "IntProp").IsConfig, false),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB").IsConfig, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NestedNullConfigB").IsConfig, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NullStringProp").IsConfig, false),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp").IsConfig, false),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedConfigC").IsConfig, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedNullConfigC").IsConfig, true),
      };
    }



    // public bool IsValid => Host is not null && Host.HasProp(Key);
    // public Type Type => Host?.TypeOfProp(Key);
    // public object Value
    // {
    //   get => Host?.GetValue(Key);
    //   set => Host?.SetValue(Key, value);
    // }
    // public bool SetValue(object value)
    // {
    //   if (Host is null) return false;
    //   return Host.SetValue(Key, value);
    // }

    // public ConfigEntry(IConfiglike host, string key)
    // {
    //   Host = host;
    //   Key = key ?? "";

    //   Locator = new DirectEntryLocator(new ConfigEntryLocatorAdapter(this));
    // }

    // public override bool Equals(object obj)
    // {
    //   if (obj is not ConfigEntry other) return false;
    //   if (Host is null && other.Host is null) return true;
    //   if (Host is null || other.Host is null) return false;

    //   return Object.Equals(Host.Target, other.Host.Target) && Key == other.Key;
    // }

    // public override string ToString() => $"[{(IsValid ? "" : "!")}{Host?.Target?.GetType().Name}.{Key} ({Value})]";
    // public string DebugLog => $"ConfigEntry [{GetHashCode()}] Host: [{Host}] Locator: [{Locator}]";














    // public class ConfigEntryLocatorTest : ConfigEntryTest
    // {
    //   public override void CreateTests()
    //   {
    //     ExampleConfigs.ConfigA config = new();

    //     ConfigEntry configb = new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB");
    //     ConfigEntry configc = configb.Locator.GetEntry("NestedConfigC");
    //     ConfigEntry deepIntProp = configc.Locator.GetEntry("IntProp");
    //     Tests.Add(new UTest(configb.Locator.Host.Target, config.NestedConfigB));
    //     Tests.Add(new UTest(configc.Locator.Host.Target, config.NestedConfigB.NestedConfigC));
    //   }
    // }

    // public class ConfigEntryGarbageInputTest : ConfigEntryTest
    // {
    //   public override void CreateTests()
    //   {
    //     ExampleConfigs.ConfigA config = new();
    //     IConfiglike configlike = new ConfiglikeObject(config);

    //     Tests.Add(new UTest(new ConfigEntry(null, null), ConfigEntry.Empty));
    //     Tests.Add(new UTest(new ConfigEntry(configlike, null).IsValid, false));
    //     Tests.Add(new UTest(new ConfigEntry(configlike, "Bruh").IsValid, false));
    //     Tests.Add(new UTest(new ConfigEntry(null, "Bruh"), ConfigEntry.Empty));
    //   }
    // }

    // public override void CreateTests()
    // {
    //   ExampleConfigs.ConfigA config = new();

    //   ConfigEntry entry1 = new ConfigEntry(new ConfiglikeObject(config), "IntProp");
    //   ConfigEntry entry2 = new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp");
    //   ConfigEntry entry3 = new ConfigEntry(new ConfiglikeObject(config.NestedConfigB.NestedConfigC), "IntProp");
    //   ConfigEntry entry4 = new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedNullConfigC");
    //   ConfigEntry entry5 = new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB").Locator.GetEntry("FloatProp");

    //   Tests.Add(new UTest(entry1.Value, 2, "Direct prop"));
    //   Tests.Add(new UTest(entry2.Value, 4, "Nested prop"));
    //   Tests.Add(new UTest(entry3.Value, 6, "Deeply nested prop"));
    //   Tests.Add(new UTest(entry4.Value, null, "nested object prop"));
    //   Tests.Add(new UTest(entry5.Value, 5.0f, "entry made from another entry"));
    //   entry1.Value = 3;
    //   entry2.Value = 5;
    //   entry3.Value = 7;
    //   entry4.Value = new ExampleConfigs.ConfigC();
    //   entry5.Value = 6.0f;
    //   Tests.Add(new UTest(entry1.Value, 3));
    //   Tests.Add(new UTest(entry2.Value, 5));
    //   Tests.Add(new UTest(entry3.Value, 7));
    //   Tests.Add(new UTest(entry4.Value is not null, true));
    //   Tests.Add(new UTest(entry5.Value, 6.0f));

    //   Tests.Add(new UTest(config.IntProp, 3));
    //   Tests.Add(new UTest(config.NestedConfigB.IntProp, 5));
    //   Tests.Add(new UTest(config.NestedConfigB.NestedConfigC.IntProp, 7));
    //   Tests.Add(new UTest(config.NestedConfigB.NestedNullConfigC is not null, true));
    //   Tests.Add(new UTest(config.NestedConfigB.FloatProp, 6.0f));
    // }



  }
}