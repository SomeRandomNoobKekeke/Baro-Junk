using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public class ConfigMixin
  {
    public object RawTarget { get; private set; }
    public IConfiglike Host { get; private set; }

    public DirectEntryLocator Locator { get; }


    public ConfigMixin(object target)
    {
      RawTarget = target;
      Host = ConfiglikeWrapper.Wrap(target);

      Locator = new DirectEntryLocator(Host);

    }
  }
}