using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace BaroJunk
{
  public class ConfigModel : IConfigEntry
  {
    public IConfig Config;
    public Dictionary<string, ConfigEntryProxy> Proxies = new();

    public event Action<string, object> PropChanged;
    public void RaiseOnPropChanged(string key, object value) => PropChanged?.Invoke(key, value);
    public void OnPropChanged(Action<string, object> callback) => PropChanged += callback;


    public object Value { get => this; set {/*bruh*/ } }
    public IConfigEntry Get(string name) => Proxies.ContainsKey(name) ? Proxies[name] : ConfigEntry.Empty;
    public IEnumerable<IConfigEntry> Entries
      => Config.Entries.Select(Entry => Get(Entry.Name));
    public bool IsConfig => Config.IsConfig;
    public string Name => Config.Name;

    public ConfigModel(IConfig config)
    {
      Config = config;
      Dictionary<string, ConfigEntry> flat = PropAccess.GetAllFlat(config);
      Proxies = flat.ToDictionary(
        kvp => kvp.Key,
        kvp => new ConfigEntryProxy(this, kvp.Key, kvp.Value)
      );
    }
  }



}