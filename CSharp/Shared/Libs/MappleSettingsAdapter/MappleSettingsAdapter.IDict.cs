using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using Barotrauma.LuaCs;
using Barotrauma.LuaCs.Data;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections.Concurrent;

namespace BaroJunk
{
  public partial class MappleSettingsAdapter : IDictionary<string, string>
  {
    public string Get(string key) => GetSetting(key).GetStringValue();
    public string this[string key]
    {
      get => Get(key); set => Add(key, value);
    }

    #region IDictionary<string, string>
    public ICollection<string> Keys
      => Settings.Select(setting => setting.InternalName).ToArray();
    public ICollection<string> Values
      => Settings.Select(setting => setting.GetStringValue()).ToArray();
    public bool ContainsKey(string key)
      => ConfigService._settingsInstances.ContainsKey((Package, key));


    public void Add(string key, string value)
    {
      if (ContainsKey(key))
      {
        GetSetting(key).TrySetValue(value);
      }
      else
      {
        CreateSetting(key).TrySetValue(value);
      }
    }
    public bool Remove(string key) => RemoveSetting(key);
    public bool TryGetValue(string key, out string value)
    {
      if (ContainsKey(key))
      {
        value = Get(key); return true;
      }
      else
      {
        value = ""; return false;
      }
    }
    #endregion


    #region ICollection<KeyValuePair<string, string>>
    void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> keyValuePair)
      => Add(keyValuePair.Key, keyValuePair.Value);
    bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> keyValuePair)
      => ContainsKey(keyValuePair.Key) && Get(keyValuePair.Key) == keyValuePair.Value;
    bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> keyValuePair)
      => Remove(keyValuePair.Key);
    void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int index)
      => throw new Exception("don't do that, i'm too lazy to implement");

    public void Clear() => ClearSettings();
    public int Count => Keys.Count;

    bool ICollection<KeyValuePair<string, string>>.IsReadOnly => false;
    #endregion

    #region IEnumerable
    public IEnumerable<KeyValuePair<string, string>> EnumerateAsDict()
    {
      foreach (string key in Keys)
      {
        yield return new KeyValuePair<string, string>(key, Get(key));
      }
    }

    IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
      => EnumerateAsDict().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => EnumerateAsDict().GetEnumerator();
    #endregion


  }
}