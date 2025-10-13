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

    public bool IsValid => Target is not null;
    public bool IsSubConfig => IsValid && Target.GetType().IsAssignableTo(SubConfigType);

    public object GetValue(string propName)
      => Target?.GetType()?.GetProperty(propName, pls)?.GetValue(Target);

    public void SetValue(string propName, object value)
      => Target?.GetType()?.GetProperty(propName, pls)?.SetValue(Target, value);

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

    public ConfiglikeObject(object target) => Target = target;
  }
}