using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public class ConfigMixin
  {
    public object RawTarget { get; }
    public IConfiglike Host { get; }
    public ReactiveCore ReactiveCore { get; }

    public DirectEntryLocator Locator { get; }
    public ReactiveEntryLocator ReactiveLocator { get; }


    public ConfigMixin(object target)
    {
      RawTarget = target;
      Host = ConfiglikeWrapper.Wrap(target);

      Locator = new DirectEntryLocator(Host);
      ReactiveCore = new ReactiveCore();
      ReactiveLocator = new ReactiveEntryLocator(ReactiveCore, Host);
    }
  }
}