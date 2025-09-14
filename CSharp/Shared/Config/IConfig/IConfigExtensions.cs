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

  public static class IConfigExtensions
  {
    public static string GetName(this IConfig config) => config.Name;
    public static IConfig Self(this IConfig config) => config;
    public static void OnPropChanged(this IConfig config, Action<string, object> action)
      => config.OnPropChanged(action);

    public static IConfigEntry Get(this IConfig config, string entryPath) => config.Get(entryPath);
    public static IEnumerable<IConfigEntry> GetEntries(this IConfig config) => config.Entries;


    public static string ToText(this IConfig config) => config.ToText();
    public static XElement ToXML(this IConfig config) => config.ToXML();
    public static void FromXML(this IConfig config, XElement element) => config.FromXML(element);



  }

}