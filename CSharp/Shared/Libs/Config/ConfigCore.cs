using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public class ConfigCore : IConfigLikeContainer
  {
    public object RawTarget { get; }
    public IConfiglike Host { get; }
    public ReactiveCore ReactiveCore { get; }

    public DirectEntryLocator Locator { get; }
    public ReactiveEntryLocator ReactiveLocator { get; }


    public ConfigCore(object target)
    {
      RawTarget = target;
      Host = ConfiglikeWrapper.Wrap(target);

      Locator = new DirectEntryLocator(this);
      ReactiveCore = new ReactiveCore(Host);
      ReactiveLocator = ReactiveCore.Locator;
    }
  }
}