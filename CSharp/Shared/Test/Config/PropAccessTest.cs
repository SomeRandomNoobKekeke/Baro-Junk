using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class PropAccessTest : UTestPack
  {

    public class GetEntryTest : PropAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UTest(PropAccess.GetEntry(config, "IntProp"), new ConfigEntry(config, "IntProp")));
        Tests.Add(new UTest(PropAccess.GetEntry(config, "NestedConfigB.IntProp"), new ConfigEntry(config.NestedConfigB, "IntProp")));
      }
    }

    public class GetSetPropTest : PropAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UTest(PropAccess.GetProp(config, "IntProp"), 2));
        Tests.Add(new UTest(PropAccess.GetProp(config, "NestedConfigB.IntProp"), 4));
        Tests.Add(new UTest(PropAccess.GetProp(PropAccess.GetProp(config, "NestedConfigB"), "IntProp"), 4));

        Tests.Add(new UTest(PropAccess.GetProp(config, "bebebe"), null));
        Tests.Add(new UTest(PropAccess.GetProp(config, ""), null));
        Tests.Add(new UTest(PropAccess.GetProp(config, null), null));
        Tests.Add(new UTest(PropAccess.GetProp(config, ".."), null));
        Tests.Add(new UTest(PropAccess.GetProp(config, "NestedConfigB.qweqe"), null));
        Tests.Add(new UTest(PropAccess.GetProp(config, ".NestedConfigB.IntProp"), null));

        PropAccess.SetProp(config, "IntProp", 3);
        Tests.Add(new UTest(PropAccess.GetProp(config, "IntProp"), 3));

        PropAccess.SetProp(config, "NestedConfigB.IntProp", 5);
        Tests.Add(new UTest(PropAccess.GetProp(config, "NestedConfigB.IntProp"), 5));
      }
    }

    public class GetEntriesTest : PropAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UListTest(PropAccess.GetEntries(config).ToList(), new List<ConfigEntry>()
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

    public class GetEntriesRecTest : PropAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        Tests.Add(new UListTest(PropAccess.GetEntriesRec(config).ToList(), new List<ConfigEntry>()
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

    public class GetFlatTest : PropAccessTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        // Tests.Add(new UListTest(PropAccess.GetEntriesRec(config).ToList(), new List<ConfigEntry>()
        // {
        //   new ConfigEntry(config, "IntProp"),
        //   new ConfigEntry(config, "FloatProp"),
        //   new ConfigEntry(config, "StringProp"),
        //   new ConfigEntry(config, "NullStringProp"),
        //   new ConfigEntry(config, "ShouldNotBeDugInto"),
        //   new ConfigEntry(config, "NestedConfigB"),
        //   new ConfigEntry(config, "NestedNullConfigB"),
        //   new ConfigEntry(config, "EmptyConfig"),
        //   new ConfigEntry(config.NestedConfigB, "IntProp"),
        //   new ConfigEntry(config.NestedConfigB, "FloatProp"),
        //   new ConfigEntry(config.NestedConfigB, "StringProp"),
        //   new ConfigEntry(config.NestedConfigB, "NullStringProp"),
        //   new ConfigEntry(config.NestedConfigB, "NestedConfigC"),
        //   new ConfigEntry(config.NestedConfigB, "NestedNullConfigC"),
        //   new ConfigEntry(config.NestedConfigB, "EmptyConfig"),
        //   new ConfigEntry(config.NestedConfigB.NestedConfigC, "IntProp"),
        //   new ConfigEntry(config.NestedConfigB.NestedConfigC, "FloatProp"),
        //   new ConfigEntry(config.NestedConfigB.NestedConfigC, "StringProp"),
        //   new ConfigEntry(config.NestedConfigB.NestedConfigC, "NullStringProp"),
        // }));
      }
    }

  }
}