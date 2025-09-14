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

  public interface IConfig : IConfigEntry
  {
    private static IConfig _current;
    public static IConfig Current
    {
      get => _current;
      set => _current = value;
    }

    object IConfigEntry.Value { get => this; set { /*bruh*/ } }
    IConfigEntry IConfigEntry.Get(string entryPath) => PropAccess.GetEntry(this, entryPath);
    IEnumerable<IConfigEntry> IConfigEntry.Entries => PropAccess.GetEntries(this);
    bool IConfigEntry.IsConfig => true;
    string IConfigEntry.Name => this.GetType().Name;

    public ConfigMixin Mixin => ConfigMixin.Mixins.GetValue(this, c => new ConfigMixin(c));
    public string Name => Mixin.Bruh;


  }



}