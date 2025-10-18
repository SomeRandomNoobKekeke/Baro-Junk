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

    public bool IsConfig => Host?.IsSubConfig(Key) == true;
    public bool IsValid => Host is not null && Host.HasProp(Key);
    public Type Type => Host?.TypeOfProp(Key);
    public object Value
    {
      get => Host?.GetValue(Key);
      set => Host?.SetValue(Key, value);
    }

    public ConfigEntry(IConfiglike host, string key)
    {
      // ArgumentNullException.ThrowIfNull(host, "can't create an entry without a host, you're using it wrong");
      // ArgumentNullException.ThrowIfNull(key, "can't create an entry without a key, you're using it wrong");

      Host = host;
      Key = key ?? "";

      Locator = new DirectEntryLocator(new ConfigEntryLocatorAdapter(this));
    }

    public override bool Equals(object obj)
    {
      if (obj is not ConfigEntry other) return false;
      if (Host is null && other.Host is null) return true;
      if (Host is null || other.Host is null) return false;

      return Object.Equals(Host.Target, other.Host.Target) && Key == other.Key;
    }

    public override string ToString() => $"[{(IsValid ? "" : "!")}{Host?.Target?.GetType().Name}.{Key} ({Value})]";
    public string DebugLog => $"ConfigEntry [{GetHashCode()}] Host: [{Host}] Locator: [{Locator}]";
  }
}