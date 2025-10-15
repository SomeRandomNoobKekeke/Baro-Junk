using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;

using Barotrauma;

namespace BaroJunk
{
  public class DirectEntryLocator
  {
    public IDirectEntryLocatorTarget Target { get; private set; }
    public IConfiglike Host => Target.Host;
    public ConfigEntry GetEntry(string propPath)
    {
      if (!Host.IsValid || propPath is null) return ConfigEntry.Empty;

      string[] names = propPath.Split('.');
      if (names.Length == 0) return ConfigEntry.Empty;

      IConfiglike o = Host;

      foreach (string name in names.SkipLast(1))
      {
        if (!o.IsValid) return ConfigEntry.Empty;
        o = o.GetConfig(name);
      }

      return new ConfigEntry(o, names.Last());
    }

    // public object GetProp(string propPath) { }
    // public void SetProp(string propPath, object value) { }
    // public IEnumerable<ConfigEntry> GetEntries() { }
    // public IEnumerable<ConfigEntry> GetAllEntries() { }
    // public IEnumerable<ConfigEntry> GetEntriesRec() { }
    // public IEnumerable<ConfigEntry> GetAllEntriesRec() { }
    // public Dictionary<string, ConfigEntry> GetFlat() { }
    // public Dictionary<string, ConfigEntry> GetAllFlat() { }
    // public Dictionary<string, object> GetFlatValues() { }
    // public Dictionary<string, object> GetAllFlatValues() { }

    public DirectEntryLocator(IDirectEntryLocatorTarget target)
    {
      Target = target;
    }
  }


}