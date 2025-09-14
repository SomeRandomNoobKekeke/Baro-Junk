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
  public class ConfigEntryProxy : IConfigEntry
  {
    public ConfigEntry Entry;

    public ConfigEntry Get(string entryPath) => Entry.Get(entryPath);
    public IEnumerable<ConfigEntry> Entries => Entry.Entries;
    public object Value
    {
      get => Entry.Value;
      set { Entry.Value = value; }
    }
    public bool IsConfig => Entry.IsConfig;

    public ConfigEntryProxy(ConfigEntry entry) => Entry = entry;
  }

}