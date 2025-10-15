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
  public class ConfigEntry : IConfigEntry, IDirectEntryLocatorTarget, IDirectEntryLocatorHost
  {
    public static ConfigEntry Empty => new ConfigEntry(null, "");

    public DirectEntryLocator Locator { get; }
    public IConfiglike Host { get; private set; }
    public string Key { get; private set; }

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
      Locator = new DirectEntryLocator(this);
    }

    public override string ToString() => $"[{(IsValid ? "" : "!")}{Value}]";
  }
}