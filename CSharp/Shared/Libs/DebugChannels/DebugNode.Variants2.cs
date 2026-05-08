using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1, T2> : DebugNode
  {
    public Func<T1, T2, string> DebugMessageFactory
    {
      set
      {
        RealPinBridge.Action = (arg1, arg2) => Pin.Raise(
          new DebugEvent()
          {
            Args = new object[] { arg1, arg2 },
            Msg = value.Invoke(arg1, arg2),
          }
        );
      }
    }

    public ClearableEvent<T1, T2> Event { get; } = new();
    public void Send(T1 arg1, T2 arg2) => Event.Raise(arg1, arg2);

    public override EventSubscription Map(DebugNode next) => Map((DebugNode<T1, T2>)next);
    public EventSubscription Map(DebugNode<T1, T2> next) => Map(next, DefaultMapping(next));

    public Action<T1, T2> DefaultMapping(DebugNode<T1, T2> next)
      => (T1 arg1, T2 arg2) => next.Event.Raise(arg1, arg2);

    private EventBridge<T1, T2> RealPinBridge;
    protected override IEventBridge PinBridge => RealPinBridge;
    protected override EventSubscription AddToEvent(Delegate callback)
      => Event.Add((Action<T1, T2>)callback);

    public DebugNode() : base()
    {
      RealPinBridge = Event.CreateBridge();
      RealPinBridge.Action = (arg1, arg2) => Pin.Raise(
       new DebugEvent() { Args = new object[] { arg1, arg2 } }
      );
    }
  }
}