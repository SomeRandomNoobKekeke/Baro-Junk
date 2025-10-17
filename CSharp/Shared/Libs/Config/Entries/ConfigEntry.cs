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
  public class ConfigEntry : IConfigEntry, IDirectEntryLocatorTarget
  {
    public static ConfigEntry Empty => new ConfigEntry(null, "");

    public DirectEntryLocator Locator { get; }
    public IConfiglike Host { get; }
    public string Key { get; }

    public bool IsConfig => Host.IsSubConfig(Key);
    public bool IsValid => Host.HasProp(Key);
    public Type Type => Host.TypeOf(Key);
    public object Value
    {
      get => Host?.GetValue(Key);
      set => Host?.SetValue(Key, value);
    }

    public ConfigEntry(IConfiglike host, string key)
    {
      Host = host;
      Key = key ?? "";
      //CRINGE
      Locator = new DirectEntryLocator(new ConfigEntryValuePromise(this));

      Mod.Logger.Log($"ConfigEntry created");
      Mod.Logger.LogVars(Host, key, Locator);
    }

    public override bool Equals(object obj)
    {
      if (obj is not ConfigEntry other) return false;
      return Object.Equals(Host.Target, other.Host.Target) && Key == other.Key;
    }

    public override string ToString() => $"[{(IsValid ? "" : "!")}{Value}]";
    public string DebugLog => $"ConfigEntry [{GetHashCode()}] Host: [{Host}] Locator: [{Locator}]";
  }
}