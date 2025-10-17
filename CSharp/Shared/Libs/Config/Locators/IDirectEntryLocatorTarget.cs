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
  public interface IDirectEntryLocatorTarget
  {
    public IConfiglike Host { get; }
  }

  /// <summary>
  /// far beyond cringe
  /// </summary>
  public class ConfigEntryValuePromise : IDirectEntryLocatorTarget
  {
    public ConfigEntry Entry { get; }
    public IConfiglike Host => Entry.Host.GetConfig(Entry.Key);
    public ConfigEntryValuePromise(ConfigEntry entry) => Entry = entry;
  }

  public class DirectEntryLocatorTargetWrapper : IDirectEntryLocatorTarget
  {
    public IConfiglike Host { get; }
    public DirectEntryLocatorTargetWrapper(IConfiglike host) => Host = host;
  }

}