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
    public IConfiglike Host { get; }
    public ReactiveEntryLocator Locator { get; }

    public event Action<string, object> PropChanged;
    public event Action Updated;

    public Action<string, object> OnPropChanged { set { PropChanged += value; } }
    public Action OnUpdated { set { Updated += value; } }

    public void RaisePropChanged(string key, object value) => PropChanged?.Invoke(key, value);
    public void RaiseUpdated() => Updated?.Invoke();

    public ReactiveCore(IConfiglike host)
    {
      Host = host;
      Locator = new ReactiveEntryLocator(this, new IConfigLikeLocatorAdapter(host), null);
    }

    public override string ToString() => $"ReactiveCore [{GetHashCode()}]";
  }

}