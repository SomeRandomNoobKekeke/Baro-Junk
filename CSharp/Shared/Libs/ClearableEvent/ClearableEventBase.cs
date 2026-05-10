using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public abstract class ClearableEventBase
  {
    public static int MaxID { get; private set; } = 0;
    public int ID { get; }

    public ClearableEventBase()
    {
      ID = MaxID++;
    }

    public abstract EventSubscription Add(Delegate callback);
    protected Dictionary<ClearableEventBase, EventSubscription> Mapping = new();
    public EventSubscription Map(ClearableEventBase next, Delegate callback)
    {
      EventSubscription subscription = Add(callback);
      Mapping[next] = subscription;
      return subscription;
    }
    public void Unmap(ClearableEventBase node)
    {
      Mapping[node].Cancel();
      Mapping.Remove(node);
    }

    public EventSubscription Route(ClearableEventBase prev, Delegate callback) => prev.Map(this, callback);
    public void Unroute(ClearableEventBase node) => node.Unmap(this);

    protected abstract Delegate DefaultMapping(ClearableEventBase next);
    public EventSubscription Map(ClearableEventBase next) => Map(next, DefaultMapping(next));
    public EventSubscription Route(ClearableEventBase prev) => prev.Map(this);


    public override int GetHashCode() => ID;

    //TODO do i even need this?
    // public override bool Equals(object obj)
    // {
    //   //TODO test, i'm sure it's not supposed to work like this
    //   if (obj is not ClearableEventBase other) return false;
    //   return other.ID == ID;
    // }
  }
}