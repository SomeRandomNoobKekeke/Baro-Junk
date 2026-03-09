using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using BaroJunk;

namespace BaroJunk.ComponentModules
{
  public partial class CodeAnalyzer
  {
    public static IEnumerable<PartInfo> GetParts(Type componentType)
    {
      bool IsPart(PropertyInfo pi)
      {
        return pi.PropertyType.IsAssignableTo(typeof(IPart));
      }

      IEnumerable<PartInfo> GetPartsRec(Type T, IEnumerable<PropertyInfo> path)
      {
        foreach (PropertyInfo pi in T.GetProperties(Pls))
        {
          if (IsPart(pi))
          {
            IEnumerable<PropertyInfo> nextPath = path.Append(pi);

            yield return new PartInfo(componentType, pi.PropertyType, nextPath);

            foreach (PartInfo info in GetPartsRec(pi.PropertyType, nextPath))
            {
              yield return info;
            }
          }
        }
      }

      foreach (PartInfo info in GetPartsRec(componentType, new List<PropertyInfo>()))
      {
        yield return info;
      }
    }
  }
}