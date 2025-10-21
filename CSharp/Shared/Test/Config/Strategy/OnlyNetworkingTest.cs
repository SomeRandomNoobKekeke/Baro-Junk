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
    public class OnlyNetworkingTest : ConfigStrategyTest
    {
      public override void CreateTests()
      {
        Prepare();

        client1NetFacade.ConnectTo(serverNetFacade);
        client2NetFacade.ConnectTo(serverNetFacade);

        serverConfig.UseStrategy(ConfigStrategy.OnlyNetworking);
        client1Config.UseStrategy(ConfigStrategy.OnlyNetworking);
        client2Config.UseStrategy(ConfigStrategy.OnlyNetworking);

        client1Config.ReactiveGetEntry("NestedConfigB.IntProp").Value = 123;
        HooksFacade.CallHook("stop");


        Tests.Add(new UListTest(WhatHappened, new List<string>(){
          "server sent BaroJunk_ConfigA_sync msg to client1",
          "server sent BaroJunk_ConfigA_sync msg to client2",
          "client1 sent BaroJunk_ConfigA_ask msg to server",
          "server sent BaroJunk_ConfigA_sync msg to client1",
          "client1Config updated",
          "client2 sent BaroJunk_ConfigA_ask msg to server",
          "server sent BaroJunk_ConfigA_sync msg to client2",
          "client2Config updated",
          "client1Config prop [NestedConfigB.IntProp] changed to [123]",
          "client1 sent BaroJunk_ConfigA_sync msg to server",
          "serverConfig updated",
          "server sent BaroJunk_ConfigA_sync msg to client1",
          "client1Config updated",
          "server sent BaroJunk_ConfigA_sync msg to client2",
          "client2Config updated"
        }));
      }
    }
  }
}