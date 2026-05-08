using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1, T2, T3> : DebugNode
  {
    public Func<T1, T2, T3, string> DebugMessageFactory
    {
      set
      {
        RealPinBridge.Action = (arg1, arg2, arg3) => Pin.Raise(
          new DebugEvent()
          {
            Args = new object[] { arg1, arg2, arg3 },
            Msg = value.Invoke(arg1, arg2, arg3),
          }
        );
      }
    }

    public ClearableEvent<T1, T2, T3> Event { get; } = new();
    public void Send(T1 arg1, T2 arg2, T3 arg3) => Event.Raise(arg1, arg2, arg3);

    public override EventSubscription Map(DebugNode next) => Map((DebugNode<T1, T2, T3>)next);
    public EventSubscription Map(DebugNode<T1, T2, T3> next) => Map(next, DefaultMapping(next));

    public Action<T1, T2, T3> DefaultMapping(DebugNode<T1, T2, T3> next)
      => (T1 arg1, T2 arg2, T3 arg3) => next.Event.Raise(arg1, arg2, arg3);

    private EventBridge<T1, T2, T3> RealPinBridge;
    protected override IEventBridge PinBridge => RealPinBridge;
    protected override EventSubscription AddToEvent(Delegate callback)
      => Event.Add((Action<T1, T2, T3>)callback);

    public DebugNode() : base()
    {
      RealPinBridge = Event.CreateBridge();
      RealPinBridge.Action = (arg1, arg2, arg3) => Pin.Raise(
       new DebugEvent() { Args = new object[] { arg1, arg2, arg3 } }
      );
    }
  }
}