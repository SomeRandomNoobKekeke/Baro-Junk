using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  /// <summary>
  /// Something like Dictionary<string, object>
  /// Half of this props are unused, idk what i need
  /// </summary>
  public interface IConfiglike
  {
    public DirectEntryLocator Locator { get; }

    public bool IsValid { get; }
    public bool AmISubConfig { get; }
    public bool HasProp(string key);
    public Type TypeOf(string key);
    public bool IsSubConfigProp(string key);
    public bool IsSubConfig(object o);
    public bool IsSubConfig(Type T);
    public object GetValue(string key);
    public void SetValue(string key, object value);
    public IConfiglike GetConfig(string key);
    public IConfiglike ToConfig(object o);
    public IEnumerable<string> Keys { get; }
    public IEnumerable<object> Values { get; }
    public Dictionary<string, object> AsDict { get; }
  }
}