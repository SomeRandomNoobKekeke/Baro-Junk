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
    public class MultiplayerClientsideTest : ConfigStrategyTest
    {
      public override void CreateTests()
      {
        Prepare();

        client1IOFacade.Storage["ModSettings\\Configs\\BaroJunk_ConfigA.xml"] = "";
        client2IOFacade.Storage["ModSettings\\Configs\\BaroJunk_ConfigA.xml"] = "";

        client1NetFacade.ConnectTo(serverNetFacade);
        client2NetFacade.ConnectTo(serverNetFacade);


        serverConfig.UseStrategy(ConfigStrategy.MultiplayerClientside);
        client1Config.UseStrategy(ConfigStrategy.MultiplayerClientside);
        client2Config.UseStrategy(ConfigStrategy.MultiplayerClientside);

        client1Config.ReactiveGetEntry("NestedConfigB.IntProp").Value = 123;

        client1HooksFacade.CallHook("roundend");
        client2HooksFacade.CallHook("roundend");
        serverHooksFacade.CallHook("roundend");

        client1HooksFacade.CallHook("stop");
        client2HooksFacade.CallHook("stop");
        serverHooksFacade.CallHook("stop");

        Tests.Add(new UListTest(WhatHappened, new List<string>()
        {

        }));
      }
    }
  }

}