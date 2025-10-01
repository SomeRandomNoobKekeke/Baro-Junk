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

        client1NetFacade.ConnectTo(serverNetFacade);
        serverConfig.UseStrategy(ConfigStrategy.MultiplayerClientside);
        client1Config.UseStrategy(ConfigStrategy.MultiplayerClientside);

        client2NetFacade.ConnectTo(serverNetFacade);
        client2Config.UseStrategy(ConfigStrategy.MultiplayerClientside);


        Tests.Add(new UListTest(WhatHappened, new List<string>(){
          "server sent BaroJunk_ConfigA_sync msg to client1",
          "client1 sent BaroJunk_ConfigA_ask msg to server",
          "server sent BaroJunk_ConfigA_sync msg to client1",
          "client2 sent BaroJunk_ConfigA_ask msg to server",
          "server sent BaroJunk_ConfigA_sync msg to client2",
        }));
      }
    }
  }

}