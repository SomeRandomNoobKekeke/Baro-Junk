using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1, T2, T3, T4, T5> : DebugNode
  {
    public Func<T1, T2, T3, T4, T5, string> DebugMessageFactory
    {
      set
      {
        RealPinBridge.Action = (arg1, arg2, arg3, arg4, arg5) => Pin.Raise(
          new DebugEvent()
          {
            Args = new object[] { arg1, arg2, arg3, arg4, arg5 },
            Msg = value.Invoke(arg1, arg2, arg3, arg4, arg5),
          }
        );
      }
    }

    public ClearableEvent<T1, T2, T3, T4, T5> Event { get; } = new();
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => Event.Raise(arg1, arg2, arg3, arg4, arg5);

    public override EventSubscription Map(DebugNode next) => Map((DebugNode<T1, T2, T3, T4, T5>)next);
    public EventSubscription Map(DebugNode<T1, T2, T3, T4, T5> next) => Map(next, DefaultMapping(next));

    public Action<T1, T2, T3, T4, T5> DefaultMapping(DebugNode<T1, T2, T3, T4, T5> next)
      => (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => next.Event.Raise(arg1, arg2, arg3, arg4, arg5);

    private EventBridge<T1, T2, T3, T4, T5> RealPinBridge;
    protected override IEventBridge PinBridge => RealPinBridge;
    protected override EventSubscription AddToEvent(Delegate callback)
      => Event.Add((Action<T1, T2, T3, T4, T5>)callback);

    public DebugNode() : base()
    {
      RealPinBridge = Event.CreateBridge();
      RealPinBridge.Action = (arg1, arg2, arg3, arg4, arg5) => Pin.Raise(
       new DebugEvent() { Args = new object[] { arg1, arg2, arg3, arg4, arg5 } }
      );
    }
  }
}