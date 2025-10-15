using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public class ConfiglikeObject : IConfiglike
  {
    public static Type SubConfigType = typeof(IConfig);
    public static BindingFlags pls = BindingFlags.Public | BindingFlags.Instance;

    public object Target { get; private set; }
    public bool IsValid { get; private set; }
    public bool AmISubConfig { get; private set; }

    public bool HasProp(string key)
      => Target?.GetType()?.GetProperty(key, pls) is not null;

    public Type TypeOf(string key)
      => Target?.GetType()?.GetProperty(key, pls).PropertyType;

    public bool IsSubConfig(string key)
      => Target?.GetType()?.GetProperty(key, pls).PropertyType == SubConfigType;

    public object GetValue(string key)
      => Target?.GetType()?.GetProperty(key, pls)?.GetValue(Target);

    public void SetValue(string key, object value)
      => Target?.GetType()?.GetProperty(key, pls)?.SetValue(Target, value);

    public IConfiglike GetConfig(string key)
      => new ConfiglikeObject(Target?.GetType()?.GetProperty(key, pls)?.GetValue(Target));

    public IEnumerable<string> Keys
    {
      get
      {
        if (!IsValid) return new string[0];
        return Target.GetType().GetProperties(pls).Select(pi => pi.Name);
      }
    }

    public IEnumerable<object> Values
    {
      get
      {
        if (!IsValid) return new object[0];
        return Target.GetType().GetProperties(pls).Select(pi => pi.GetValue(Target));
      }
    }

    public ConfiglikeObject(object target)
    {
      Target = target;
      IsValid = Target is not null;
      AmISubConfig = IsValid && Target.GetType() == SubConfigType;

    }
  }
}