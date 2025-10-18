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
  public class ReactiveEntryLocator
  {
    public ReactiveCore Core { get; }
    public IConfiglike Host { get; }
    public string CurrentPath { get; }

    public string RelativePath(string path)
      => string.IsNullOrEmpty(CurrentPath) ? path : String.Join('.', CurrentPath, path);


    public ReactiveEntry GetEntry(string propPath)
      => new ReactiveEntry(Core, Host.Locator.GetEntry(propPath), RelativePath(propPath));

    public object GetValue(string propPath) => GetEntry(propPath).Value;

    public void SetValue(string propPath, object value) => GetEntry(propPath).Value = value;

    public IEnumerable<ReactiveEntry> GetEntries()
    {
      Dictionary<string, object> props = Host.AsDict;

      foreach (var (key, value) in props)
      {
        if (!Host.IsPropASubConfig(key))
        {
          yield return new ReactiveEntry(Core, new ConfigEntry(Host, key), RelativePath(key));
        }
      }
    }

    public IEnumerable<ReactiveEntry> GetAllEntries()
    {
      Dictionary<string, object> props = Host.AsDict;

      foreach (var (key, value) in props)
      {
        yield return new ReactiveEntry(Core, new ConfigEntry(Host, key), RelativePath(key));
      }
    }

    public IEnumerable<ReactiveEntry> GetEntriesRec()
    {
      IEnumerable<ReactiveEntry> scanPropsRec(IConfiglike cfg, string path = null)
      {
        Dictionary<string, object> props = Host.AsDict;

        foreach (var (key, value) in props)
        {
          string newPath = path is null ? key : String.Join('.', path, key);

          if (cfg.IsSubConfig(value))
          {
            IConfiglike subConfig = Host.ToConfig(value);
            if (!subConfig.IsValid) continue;
            foreach (ReactiveEntry entry in scanPropsRec(subConfig, newPath))
            {
              yield return entry;
            }
          }
          else
          {
            yield return new ReactiveEntry(Core, new ConfigEntry(cfg, newPath), RelativePath(newPath));
          }
        }
      }

      foreach (ReactiveEntry entry in scanPropsRec(Host))
      {
        yield return entry;
      }
    }

    public IEnumerable<ReactiveEntry> GetAllEntriesRec()
    {
      IEnumerable<ReactiveEntry> scanPropsRec(IConfiglike cfg, string path = null)
      {
        Dictionary<string, object> props = Host.AsDict;

        foreach (var (key, value) in props)
        {
          string newPath = path is null ? key : String.Join('.', path, key);

          yield return new ReactiveEntry(Core, new ConfigEntry(cfg, newPath), RelativePath(newPath));

          if (cfg.IsSubConfig(value))
          {
            IConfiglike subConfig = Host.ToConfig(value);
            if (!subConfig.IsValid) continue;
            foreach (ReactiveEntry entry in scanPropsRec(subConfig, newPath))
            {
              yield return entry;
            }
          }
        }
      }

      foreach (ReactiveEntry entry in scanPropsRec(Host))
      {
        yield return entry;
      }
    }

    public Dictionary<string, ReactiveEntry> GetFlat()
      => Host.Locator.GetFlat().ToDictionary(
        kvp => kvp.Key,
        kvp => new ReactiveEntry(Core, kvp.Value, RelativePath(kvp.Key))
      );

    public Dictionary<string, ReactiveEntry> GetAllFlat()
      => Host.Locator.GetAllFlat().ToDictionary(
        kvp => kvp.Key,
        kvp => new ReactiveEntry(Core, kvp.Value, RelativePath(kvp.Key))
      );

    public Dictionary<string, object> GetFlatValues() => Host.Locator.GetFlatValues();
    public Dictionary<string, object> GetAllFlatValues() => Host.Locator.GetFlatValues();

    public ReactiveEntryLocator(ReactiveCore core, IConfiglike host, string path)
    {
      Core = core;
      Host = host;
      CurrentPath = path;
    }

    public override string ToString() => $"ReactiveEntryLocator [{GetHashCode()}] Core: [{Core}] Host: [{Host}] CurrentPath: [{CurrentPath}]";
  }


}