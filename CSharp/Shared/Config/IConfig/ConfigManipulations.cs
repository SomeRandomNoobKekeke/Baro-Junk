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
    public bool Equals(IConfig other) => IsEqual(this, other);
    public static bool IsEqual(IConfig configA, IConfig configB)
      => Compare(configA, configB).Equals;


    public ConfigCompareResult CompareTo(IConfig other) => Compare(this, other);
    public static ConfigCompareResult Compare(IConfig configA, IConfig configB)
      => new ConfigCompareResult(configA, configB);

    public void Clear()
    {
      foreach (ConfigEntry entry in this.GetAllEntriesRec())
      {
        entry.Value = Parser.DefaultFor(entry.Type);
      }
    }

    //TODO best name ever
    public void Normalize()
    {
      foreach (ConfigEntry entry in this.GetAllEntriesRec())
      {
        if (entry.IsConfig && entry.Value is null)
        {
          entry.Value = Activator.CreateInstance(entry.Type);
        }
      }
    }


    public IConfig Copy()
    {
      IConfig copy = (IConfig)Activator.CreateInstance(this.GetType());

      copy.Normalize();

      foreach (var (key, entry) in this.GetAllFlat())
      {
        copy.Get(key).Value = entry.Value;
      }

      return copy;
    }
  }

}