using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public class ConfigMixin : IConfiglikeProvider
  {
    public object RawTarget { get; private set; }
    public IConfiglike Target { get; private set; }


    public ConfigMixin(object target)
    {
      RawTarget = target;
      Target = ConfiglikeWrapper.Wrap(target);

    }
  }
}