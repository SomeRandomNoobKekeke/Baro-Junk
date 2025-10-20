using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace BaroJunk
{
  public class ConfigCommandsManagerTest : ConfigManagersTest
  {
    public UTest ShouldAddCommmandWhenYouSetName()
    {
      ExampleConfigs.ConfigA config = new();
      FakeConsoleFacade ConsoleFacade = new FakeConsoleFacade();

      config.Self().Facades.ConsoleFacade = ConsoleFacade;

      config.Settings().CommandName = "bruh";

      return new UTest(ConsoleFacade.Commands.First()?.Names[0], "bruh");
    }

    public UTest CommandShouldBeAbleToChangeConfig()
    {
      ExampleConfigs.ConfigA config = new();
      FakeConsoleFacade ConsoleFacade = new FakeConsoleFacade();

      config.Self().Facades.ConsoleFacade = ConsoleFacade;

      config.Settings().CommandName = "bruh";

      ConsoleFacade.Execute("bruh NestedConfigB.StringProp bebebe");

      return new UTest(config.NestedConfigB.StringProp, "bebebe");
    }

    public UTest CommandShouldPropChanged()
    {
      ExampleConfigs.ConfigA config = new();
      FakeConsoleFacade ConsoleFacade = new FakeConsoleFacade();

      config.Self().Facades.ConsoleFacade = ConsoleFacade;

      config.Settings().CommandName = "bruh";

      bool updated = false;
      config.OnPropChanged((s, o) => updated = true);

      ConsoleFacade.Execute("bruh NestedConfigB.StringProp bebebe");

      return new UTest(updated, true, "changing prop via command should trigger PropChanged");
    }


  }
}