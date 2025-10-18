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
  /// <summary>
  /// ReactiveEntry is a reactive proxy for ConfigEntry
  /// It has all IConfigEntry methods, but doesn't implement it
  /// This is made on purpose so you couldn't confuse reactive services with direct ones
  /// </summary>
  public class ReactiveEntry //: IConfigEntry
  {
    public ReactiveEntryLocator Locator { get; private set; }

    public ReactiveCore ReactiveCore { get; private set; }
    public ConfigEntry Entry { get; private set; }
    public string Path { get; private set; }

    public object Value
    {
      get => Entry.Value;
      set
      {
        Entry.Value = value;
        ReactiveCore.RaiseOnPropChanged(Path, value);
      }
    }
    public bool IsConfig => Entry.IsConfig;
    public bool IsValid => Entry.IsValid;
    public string Key => Entry.Key;
    public Type Type => Entry.Type;


    public ReactiveEntry(ReactiveCore core, ConfigEntry entry, string path)
    {
      ReactiveCore = core;
      Entry = entry;
      Path = path;
      Locator = new ReactiveEntryLocator(core, new ConfigEntryLocatorAdapter(entry), path);
    }
    public override string ToString() => Entry.ToString();
    public string DebugLog => $"ReactiveEntry [{GetHashCode()}]  core: [{ReactiveCore}] Entry: [{Entry.DebugLog}] Path: [{Path}] Locator: [{Locator}]";
  }

}