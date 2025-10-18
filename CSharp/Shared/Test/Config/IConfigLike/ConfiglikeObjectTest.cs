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
    public UTest TargetTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);
      return new UTest(configlike.Target, config);
    }

    public List<UTest> IsValidTest() => new List<UTest>(){
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).IsValid, true),
        new UTest(new ConfiglikeObject(null).IsValid, false),
      };

    public List<UTest> AmISubConfigTest() => new List<UTest>(){
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).AmISubConfig, true),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA().NestedConfigB).AmISubConfig, true),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA().IntProp).AmISubConfig, false),
        new UTest(new ConfiglikeObject(null).AmISubConfig, false),
      };

    public List<UTest> LocatorTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new List<UTest>(){
          new UTest(configlike.Locator is not null, true),
          new UTest(configlike.Locator.Host, configlike),
        };
    }

    public List<UTest> HasPropTest() => new List<UTest>(){
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).HasProp("IntProp"), true),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).HasProp("Bruh"), false),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).HasProp("NestedConfigB"), true),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).HasProp(""), false),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).HasProp(null), false),
      };

    public List<UTest> TypeOfPropTest() => new List<UTest>(){
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).TypeOfProp("IntProp"), typeof(int)),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).TypeOfProp("Bruh"), null),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).TypeOfProp("NestedConfigB"), typeof(ExampleConfigs.ConfigB)),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).TypeOfProp(""), null),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).TypeOfProp(null), null),
      };

    public List<UTest> IsPropASubConfigTest() => new List<UTest>(){
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).IsPropASubConfig("IntProp"), false),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).IsPropASubConfig("Bruh"), false),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).IsPropASubConfig("NestedConfigB"), true),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).IsPropASubConfig(""), false),
        new UTest(new ConfiglikeObject(new ExampleConfigs.ConfigA()).IsPropASubConfig(null), false),
      };

    public List<UTest> IsSubConfigTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new List<UTest>()
        {
          new UTest(configlike.IsSubConfig(config), true),
          new UTest(configlike.IsSubConfig(config.NestedConfigB), true),
          new UTest(configlike.IsSubConfig(config.IntProp), false),
          new UTest(configlike.IsSubConfig(null), false),
          new UTest(configlike.IsSubConfig(123), false),
          new UTest(configlike.IsSubConfig(""), false),
          new UTest(configlike.IsSubConfig(new object()), false),
          new UTest(configlike.IsSubConfig(typeof(string)), false),
          new UTest(configlike.IsSubConfig(typeof(ExampleConfigs.ConfigA)), true),
          new UTest(configlike.IsSubConfig(typeof(IConfig)), true),
        };
    }

    public List<UTest> GetValueTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new List<UTest>()
        {
          new UTest(configlike.GetValue("IntProp"), 2),
          new UTest(configlike.GetValue("NestedConfigB"), config.NestedConfigB),
          new UTest(configlike.GetValue(""), null),
          new UTest(configlike.GetValue(null), null),
          new UTest(configlike.GetValue("Bruh"), null),
        };
    }

    public List<UTest> SetValueTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      configlike.SetValue("IntProp", 3);
      configlike.SetValue("NestedNullConfigB", new ExampleConfigs.ConfigB());
      configlike.SetValue("NestedConfigB", null);
      configlike.SetValue("", null);
      configlike.SetValue(null, null);
      configlike.SetValue("Bruh", null);

      return new List<UTest>()
        {
          new UTest(config.IntProp, 3),
          new UTest(config.NestedConfigB is null, true),
          new UTest(config.NestedNullConfigB is not null, true),
        };
    }

    public List<UTest> GetPropAsConfigTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new List<UTest>()
        {
          new UTest(configlike.GetPropAsConfig("IntProp").AmISubConfig, false),
          new UTest(configlike.GetPropAsConfig("NestedConfigB").AmISubConfig, true),
          new UTest(configlike.GetPropAsConfig("").AmISubConfig, false),
          new UTest(configlike.GetPropAsConfig(null).AmISubConfig, false),
          new UTest(configlike.GetPropAsConfig("Bruh").AmISubConfig, false),
        };
    }

    public List<UTest> ToConfigTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new List<UTest>()
        {
          new UTest(configlike.ToConfig("Bruh").AmISubConfig, false),
          new UTest(configlike.ToConfig(config).AmISubConfig, true),
          new UTest(configlike.ToConfig(new object()).AmISubConfig, false),
          new UTest(configlike.ToConfig(null).AmISubConfig, false),
          new UTest(configlike.ToConfig(123).AmISubConfig, false),
        };
    }

    public UListTest KeysTest() => new UListTest(
      new ConfiglikeObject(new ExampleConfigs.ConfigA()).Keys,
      new List<string>()
      {
          "IntProp","FloatProp","StringProp","NullStringProp","ShouldNotBeDugInto","NestedConfigB","NestedNullConfigB","EmptyConfig",
      }
    );


    public UDictTest AsDictTest()
    {
      ExampleConfigs.ConfigA config = new();
      IConfiglike configlike = new ConfiglikeObject(config);

      return new UDictTest(
        configlike.AsDict,
        new Dictionary<string, object>
        {
          ["IntProp"] = config.IntProp,
          ["FloatProp"] = config.FloatProp,
          ["StringProp"] = config.StringProp,
          ["NullStringProp"] = config.NullStringProp,
          ["ShouldNotBeDugInto"] = config.ShouldNotBeDugInto,
          ["NestedConfigB"] = config.NestedConfigB,
          ["NestedNullConfigB"] = config.NestedNullConfigB,
          ["EmptyConfig"] = config.EmptyConfig,
        }
      );
    }
  }
}