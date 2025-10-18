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
    public IDirectEntryLocatorTarget Target { get; }
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

    public object GetValue(string propPath) => GetEntry(propPath).Value;
    public void SetValue(string propPath, object value) => GetEntry(propPath).Value = value;

    public IEnumerable<ConfigEntry> GetEntries()
    {
      Dictionary<string, object> props = Host.AsDict;

      foreach (var (key, value) in props)
      {
        if (!Host.IsSubConfigProp(key))
        {
          yield return new ConfigEntry(Host, key);
        }
      }
    }

    public IEnumerable<ConfigEntry> GetAllEntries()
    {
      Dictionary<string, object> props = Host.AsDict;

      foreach (var (key, value) in props)
      {
        yield return new ConfigEntry(Host, key);
      }
    }

    public IEnumerable<ConfigEntry> GetEntriesRec()
    {
      Dictionary<string, object> props = Host.AsDict;

      foreach (var (key, value) in props)
      {
        if (!Host.IsSubConfigProp(key))
        {
          yield return new ConfigEntry(Host, key);
        }
      }

      foreach (var (key, value) in props)
      {
        if (Host.IsSubConfigProp(key))
        {
          IConfiglike subConfig = Host.ToConfig(value);
          if (!subConfig.IsValid) continue;

          foreach (ConfigEntry entry in subConfig.Locator.GetEntries())
          {
            yield return entry;
          }
        }
      }
    }
    public IEnumerable<ConfigEntry> GetAllEntriesRec()
    {
      Dictionary<string, object> props = Host.AsDict;

      foreach (var (key, value) in props)
      {
        yield return new ConfigEntry(Host, key);
      }

      foreach (var (key, value) in props)
      {
        if (Host.IsSubConfigProp(key))
        {
          IConfiglike subConfig = Host.ToConfig(value);
          if (!subConfig.IsValid) continue;

          foreach (ConfigEntry entry in subConfig.Locator.GetEntries())
          {
            yield return entry;
          }
        }
      }
    }
    public Dictionary<string, ConfigEntry> GetFlat()
    {
      Dictionary<string, ConfigEntry> flat = new();

      void scanPropsRec(IConfiglike cfg, string path = null)
      {
        Dictionary<string, object> props = Host.AsDict;

        foreach (var (key, value) in props)
        {
          string newPath = path is null ? key : String.Join('.', path, key);

          if (cfg.IsSubConfig(value))
          {
            IConfiglike subConfig = Host.ToConfig(value);
            if (!subConfig.IsValid) continue;
            scanPropsRec(subConfig, newPath);
          }
          else
          {
            flat[newPath] = new ConfigEntry(cfg, newPath);
          }
        }
      }

      scanPropsRec(Host);

      return flat;
    }
    public Dictionary<string, ConfigEntry> GetAllFlat()
    {
      Dictionary<string, ConfigEntry> flat = new();

      void scanPropsRec(IConfiglike cfg, string path = null)
      {
        Dictionary<string, object> props = Host.AsDict;

        foreach (var (key, value) in props)
        {
          string newPath = path is null ? key : String.Join('.', path, key);

          flat[newPath] = new ConfigEntry(cfg, newPath);

          if (cfg.IsSubConfig(value))
          {
            IConfiglike subConfig = Host.ToConfig(value);
            if (!subConfig.IsValid) continue;
            scanPropsRec(subConfig, newPath);
          }
        }
      }

      scanPropsRec(Host);

      return flat;
    }
    public Dictionary<string, object> GetFlatValues()
      => GetFlat().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Value);
    public Dictionary<string, object> GetAllFlatValues()
      => GetAllFlat().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Value);


    public DirectEntryLocator(IConfiglike host)
    {
      Target = new IConfigLikeLocatorAdapter(host);
    }

    public DirectEntryLocator(IDirectEntryLocatorTarget target)
    {
      Target = target;
    }

    public override string ToString() => $"DirectEntryLocator [{GetHashCode()}] Host: [{Host}]";
  }


}