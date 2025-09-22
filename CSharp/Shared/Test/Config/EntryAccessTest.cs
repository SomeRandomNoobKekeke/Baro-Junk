using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class EntryAccessTest : UTestPack
  {

    public class GetEntryTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UTest(EntryAccess.GetEntry(config, "IntProp"), new ConfigEntry(config, "IntProp")));
        Tests.Add(new UTest(EntryAccess.GetEntry(config, "NestedConfigB.IntProp"), new ConfigEntry(config.NestedConfigB, "IntProp")));
      }
    }

    public class GetSetPropTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UTest(EntryAccess.GetProp(config, "IntProp"), 2));
        Tests.Add(new UTest(EntryAccess.GetProp(config, "NestedConfigB.IntProp"), 4));
        Tests.Add(new UTest(EntryAccess.GetProp(EntryAccess.GetProp(config, "NestedConfigB"), "IntProp"), 4));

        Tests.Add(new UTest(EntryAccess.GetProp(config, "bebebe"), null));
        Tests.Add(new UTest(EntryAccess.GetProp(config, ""), null));
        Tests.Add(new UTest(EntryAccess.GetProp(config, null), null));
        Tests.Add(new UTest(EntryAccess.GetProp(config, ".Settings."), null));
        Tests.Add(new UTest(EntryAccess.GetProp(config, "NestedConfigB.qweqe"), null));
        Tests.Add(new UTest(EntryAccess.GetProp(config, ".NestedConfigB.IntProp"), null));

        EntryAccess.SetProp(config, "IntProp", 3);
        Tests.Add(new UTest(EntryAccess.GetProp(config, "IntProp"), 3));

        EntryAccess.SetProp(config, "NestedConfigB.IntProp", 5);
        Tests.Add(new UTest(EntryAccess.GetProp(config, "NestedConfigB.IntProp"), 5));
      }
    }

    public class GetEntriesTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UListTest(EntryAccess.GetEntries(config).ToList(), new List<ConfigEntry>()
        {
          new ConfigEntry(config,"IntProp"),
          new ConfigEntry(config,"FloatProp"),
          new ConfigEntry(config,"StringProp"),
          new ConfigEntry(config,"NullStringProp"),
          new ConfigEntry(config,"ShouldNotBeDugInto"),
        }));
      }
    }

    public class GetAllEntriesTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UListTest(EntryAccess.GetAllEntries(config).ToList(), new List<ConfigEntry>()
        {
          new ConfigEntry(config,"IntProp"),
          new ConfigEntry(config,"FloatProp"),
          new ConfigEntry(config,"StringProp"),
          new ConfigEntry(config,"NullStringProp"),
          new ConfigEntry(config,"ShouldNotBeDugInto"),
          new ConfigEntry(config,"NestedConfigB"),
          new ConfigEntry(config,"NestedNullConfigB"),
          new ConfigEntry(config,"EmptyConfig"),
        }));
      }
    }

    public class GetEntriesRecTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UListTest(EntryAccess.GetEntriesRec(config).ToList(), new List<ConfigEntry>()
        {
          new ConfigEntry(config, "IntProp"),
          new ConfigEntry(config, "FloatProp"),
          new ConfigEntry(config, "StringProp"),
          new ConfigEntry(config, "NullStringProp"),
          new ConfigEntry(config, "ShouldNotBeDugInto"),
          new ConfigEntry(config.NestedConfigB, "IntProp"),
          new ConfigEntry(config.NestedConfigB, "FloatProp"),
          new ConfigEntry(config.NestedConfigB, "StringProp"),
          new ConfigEntry(config.NestedConfigB, "NullStringProp"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "IntProp"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "FloatProp"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "StringProp"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "NullStringProp"),
        }));
      }
    }

    public class GetAllEntriesRecTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UListTest(EntryAccess.GetAllEntriesRec(config).ToList(), new List<ConfigEntry>()
        {
          new ConfigEntry(config, "IntProp"),
          new ConfigEntry(config, "FloatProp"),
          new ConfigEntry(config, "StringProp"),
          new ConfigEntry(config, "NullStringProp"),
          new ConfigEntry(config, "ShouldNotBeDugInto"),
          new ConfigEntry(config, "NestedConfigB"),
          new ConfigEntry(config, "NestedNullConfigB"),
          new ConfigEntry(config, "EmptyConfig"),
          new ConfigEntry(config.NestedConfigB, "IntProp"),
          new ConfigEntry(config.NestedConfigB, "FloatProp"),
          new ConfigEntry(config.NestedConfigB, "StringProp"),
          new ConfigEntry(config.NestedConfigB, "NullStringProp"),
          new ConfigEntry(config.NestedConfigB, "NestedConfigC"),
          new ConfigEntry(config.NestedConfigB, "NestedNullConfigC"),
          new ConfigEntry(config.NestedConfigB, "EmptyConfig"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "IntProp"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "FloatProp"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "StringProp"),
          new ConfigEntry(config.NestedConfigB.NestedConfigC, "NullStringProp"),
        }));
      }
    }

    public class GetFlatTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UDictTest(EntryAccess.GetFlat(config), new Dictionary<string, ConfigEntry>
        {
          ["IntProp"] = EntryAccess.GetEntry(config, "IntProp"),
          ["FloatProp"] = EntryAccess.GetEntry(config, "FloatProp"),
          ["StringProp"] = EntryAccess.GetEntry(config, "StringProp"),
          ["NullStringProp"] = EntryAccess.GetEntry(config, "NullStringProp"),
          ["ShouldNotBeDugInto"] = EntryAccess.GetEntry(config, "ShouldNotBeDugInto"),
          ["NestedConfigB.IntProp"] = EntryAccess.GetEntry(config, "NestedConfigB.IntProp"),
          ["NestedConfigB.FloatProp"] = EntryAccess.GetEntry(config, "NestedConfigB.FloatProp"),
          ["NestedConfigB.StringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.StringProp"),
          ["NestedConfigB.NullStringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NullStringProp"),
          ["NestedConfigB.NestedConfigC.IntProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.IntProp"),
          ["NestedConfigB.NestedConfigC.FloatProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.FloatProp"),
          ["NestedConfigB.NestedConfigC.StringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.StringProp"),
          ["NestedConfigB.NestedConfigC.NullStringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.NullStringProp"),
        }));
      }
    }


    public class GetAllFlatTest : EntryAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UDictTest(EntryAccess.GetAllFlat(config), new Dictionary<string, ConfigEntry>
        {
          ["IntProp"] = EntryAccess.GetEntry(config, "IntProp"),
          ["FloatProp"] = EntryAccess.GetEntry(config, "FloatProp"),
          ["StringProp"] = EntryAccess.GetEntry(config, "StringProp"),
          ["NullStringProp"] = EntryAccess.GetEntry(config, "NullStringProp"),
          ["ShouldNotBeDugInto"] = EntryAccess.GetEntry(config, "ShouldNotBeDugInto"),
          ["NestedConfigB"] = EntryAccess.GetEntry(config, "NestedConfigB"),
          ["NestedNullConfigB"] = EntryAccess.GetEntry(config, "NestedNullConfigB"),
          ["EmptyConfig"] = EntryAccess.GetEntry(config, "EmptyConfig"),
          ["NestedConfigB.IntProp"] = EntryAccess.GetEntry(config, "NestedConfigB.IntProp"),
          ["NestedConfigB.FloatProp"] = EntryAccess.GetEntry(config, "NestedConfigB.FloatProp"),
          ["NestedConfigB.StringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.StringProp"),
          ["NestedConfigB.NullStringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NullStringProp"),
          ["NestedConfigB.NestedConfigC"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC"),
          ["NestedConfigB.NestedNullConfigC"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedNullConfigC"),
          ["NestedConfigB.EmptyConfig"] = EntryAccess.GetEntry(config, "NestedConfigB.EmptyConfig"),
          ["NestedConfigB.NestedConfigC.IntProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.IntProp"),
          ["NestedConfigB.NestedConfigC.FloatProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.FloatProp"),
          ["NestedConfigB.NestedConfigC.StringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.StringProp"),
          ["NestedConfigB.NestedConfigC.NullStringProp"] = EntryAccess.GetEntry(config, "NestedConfigB.NestedConfigC.NullStringProp"),
        }));
      }
    }

  }
}