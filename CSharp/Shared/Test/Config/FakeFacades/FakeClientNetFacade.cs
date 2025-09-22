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

  public class FakeClientNetFacade : INetFacade
  {
    public bool HasPermissions = true;
    public bool IsMultiplayer => true;
    public bool IsClient => true;

    public FakeServerNetFacade Server;
    public Dictionary<string, Action<IReadMessage>> RecieveCallbacks = new();

    public void Recieve(string header, IReadMessage msg)
    {
      MessageRecieved?.Invoke(header, msg);

      if (RecieveCallbacks.ContainsKey(header))
      {
        RecieveCallbacks[header]?.Invoke(msg);
      }
    }

    public event Action<string, IWriteMessage> MessageSent;
    public event Action<string, IReadMessage> MessageRecieved;

    public bool DoIHavePermissions() => HasPermissions;

    public void ClientSend(string header)
    {
      FakeReadWriteMessage outMsg = new FakeReadWriteMessage();
      MessageSent?.Invoke(header, outMsg);
      Server?.Recieve(header, outMsg, this);
    }

    public void ClientEncondeAndSend(string header, IConfig config)
    {
      FakeReadWriteMessage outMsg = new FakeReadWriteMessage();
      config.NetEncode(outMsg);
      MessageSent?.Invoke(header, outMsg);
      Server?.Recieve(header, outMsg, this);
    }

    public void ListenForServer(string header, Action<IReadMessage> callback)
    {
      RecieveCallbacks[header] = callback;
    }

    public bool DoesClientHasPermissions(Client client) => false;
    public void ServerSend(string header, Client client) { }
    public void ServerEncondeAndSend(string header, IConfig config, Client client) { }
    public void ServerEncondeAndBroadcast(string header, IConfig config) { }
    public void ListenForClients(string header, Action<IReadMessage, Client> callback) { }

  }



}
