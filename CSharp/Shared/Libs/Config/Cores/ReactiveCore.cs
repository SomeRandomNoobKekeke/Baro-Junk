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
  /// It's just an object where you can listen for reactive events
  /// </summary>
  public class ReactiveCore
  {
    public ConfigCore Core { get; }
    public IConfiglike Host => Core.Host;
    public ReactiveEntryLocator Locator { get; }


    public bool DeeplyReactive { get; set; } = false;

    public event Action<string, object> PropChanged;
    public event Action Updated;

    public Action<string, object> OnPropChanged { set { PropChanged += value; } }
    public Action OnUpdated { set { Updated += value; } }

    public void RaisePropChanged(string key, object value)
    {
      PropChanged?.Invoke(key, value);

      if (DeeplyReactive)
      {
        string[] names = key.Split('.');
        if (names.Length < 2) return;

        ConfigEntry entry = Core.GetEntry(names[0]);

        if (entry.IsConfig)
        {
          entry.Host.Core?.ReactiveCore.RaisePropChanged(string.Join('.', names.Skip(1)), value);
        }
      }
    }
    public void RaiseUpdated()
    {
      Updated?.Invoke();

      if (DeeplyReactive)
      {
        foreach (ConfigEntry entry in Core.GetSubConfigs())
        {
          entry.Host.Core?.ReactiveCore.RaiseUpdated();
        }
      }
    }

    public ReactiveCore(ConfigCore core)
    {
      Core = core;
      Locator = new ReactiveEntryLocator(this, new IConfigLikeLocatorAdapter(Host), null);
    }

    public override string ToString() => $"ReactiveCore [{GetHashCode()}]";
  }

}