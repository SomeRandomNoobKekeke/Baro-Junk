using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public interface IConfig : IDirectlyLocatable, IReactiveLocatable
  {
    public static ConditionalWeakTable<IConfig, ConfigCore> Cores = new();

    public ConfigCore Core => Cores.GetValue(this, c => new ConfigCore(c));


    public IConfiglike Host => Core.Host;
    public ReactiveCore ReactiveCore => Core.ReactiveCore;
    public string ID => Core.ID;

    DirectEntryLocator IDirectlyLocatable.Locator => Core.Locator;
    ReactiveEntryLocator IReactiveLocatable.ReactiveLocator => Core.ReactiveLocator;
    public ConfigManager Manager => Core.Manager;

    public SimpleParser Parser
    {
      get => Core.Parser;
      set => Core.Parser = value;
    }
    public NetParser NetParser
    {
      get => Core.NetParser;
      set => Core.NetParser = value;
    }
    public XMLParser XMLParser
    {
      get => Core.XMLParser;
      set => Core.XMLParser = value;
    }
    public Logger Logger
    {
      get => Core.Logger;
      set => Core.Logger = value;
    }

    public IConfigFacades Facades
    {
      get => Core.Facades;
      set => Core.Facades = value;
    }
    public ConfigSettings Settings
    {
      get => Core.Settings;
      set => Core.Settings = value;
    }
  }
}