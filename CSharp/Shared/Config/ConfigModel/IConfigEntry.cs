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

  public interface IConfigEntry
  {
    public object Value { get; set; }
    public ConfigEntry Get(string name);
    public IEnumerable<ConfigEntry> Entries { get; }
    public bool IsConfig { get; }
  }



}