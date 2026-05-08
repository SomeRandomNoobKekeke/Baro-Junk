using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public abstract class DebugNode
  {
    public static int MaxID { get; private set; } = 0;
    public int ID { get; }

    public string Name { get; set; }

    public ClearableEvent<DebugEvent> Pin { get; } = new();


    protected abstract IEventBridge PinBridge { get; }
    protected abstract EventSubscription AddToEvent(Delegate callback);

    private Dictionary<DebugNode, EventSubscription> Mapping = new();
    public EventSubscription Map(DebugNode next, Delegate callback)
    {
      EventSubscription subscription = AddToEvent(callback);
      Mapping[next] = subscription;
      return subscription;
    }
    public void Unmap(DebugNode node)
    {
      Mapping[node].Cancel();
      Mapping.Remove(node);
    }



    public EventSubscription Route(DebugNode prev, Delegate callback) => prev.Map(this, callback);
    public void Unroute(DebugNode node) => node.Unmap(this);

    // Default direct arg mapping
    public abstract EventSubscription Map(DebugNode next);
    public EventSubscription Route(DebugNode prev) => prev.Map(this);


    protected void SetupPin()
    {
      Pin.OnSubscribed += (_) =>
      {
        if (!Pin.Empty && !PinBridge.Opened) PinBridge.Open();
      };

      Pin.OnUnSubscribed += (_) =>
      {
        if (Pin.Empty && PinBridge.Opened) PinBridge.Close();
      };
    }

    public DebugNode()
    {
      ID = MaxID++;
      SetupPin();
    }

    public override int GetHashCode() => ID;

    public override bool Equals(object obj)
    {
      if (obj is not DebugNode other) return false;
      return other.ID == ID;
    }
  }
}