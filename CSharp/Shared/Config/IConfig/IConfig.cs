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

  public partial interface IConfig : IConfigEntry
  {
    public static string HookId => Assembly.GetExecutingAssembly().GetName().Name;


    private static IConfig _current;
    public static IConfig Current
    {
      get => _current;
      set
      {
        _current = value;

      }
    }

    public static IIOAdapter IOAdapter = new IOAdapter();
    public static ConfigLogger Logger = new();
    public static ConfigSaver ConfigSaver = new(IOAdapter);

    public void OnPropChanged(Action<string, object> action) => Mixin.Model.OnPropChanged(action);

    public string ID => $"{this.GetType().Namespace}_{this.GetType().Name}";


    object IConfigEntry.Value { get => this; set { /*bruh*/ } }
    IConfigEntry IConfigEntry.Get(string entryPath) => Mixin.Model.Get(entryPath);
    IEnumerable<IConfigEntry> IConfigEntry.Entries => Mixin.Model.Entries;
    bool IConfigEntry.IsConfig => true;
    bool IConfigEntry.IsValid => true; // :BaroDev:
    string IConfigEntry.Name => this.GetType().Name;
    Type IConfigEntry.Type => this.GetType();

    public ConfigMixin Mixin => ConfigMixin.Mixins.GetValue(this, c => new ConfigMixin(c));

    public object GetProp(string propPath) => PropAccess.GetProp(this, propPath);
    public void SetProp(string propPath, object value) => PropAccess.SetProp(this, propPath, value);
    public ConfigEntry GetEntry(string propPath) => PropAccess.GetEntry(this, propPath);
    public IEnumerable<ConfigEntry> GetEntries() => PropAccess.GetEntries(this);
    public IEnumerable<ConfigEntry> GetAllEntries() => PropAccess.GetAllEntries(this);
    public IEnumerable<ConfigEntry> GetEntriesRec() => PropAccess.GetEntriesRec(this);
    public IEnumerable<ConfigEntry> GetAllEntriesRec() => PropAccess.GetAllEntriesRec(this);
    public Dictionary<string, ConfigEntry> GetFlat() => PropAccess.GetFlat(this);
    public Dictionary<string, ConfigEntry> GetAllFlat() => PropAccess.GetAllFlat(this);
    public Dictionary<string, object> GetFlatValues() => PropAccess.GetFlatValues(this);

  }



}