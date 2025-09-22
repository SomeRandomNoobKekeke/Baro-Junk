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
    public class OrderTest : ConfigClientNetManagerTest
    {
      public record SentMessage(string Header, object target);

      public override void CreateTests()
      {
        FakeClientNetFacade clientNetFacade = new FakeClientNetFacade();
        FakeServerNetFacade serverNetFacade = new FakeServerNetFacade();
        ExampleConfigs.ConfigA clientConfig = new ExampleConfigs.ConfigA();
        ExampleConfigs.ConfigA serverConfig = new ExampleConfigs.ConfigA();

        clientConfig.Self().Facades.NetFacade = clientNetFacade;
        serverConfig.Self().Facades.NetFacade = serverNetFacade;

        serverNetFacade.Connect(clientNetFacade);

        List<SentMessage> SentMessages = new();

        string lastSentMessage = null;
        clientNetFacade.MessageSent += (header, msg) => SentMessages.Add(new SentMessage(header, clientNetFacade.Server));
        serverNetFacade.MessageSent += (header, msg, target) => SentMessages.Add(new SentMessage(header, target));


        serverConfig.Settings().NetSync = true;
        clientConfig.Settings().NetSync = true;

        Tests.Add(new UListTest(SentMessages, new List<SentMessage>(){
          new SentMessage($"{clientConfig.Self().ID}_ask",serverNetFacade),
          new SentMessage($"{clientConfig.Self().ID}_sync",clientNetFacade),
        }));
      }

    }

    public override void CreateTests()
    {

    }

  }
}