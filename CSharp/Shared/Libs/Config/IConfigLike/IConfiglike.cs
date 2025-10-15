using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public interface IConfiglike
  {
    public bool IsValid { get; }
    public bool AmISubConfig { get; }
    public bool HasProp(string key);
    public Type TypeOf(string key);
    public bool IsSubConfig(string key);
    public object GetValue(string key);
    public void SetValue(string key, object value);
    public IConfiglike GetConfig(string key);
    public IEnumerable<string> Keys { get; }
    public IEnumerable<object> Values { get; }
  }
}