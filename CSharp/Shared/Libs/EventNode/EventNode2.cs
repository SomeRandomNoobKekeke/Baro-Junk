using System;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public class EventNode<T1, T2> : EventNodeBase
  {
    public ClearableEvent<T1, T2> Event { get; } = new();

    public EventNode() { Gates.Add(Event); }

    public Action<EventNode<T1, T2>> Setup { set => value(this); }

    public override void Map(EventNodeBase next)
    {
      ClearableEvent<T1, T2> matchingGate = (ClearableEvent<T1, T2>)next.Gates.Find(e => e is ClearableEvent<T1, T2>);

      if (matchingGate is null)
      {
        throw new Exception($"Next [{next}] doesn't have a gate that matches [{typeof(ClearableEvent<T1, T2>)}]");
      }

      Event.Add((a1, a2) => matchingGate.Raise(a1, a2));
    }

    public void AddGate(ClearableEventBase gate)
    {
      Gates.Add(gate);
    }
  }
}