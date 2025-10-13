using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public class ConfiglikeWrapper
  {
    public static IConfiglike Wrap(object o)
    {
      return new ConfiglikeObject(o);
    }
  }
}