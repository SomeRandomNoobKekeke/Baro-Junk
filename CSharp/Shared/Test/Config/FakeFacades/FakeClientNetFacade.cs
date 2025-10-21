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
    public string Name { get; set; } = "FakeClientNetFacade";
    public override string ToString() => Name;
    public bool HasPermissions = true;
    public bool IsMultiplayer => true;
    public bool IsClient => true;

    public HashSet<string> AlreadyListeningFor { get; } = new HashSet<string>();
    public string DontHavePermissionsString => "You need to be the host or have ConsoleCommands permission to use it";

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

    public void ConnectTo(INetFacade server)
    {
      if (server is not FakeServerNetFacade svf) return;
      svf.Connect(this);
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

    public void ClientEncondeAndSend(string header, ConfigCore config)
    {
      FakeReadWriteMessage outMsg = new FakeReadWriteMessage();
      config.NetEncode(outMsg);
      MessageSent?.Invoke(header, outMsg);
      Server?.Recieve(header, outMsg, this);
    }

    public void ListenForServer(string header, Action<IReadMessage> callback)
    {
      AlreadyListeningFor.Add(header);
      RecieveCallbacks[header] = callback;
    }

    public bool DoesClientHasPermissions(Client client) => false;
    public void ServerSend(string header, Client client) { }
    public void ServerEncondeAndSend(string header, ConfigCore config, Client client) { }
    public void ServerEncondeAndBroadcast(string header, ConfigCore config) { }
    public void ListenForClients(string header, Action<IReadMessage, Client> callback) { }

  }



}
