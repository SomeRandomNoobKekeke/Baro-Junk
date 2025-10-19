using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public partial class ConfigCore : IConfigLikeContainer, IDirectlyLocatable, IReactiveLocatable
  {
    public object RawTarget { get; }
    public IConfiglike Host { get; }
    public ReactiveCore ReactiveCore { get; }
    public string ID => Host.ID;

    public DirectEntryLocator Locator { get; }
    public ReactiveEntryLocator ReactiveLocator { get; }
    public ConfigManager Manager { get; }

    public SimpleParser Parser { get; set; }
    public NetParser NetParser { get; set; }
    public XMLParser XMLParser { get; set; }
    public Logger Logger { get; set; }

    public IConfigFacades Facades { get; set; }
    public ConfigSettings Settings { get; set; }



    public ConfigCore(object target)
    {
      ArgumentNullException.ThrowIfNull(target);

      RawTarget = target;

      //THINK i don't like that i don't have any control over IConfiglike type
      Host = ConfiglikeWrapper.Wrap(target);

      Locator = new DirectEntryLocator(this);
      ReactiveCore = new ReactiveCore(Host);
      ReactiveLocator = ReactiveCore.Locator;

      Parser = new SimpleParser();
      NetParser = new NetParser(Parser);
      XMLParser = new XMLParser(Parser);
      Logger = new Logger();

      Facades = new ConfigFacades();
      Manager = new ConfigManager(this);
    }
  }
}