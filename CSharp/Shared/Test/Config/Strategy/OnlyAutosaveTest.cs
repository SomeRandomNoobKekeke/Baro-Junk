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
    public class OnlyAutosaveTest : ConfigStrategyTest
    {
      public override void CreateTests()
      {
        Prepare();

        client1NetFacade.ConnectTo(serverNetFacade);
        client2NetFacade.ConnectTo(serverNetFacade);

        client1IOFacade.Storage["ModSettings\\Configs\\BaroJunk_ConfigA.xml"] = "";

        serverConfig.UseStrategy(ConfigStrategy.OnlyAutosave);
        client1Config.UseStrategy(ConfigStrategy.OnlyAutosave);
        client2Config.UseStrategy(ConfigStrategy.OnlyAutosave);

        client1Config.ReactiveGetEntry("NestedConfigB.IntProp").Value = 123;

        client1HooksFacade.CallHook("roundend");
        client2HooksFacade.CallHook("roundend");
        serverHooksFacade.CallHook("roundend");

        client1HooksFacade.CallHook("stop");
        client2HooksFacade.CallHook("stop");
        serverHooksFacade.CallHook("stop");


        Tests.Add(new UListTest(WhatHappened, new List<string>()
        {
          @"client1 xdoc loaded from ModSettings\Configs\BaroJunk_ConfigA.xml",
          @"client1Config prop [NestedConfigB.IntProp] changed to [123]",
          @"client1 xdoc saved to ModSettings\Configs\BaroJunk_ConfigA.xml",
          @"client2 xdoc saved to ModSettings\Configs\BaroJunk_ConfigA.xml",
          @"client1 xdoc saved to ModSettings\Configs\BaroJunk_ConfigA.xml",
          @"client2 xdoc saved to ModSettings\Configs\BaroJunk_ConfigA.xml"
        }));
      }
    }
  }
}