using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;

namespace BaroJunk
{

  public partial interface IConfig
  {
    public string ToText()
    {
      Dictionary<string, ConfigEntry> flat = GetAllFlat();
      StringBuilder sb = new StringBuilder();

      sb.Append("{\n");
      foreach (string key in flat.Keys)
      {
        sb.Append($"    {key}: [{ConfigLogger.WrapInColor(flat[key], "white")}],\n");
      }
      sb.Append("}");

      return sb.ToString();
    }


    public XElement ToXML()
    {
      XElement element = new XElement(Name);

      foreach (IConfigEntry entry in Entries)
      {
        if (entry.IsConfig)
        {
          IConfig subConfig = entry.Value as IConfig;
          if (subConfig is null) continue;
          element.Add(subConfig.ToXML());
        }
      }

      foreach (IConfigEntry entry in Entries)
      {
        if (!entry.IsConfig)
        {
          element.Add(XMLParser.Serialize(entry));
        }
      }

      return element;
    }

    public void FromXML(XElement element)
    {
      foreach (XElement child in element.Elements())
      {
        IConfigEntry entry = Get(child.Name.ToString());
        if (!entry.IsValid) continue;

        if (entry.IsConfig)
        {
          IConfig subConfig = entry.Value as IConfig;

          if (subConfig is null)
          {
            subConfig = (IConfig)Activator.CreateInstance(entry.Type);
            entry.Value = subConfig;
          }

          subConfig.FromXML(child);
        }
        else
        {
          entry.Value = XMLParser.Parse(child, entry.Type);
        }
      }
    }

    // public static Func<string[][]> ToHints(object config)
    // {
    //   if (config is null) return () => new string[][] { };
    //   return () => new string[][] { ConfigTraverse.GetFlat(config).Keys.ToArray() };
    // }

    // public static Hint ToAdvancedHints(object config)
    // {
    //   Hint root = new Hint();

    //   if (config is null) return root;

    //   void scanHintsRec(Type T, Hint node)
    //   {
    //     PropertyInfo[] props = T.GetProperties(BindingFlags.Instance | BindingFlags.Public);

    //     foreach (PropertyInfo pi in props)
    //     {
    //       if (pi.PropertyType.IsAssignableTo(typeof(IConfig)))
    //       {
    //         Hint subNode = new Hint(pi.Name);
    //         node.Children.Add(subNode);
    //         scanHintsRec(pi.PropertyType, subNode);
    //       }
    //     }

    //     foreach (PropertyInfo pi in props)
    //     {
    //       if (!pi.PropertyType.IsAssignableTo(typeof(IConfig)))
    //       {
    //         node.Children.Add(new Hint(pi.Name));
    //       }
    //     }
    //   }

    //   scanHintsRec(config.GetType(), root);

    //   return root;
    // }
  }

}