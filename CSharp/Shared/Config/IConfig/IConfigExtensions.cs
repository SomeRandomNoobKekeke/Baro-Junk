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
      => config.Mixin.OnPropChanged += action;
  }

}