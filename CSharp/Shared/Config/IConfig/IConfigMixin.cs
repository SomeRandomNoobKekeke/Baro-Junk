using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  /// <summary>
  /// Additional state attached to IConfig
  /// </summary>
  public class ConfigMixin
  {
    public static ConditionalWeakTable<IConfig, ConfigMixin> Mixins = new();

    public IConfig Config;
    public ConfigModel Model;
    public string Bruh => Config.GetType().Name;
    public event Action<string, object> OnPropChanged;



    public ConfigMixin(IConfig config)
    {
      Config = config;
      Model = new ConfigModel(config);
    }
  }




}