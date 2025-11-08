using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;



namespace BaroJunk
{
  public partial class ConfigStrategyTest
  {
    public class PassiveTest : ConfigStrategyTest
    {
      public override void CreateTests()
      {
        Prepare();

        client1NetFacade.ConnectTo(serverNetFacade);
        serverConfig.UseStrategy(ConfigStrategy.Passive);
        client1Config.UseStrategy(ConfigStrategy.Passive);

        client2NetFacade.ConnectTo(serverNetFacade);
        client2Config.UseStrategy(ConfigStrategy.Passive);

        client1Config.ReactiveGetEntry("NestedConfigB.IntProp").Value = 123;

        client1HooksFacade.CallHook("roundend");
        client2HooksFacade.CallHook("roundend");
        serverHooksFacade.CallHook("roundend");

        client1HooksFacade.CallHook("stop");
        client2HooksFacade.CallHook("stop");
        serverHooksFacade.CallHook("stop");


        Tests.Add(new UListTest(WhatHappened, new List<string>() {
          "client1Config prop [NestedConfigB.IntProp] changed to [123]",
        }));
      }
    }
  }
}