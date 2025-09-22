using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Barotrauma.Networking;

namespace BaroJunk
{

  public class FakeServerNetFacade : INetFacade
  {
    public string Name { get; set; } = "FakeServerNetFacade";
    public override string ToString() => Name;
    public bool IsMultiplayer => true;
    public bool IsClient => false;
    public Dictionary<string, Action<IReadMessage, Client>> RecieveCallbacks = new();

    public void Recieve(string header, IReadMessage msg, FakeClientNetFacade client)
    {
      LastClient = client;
      MessageRecieved?.Invoke(header, msg, client);

      if (RecieveCallbacks.ContainsKey(header))
      {
        RecieveCallbacks[header]?.Invoke(msg, null);
      }
    }

    public event Action<string, IWriteMessage, FakeClientNetFacade> MessageSent;
    public event Action<string, IReadMessage, FakeClientNetFacade> MessageRecieved;


    public List<FakeClientNetFacade> Clients = new();
    public FakeClientNetFacade LastClient;
    public void Connect(INetFacade client)
    {
      if (client is not FakeClientNetFacade clf) return;
      Clients.Add(clf);
      clf.Server = this;
    }

    public void SelectLastClient(FakeClientNetFacade client) => LastClient = client;

    public bool DoesClientHasPermissions(Client client) => LastClient.HasPermissions;

    public void ServerSend(string header, Client client)
    {
      LastClient?.Recieve(header, new FakeReadWriteMessage());
    }
    public void ServerEncondeAndSend(string header, IConfig config, Client client)
    {
      FakeReadWriteMessage outMsg = new FakeReadWriteMessage();
      config.NetEncode(outMsg);
      MessageSent?.Invoke(header, outMsg, LastClient);
      LastClient?.Recieve(header, outMsg);
    }
    public void ServerEncondeAndBroadcast(string header, IConfig config)
    {
      FakeReadWriteMessage outMsg = new FakeReadWriteMessage();
      config.NetEncode(outMsg);
      foreach (FakeClientNetFacade client in Clients)
      {
        MessageSent?.Invoke(header, outMsg, client);
        client.Recieve(header, outMsg.Copy());
      }
    }

    public void ListenForClients(string header, Action<IReadMessage, Client> callback)
    {
      RecieveCallbacks[header] = callback;
    }

    public bool DoIHavePermissions() => true;
    public void ClientSend(string header) { }
    public void ClientEncondeAndSend(string header, IConfig config) { }
    public void ListenForServer(string header, Action<IReadMessage> callback) { }

  }



}
