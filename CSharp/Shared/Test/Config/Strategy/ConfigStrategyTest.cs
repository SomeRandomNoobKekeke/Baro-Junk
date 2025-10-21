using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

using BaroJunk_Config;

namespace BaroJunk
{
  public partial class ConfigStrategyTest : ConfigManagersTest
  {
    public ExampleConfigs.ConfigA serverConfig;
    public ExampleConfigs.ConfigA client1Config;
    public ExampleConfigs.ConfigA client2Config;

    public FakeServerNetFacade serverNetFacade;
    public FakeClientNetFacade client1NetFacade;
    public FakeClientNetFacade client2NetFacade;
    public FakeIOFacade serverIOFacade;
    public FakeIOFacade client1IOFacade;
    public FakeIOFacade client2IOFacade;

    public FakeHooksFacade serverHooksFacade;
    public FakeHooksFacade client1HooksFacade;
    public FakeHooksFacade client2HooksFacade;

    public List<string> WhatHappened = new();

    public void Prepare()
    {
      WhatHappened = new();

      client1Config = new ExampleConfigs.ConfigA();
      client2Config = new ExampleConfigs.ConfigA();
      serverConfig = new ExampleConfigs.ConfigA();

      client1NetFacade = new FakeClientNetFacade() { Name = "client1" };
      client2NetFacade = new FakeClientNetFacade() { Name = "client2" };
      serverNetFacade = new FakeServerNetFacade() { Name = "server" };


      client1IOFacade = new FakeIOFacade();
      client2IOFacade = new FakeIOFacade();
      serverIOFacade = new FakeIOFacade();

      client1HooksFacade = new FakeHooksFacade();
      client2HooksFacade = new FakeHooksFacade();
      serverHooksFacade = new FakeHooksFacade();

      client1Config.Self().Facades.NetFacade = client1NetFacade;
      client2Config.Self().Facades.NetFacade = client2NetFacade;
      serverConfig.Self().Facades.NetFacade = serverNetFacade;

      client1Config.Self().Facades.IOFacade = client1IOFacade;
      client2Config.Self().Facades.IOFacade = client2IOFacade;
      serverConfig.Self().Facades.IOFacade = serverIOFacade;

      client1Config.Self().Facades.HooksFacade = client1HooksFacade;
      client2Config.Self().Facades.HooksFacade = client2HooksFacade;
      serverConfig.Self().Facades.HooksFacade = serverHooksFacade;


      client1NetFacade.MessageSent += (header, msg) => WhatHappened.Add($"client1 sent {header} msg to server");
      client2NetFacade.MessageSent += (header, msg) => WhatHappened.Add($"client2 sent {header} msg to server");
      serverNetFacade.MessageSent += (header, msg, target) => WhatHappened.Add($"server sent {header} msg to {target.Name}");

      client1Config.OnUpdated(() => WhatHappened.Add($"client1Config updated"));
      client2Config.OnUpdated(() => WhatHappened.Add($"client2Config updated"));
      serverConfig.OnUpdated(() => WhatHappened.Add($"serverConfig updated"));

      client1Config.OnPropChanged((name, value) => WhatHappened.Add($"client1Config prop [{name}] changed to [{value}]"));
      client2Config.OnPropChanged((name, value) => WhatHappened.Add($"client2Config prop [{name}] changed to [{value}]"));
      serverConfig.OnPropChanged((name, value) => WhatHappened.Add($"serverConfig prop [{name}] changed to [{value}]"));

      client1IOFacade.SomethingHappened += (what) => WhatHappened.Add($"client1 {what}");
      client2IOFacade.SomethingHappened += (what) => WhatHappened.Add($"client2 {what}");
      serverIOFacade.SomethingHappened += (what) => WhatHappened.Add($"server {what}");
    }

  }
}