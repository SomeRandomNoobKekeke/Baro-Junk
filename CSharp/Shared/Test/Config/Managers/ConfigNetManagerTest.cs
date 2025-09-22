using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace BaroJunk
{
  public class ConfigClientNetManagerTest : ConfigManagersTest
  {
    public class GreetingTest : ConfigClientNetManagerTest
    {
      public record SentMessage(string Header, object target);

      public override void CreateTests()
      {
        FakeServerNetFacade serverNetFacade = new FakeServerNetFacade() { Name = "server" };
        FakeClientNetFacade client1NetFacade = new FakeClientNetFacade() { Name = "client1" };
        FakeClientNetFacade client2NetFacade = new FakeClientNetFacade() { Name = "client2" };

        ExampleConfigs.ConfigA client1Config = new ExampleConfigs.ConfigA();
        ExampleConfigs.ConfigA client2Config = new ExampleConfigs.ConfigA();
        ExampleConfigs.ConfigA serverConfig = new ExampleConfigs.ConfigA();

        client1Config.Self().Facades.NetFacade = client1NetFacade;
        client2Config.Self().Facades.NetFacade = client2NetFacade;
        serverConfig.Self().Facades.NetFacade = serverNetFacade;

        List<SentMessage> SentMessages = new();

        client1NetFacade.MessageSent += (header, msg) => SentMessages.Add(new SentMessage(header, client1NetFacade.Server));
        client2NetFacade.MessageSent += (header, msg) => SentMessages.Add(new SentMessage(header, client2NetFacade.Server));
        serverNetFacade.MessageSent += (header, msg, target) => SentMessages.Add(new SentMessage(header, target));


        serverNetFacade.Connect(client1NetFacade);
        serverConfig.Settings().NetSync = true; // server is on -> broadcasting settings
        client1Config.Settings().NetSync = true; // client is on -> ask for server settings
        serverNetFacade.Connect(client2NetFacade);
        client2Config.Settings().NetSync = true; // client is on -> ask for server settings


        Tests.Add(new UListTest(SentMessages, new List<SentMessage>(){
          new SentMessage($"{IConfig.TypeID<ExampleConfigs.ConfigA>()}_sync", client1NetFacade ),
          new SentMessage($"{IConfig.TypeID<ExampleConfigs.ConfigA>()}_ask", serverNetFacade),
          new SentMessage($"{IConfig.TypeID<ExampleConfigs.ConfigA>()}_sync", client1NetFacade),
          new SentMessage($"{IConfig.TypeID<ExampleConfigs.ConfigA>()}_ask", serverNetFacade),
          new SentMessage($"{IConfig.TypeID<ExampleConfigs.ConfigA>()}_sync", client2NetFacade),
        }));
      }

    }

    public override void CreateTests()
    {

    }

  }
}