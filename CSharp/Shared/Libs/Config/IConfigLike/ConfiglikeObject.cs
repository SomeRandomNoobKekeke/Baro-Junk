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

    public object Target { get; }
    public bool IsValid { get; }
    public bool AmISubConfig { get; }
    public DirectEntryLocator Locator { get; }

    public bool HasProp(string key)
      => Target?.GetType()?.GetProperty(key, pls) is not null;

    public Type TypeOfProp(string key)
      => Target?.GetType()?.GetProperty(key, pls).PropertyType;

    public bool IsPropASubConfig(string key)
      => Target?.GetType()?.GetProperty(key, pls).PropertyType == SubConfigType;

    public bool IsSubConfig(object o) => o?.GetType() == SubConfigType;

    public bool IsSubConfig(Type T) => T == SubConfigType;

    public object GetValue(string key)
      => Target?.GetType()?.GetProperty(key, pls)?.GetValue(Target);

    public void SetValue(string key, object value)
      => Target?.GetType()?.GetProperty(key, pls)?.SetValue(Target, value);

    public IConfiglike GetPropAsConfig(string key) => ToConfig(GetValue(key));
    public IConfiglike ToConfig(object o) => new ConfiglikeObject(o);

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

    public Dictionary<string, object> AsDict
      => !IsValid ? new Dictionary<string, object>()
         : Target.GetType().GetProperties(pls)
         .ToDictionary(pi => pi.Name, pi => pi.GetValue(Target));

    public ConfiglikeObject(object target)
    {
      Target = target;
      IsValid = Target is not null;
      AmISubConfig = IsValid && Target.GetType() == SubConfigType;
      Locator = new DirectEntryLocator(new IConfigLikeLocatorAdapter(this));
    }

    public override string ToString() => $"ConfiglikeObject [{Target} ({Target.GetHashCode()})]";
  }
}