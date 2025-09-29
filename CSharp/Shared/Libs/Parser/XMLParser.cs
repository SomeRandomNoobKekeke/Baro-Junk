using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace BaroJunk
{
  public static class XMLParser
  {
    public static SimpleParser Parser { get; set; } = new SimpleParser();

    //TODO it shouldn't throw
    public static SimpleResult Parse(XElement element, Type T)
    {
      MethodInfo fromxml = T.GetMethod("FromXML", BindingFlags.Public | BindingFlags.Instance);
      if (fromxml != null) return SimpleResult.Success(fromxml.Invoke(null, new object[] { element }));

      return SimpleResult.Success(Parser.Parse(element.Value, T).Result);
    }

    public static XElement Serialize(IConfigEntry entry)
      => Serialize(entry.Value, entry.Name);

    public static XElement Serialize(object o, string name)
    {
      if (o is null) return new XElement(name, Parser.Serialize(o).Result);

      MethodInfo toxml = o.GetType().GetMethod("ToXml", BindingFlags.Public | BindingFlags.Instance);
      if (toxml != null) return (XElement)toxml.Invoke(o, new object[] { });

      return new XElement(name, Parser.Serialize(o).Result);
    }
  }
}
