using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class DirectEntryLocatorTest : ConfigTest
  {
    public List<UTest> GetEntryTest()
    {
      ExampleConfigs.ConfigA config = new();
      DirectEntryLocator locator = new DirectEntryLocator(new ConfiglikeObject(config));
      DirectEntryLocator borked = new DirectEntryLocator(null as IConfiglike);


      return new List<UTest>()
      {
        new UTest(
          borked.GetEntry("Bruh"),
          ConfigEntry.Empty
        ),
        new UTest(
          locator.GetEntry("IntProp"),
          new ConfigEntry(new ConfiglikeObject(config),"IntProp")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB),"IntProp")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.NestedConfigC.IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB.NestedConfigC),"IntProp")
        ),
        new UTest(
          locator.GetEntry(""),
          new ConfigEntry(new ConfiglikeObject(config),"")
        ),
        new UTest(
          locator.GetEntry(null),
          new ConfigEntry(new ConfiglikeObject(config), null)
        ),
        new UTest(
          locator.GetEntry("Bruh"),
          new ConfigEntry(new ConfiglikeObject(config), "Bruh")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.Bruh"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "Bruh")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB..Bruh"),
          ConfigEntry.Empty
        ),
        new UTest(
          locator.GetEntry("...Bruh"),
          ConfigEntry.Empty
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.    .Bruh"),
          ConfigEntry.Empty
        ),
        new UTest(
          locator.GetEntry("    .     .     .       "),
          ConfigEntry.Empty
        ),
      };
    }


    public List<UTest> GetValueTest()
    {
      ExampleConfigs.ConfigA config = new();
      DirectEntryLocator locator = new DirectEntryLocator(new ConfiglikeObject(config));

      return new List<UTest>()
      {
        new UTest(locator.GetValue("IntProp"), 2),
        new UTest(locator.GetValue("NestedConfigB"), config.NestedConfigB),
        new UTest(locator.GetValue("Bruh"), null),
        new UTest(locator.GetValue(null), null),
        new UTest(locator.GetValue(""), null),
      };
    }

    public List<UTest> SetValueTest()
    {
      ExampleConfigs.ConfigA config = new();
      DirectEntryLocator locator = new DirectEntryLocator(new ConfiglikeObject(config));

      // SetValue also returns success status
      return new List<UTest>()
      {
        new UTest(locator.SetValue("IntProp", 3), true),
        new UTest(locator.SetValue("NestedNullConfigB", new ExampleConfigs.ConfigB()), true),
        new UTest(locator.SetValue("NestedConfigB.IntProp", 5), true),

        new UTest(config.IntProp, 3),
        new UTest(config.NestedNullConfigB is not null, true),
        new UTest(config.NestedConfigB.IntProp, 5),

        new UTest(locator.SetValue("Bruh", 123), false),
        new UTest(locator.SetValue("", 123), false),
        new UTest(locator.SetValue(null, 123), false),
        new UTest(locator.SetValue("FloatProp", "100px"), false),
      };
    }

    public UListTest GetEntriesTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new UListTest(
        configlike.Locator.GetEntries(),
        new List<ConfigEntry>()
        {
          new ConfigEntry(configlike,"IntProp"),
          new ConfigEntry(configlike,"FloatProp"),
          new ConfigEntry(configlike,"StringProp"),
          new ConfigEntry(configlike,"NullStringProp"),
          new ConfigEntry(configlike,"ShouldNotBeDugInto"),
        }
      );
    }

    public UListTest GetAllEntriesTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new UListTest(
        configlike.Locator.GetAllEntries(),
        new List<ConfigEntry>()
        {
          new ConfigEntry(configlike,"IntProp"),
          new ConfigEntry(configlike,"FloatProp"),
          new ConfigEntry(configlike,"StringProp"),
          new ConfigEntry(configlike,"NullStringProp"),
          new ConfigEntry(configlike,"ShouldNotBeDugInto"),
          new ConfigEntry(configlike,"NestedConfigB"),
          new ConfigEntry(configlike,"NestedNullConfigB"),
          new ConfigEntry(configlike,"EmptyConfig"),
        }
      );
    }

    public UListTest GetEntriesRecTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlikea = new ConfiglikeObject(config);
      IConfiglike configlikeb = new ConfiglikeObject(config.NestedConfigB);
      IConfiglike configlikec = new ConfiglikeObject(config.NestedConfigB.NestedConfigC);

      return new UListTest(
        configlikea.Locator.GetEntriesRec(),
        new List<ConfigEntry>()
        {
          new ConfigEntry(configlikea,"IntProp"),
          new ConfigEntry(configlikea,"FloatProp"),
          new ConfigEntry(configlikea,"StringProp"),
          new ConfigEntry(configlikea,"NullStringProp"),
          new ConfigEntry(configlikea,"ShouldNotBeDugInto"),

          new ConfigEntry(configlikeb,"IntProp"),
          new ConfigEntry(configlikeb,"FloatProp"),
          new ConfigEntry(configlikeb,"StringProp"),
          new ConfigEntry(configlikeb,"NullStringProp"),

          new ConfigEntry(configlikec,"IntProp"),
          new ConfigEntry(configlikec,"FloatProp"),
          new ConfigEntry(configlikec,"StringProp"),
          new ConfigEntry(configlikec,"NullStringProp"),
        }
      );
    }

    public UListTest GetAllEntriesRecTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlikea = new ConfiglikeObject(config);
      IConfiglike configlikeb = new ConfiglikeObject(config.NestedConfigB);
      IConfiglike configlikec = new ConfiglikeObject(config.NestedConfigB.NestedConfigC);

      return new UListTest(
        configlikea.Locator.GetAllEntriesRec(),
        new List<ConfigEntry>()
        {
          new ConfigEntry(configlikea,"IntProp"),
          new ConfigEntry(configlikea,"FloatProp"),
          new ConfigEntry(configlikea,"StringProp"),
          new ConfigEntry(configlikea,"NullStringProp"),
          new ConfigEntry(configlikea,"ShouldNotBeDugInto"),
          new ConfigEntry(configlikea,"NestedConfigB"),
          new ConfigEntry(configlikea,"NestedNullConfigB"),
          new ConfigEntry(configlikea,"EmptyConfig"),

          new ConfigEntry(configlikeb,"IntProp"),
          new ConfigEntry(configlikeb,"FloatProp"),
          new ConfigEntry(configlikeb,"StringProp"),
          new ConfigEntry(configlikeb,"NullStringProp"),
          new ConfigEntry(configlikeb,"NestedConfigC"),
          new ConfigEntry(configlikeb,"NestedNullConfigC"),
          new ConfigEntry(configlikeb,"EmptyConfig"),

          new ConfigEntry(configlikec,"IntProp"),
          new ConfigEntry(configlikec,"FloatProp"),
          new ConfigEntry(configlikec,"StringProp"),
          new ConfigEntry(configlikec,"NullStringProp"),
        }
      );
    }

    public UDictTest GetFlatTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlikea = new ConfiglikeObject(config);
      IConfiglike configlikeb = new ConfiglikeObject(config.NestedConfigB);
      IConfiglike configlikec = new ConfiglikeObject(config.NestedConfigB.NestedConfigC);

      return new UDictTest(
        configlikea.Locator.GetFlat(),
        new Dictionary<string, ConfigEntry>()
        {
          ["IntProp"] = new ConfigEntry(configlikea, "IntProp"),
          ["FloatProp"] = new ConfigEntry(configlikea, "FloatProp"),
          ["StringProp"] = new ConfigEntry(configlikea, "StringProp"),
          ["NullStringProp"] = new ConfigEntry(configlikea, "NullStringProp"),
          ["ShouldNotBeDugInto"] = new ConfigEntry(configlikea, "ShouldNotBeDugInto"),
          ["NestedConfigB.IntProp"] = new ConfigEntry(configlikeb, "IntProp"),
          ["NestedConfigB.FloatProp"] = new ConfigEntry(configlikeb, "FloatProp"),
          ["NestedConfigB.StringProp"] = new ConfigEntry(configlikeb, "StringProp"),
          ["NestedConfigB.NullStringProp"] = new ConfigEntry(configlikeb, "NullStringProp"),
          ["NestedConfigB.NestedConfigC.IntProp"] = new ConfigEntry(configlikec, "IntProp"),
          ["NestedConfigB.NestedConfigC.FloatProp"] = new ConfigEntry(configlikec, "FloatProp"),
          ["NestedConfigB.NestedConfigC.StringProp"] = new ConfigEntry(configlikec, "StringProp"),
          ["NestedConfigB.NestedConfigC.NullStringProp"] = new ConfigEntry(configlikec, "NullStringProp"),
        }
      );
    }

    public UDictTest GetAllFlatTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlikea = new ConfiglikeObject(config);
      IConfiglike configlikeb = new ConfiglikeObject(config.NestedConfigB);
      IConfiglike configlikec = new ConfiglikeObject(config.NestedConfigB.NestedConfigC);

      return new UDictTest(
        configlikea.Locator.GetAllFlat(),
        new Dictionary<string, ConfigEntry>()
        {
          ["IntProp"] = new ConfigEntry(configlikea, "IntProp"),
          ["FloatProp"] = new ConfigEntry(configlikea, "FloatProp"),
          ["StringProp"] = new ConfigEntry(configlikea, "StringProp"),
          ["NullStringProp"] = new ConfigEntry(configlikea, "NullStringProp"),
          ["ShouldNotBeDugInto"] = new ConfigEntry(configlikea, "ShouldNotBeDugInto"),
          ["NestedConfigB"] = new ConfigEntry(configlikea, "NestedConfigB"),
          ["NestedNullConfigB"] = new ConfigEntry(configlikea, "NestedNullConfigB"),
          ["EmptyConfig"] = new ConfigEntry(configlikea, "EmptyConfig"),
          ["NestedConfigB.IntProp"] = new ConfigEntry(configlikeb, "IntProp"),
          ["NestedConfigB.FloatProp"] = new ConfigEntry(configlikeb, "FloatProp"),
          ["NestedConfigB.StringProp"] = new ConfigEntry(configlikeb, "StringProp"),
          ["NestedConfigB.NullStringProp"] = new ConfigEntry(configlikeb, "NullStringProp"),
          ["NestedConfigB.NestedConfigC"] = new ConfigEntry(configlikeb, "NestedConfigC"),
          ["NestedConfigB.NestedNullConfigC"] = new ConfigEntry(configlikeb, "NestedNullConfigC"),
          ["NestedConfigB.EmptyConfig"] = new ConfigEntry(configlikeb, "EmptyConfig"),
          ["NestedConfigB.NestedConfigC.IntProp"] = new ConfigEntry(configlikec, "IntProp"),
          ["NestedConfigB.NestedConfigC.FloatProp"] = new ConfigEntry(configlikec, "FloatProp"),
          ["NestedConfigB.NestedConfigC.StringProp"] = new ConfigEntry(configlikec, "StringProp"),
          ["NestedConfigB.NestedConfigC.NullStringProp"] = new ConfigEntry(configlikec, "NullStringProp"),
        }
      );
    }

    public UDictTest GetFlatValuesTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new UDictTest(
        configlike.Locator.GetFlatValues(),
        configlike.Locator.GetFlat().ToDictionary(
          kvp => kvp.Key,
          kvp => kvp.Value.Value
        )
      );
    }

    public UDictTest GetAllFlatValuesTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new UDictTest(
        configlike.Locator.GetAllFlatValues(),
        configlike.Locator.GetAllFlat().ToDictionary(
          kvp => kvp.Key,
          kvp => kvp.Value.Value
        )
      );
    }
  }
}