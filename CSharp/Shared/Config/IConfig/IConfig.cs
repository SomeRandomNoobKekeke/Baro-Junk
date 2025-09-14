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

  public partial interface IConfig
  {
    private static IConfig _current;
    public static IConfig Current
    {
      get => _current;
      set
      {
        _current = value;
      }
    }

    public ConfigMixin Mixin => ConfigMixin.Mixins.GetValue(this, c => new ConfigMixin(c));
    public string Name => Mixin.Bruh;


  }



}