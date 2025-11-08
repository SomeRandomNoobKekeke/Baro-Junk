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

    public List<UTest> IsValidTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new List<UTest>()
      {
        new UTest(new ConfigEntry(null, null).IsValid, false),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "bruh").IsValid, false),
        new UTest(new ConfigEntry(null, "bruh").IsValid, false),
        new UTest(ConfigEntry.Empty.IsValid, false),

        new UTest(new ConfigEntry(new ConfiglikeObject(config), "Bruh").IsValid, false),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.IntProp), "IntProp").IsValid, false),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "IntProp").IsValid, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB").IsValid, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NestedNullConfigB").IsValid, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NullStringProp").IsValid, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp").IsValid, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedConfigC").IsValid, true),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedNullConfigC").IsValid, true),
      };
    }

    public List<UTest> TypeTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new List<UTest>()
      {
        new UTest(new ConfigEntry(new ConfiglikeObject(config), null).Type, null),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "IntProp").Type, typeof(int)),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NestedNullConfigB").Type, typeof(ExampleConfigs.ConfigB)),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "").Type, null),
      };
    }
    public List<UTest> GetValueTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new List<UTest>()
      {
        new UTest(new ConfigEntry(new ConfiglikeObject(config), null).Value, null),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "IntProp").Value, 2),
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "NestedNullConfigB").Value, config.NestedNullConfigB),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp").Value, 4),
      };
    }

    public List<UTest> SetValueTest()
    {
      ExampleConfigs.ConfigA config = new();

      new ConfigEntry(new ConfiglikeObject(config), null).Value = 123;
      new ConfigEntry(new ConfiglikeObject(config), "IntProp").Value = 3;
      new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp").Value = 5;

      return new List<UTest>()
      {
        new UTest(new ConfigEntry(new ConfiglikeObject(config), "IntProp").Value, 3),
        new UTest(new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp").Value, 5),
      };
    }

    public List<UTest> EqualsTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new List<UTest>()
      {
        new UTest(
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp")
        ),
        new UTest(
          new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB").GetEntry("IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp")
        ),
        new UTest(
          new ConfiglikeObject(config).GetEntry("NestedConfigB.IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp")
        ),
      };
    }

  }
}