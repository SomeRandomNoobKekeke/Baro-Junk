using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public interface IConfiglike
  {
    public bool IsSubConfig { get; }
    public bool IsValid { get; }
    public object GetValue(string propName);
    public void SetValue(string propName, object value);
    public IEnumerable<string> Keys { get; }
    public IEnumerable<object> Values { get; }
  }
}