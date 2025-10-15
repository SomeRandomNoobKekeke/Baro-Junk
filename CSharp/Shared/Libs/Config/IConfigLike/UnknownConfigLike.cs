using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  /// <summary>
  /// Ignore
  /// </summary>
  public class UnknownConfigLike : IConfiglike
  {
    public bool IsValid { get; } = false;
    public bool AmISubConfig { get; } = false;
    public bool HasProp(string key) => false;
    public Type TypeOf(string key) => null;
    public bool IsSubConfig(string key) => false;
    public object GetValue(string key) => null;
    public void SetValue(string key, object value) { }
    public IConfiglike GetConfig(string key) => new UnknownConfigLike();

    public IEnumerable<string> Keys => new string[0];
    public IEnumerable<object> Values => new object[0];

  }
}