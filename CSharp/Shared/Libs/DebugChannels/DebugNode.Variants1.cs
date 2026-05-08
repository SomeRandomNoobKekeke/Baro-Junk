using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugNode<T1> : DebugNode
  {

    public Func<T1, string> DebugMessageFactory
    {
      set
      {
        RealPinBridge.Action = (arg1) => Pin.Raise(
          new DebugEvent()
          {
            Args = new object[] { arg1 },
            Msg = value.Invoke(arg1),
          }
        );
      }
    }

    public ClearableEvent<T1> Event { get; } = new();
    public void Send(T1 arg1) => Event.Raise(arg1);

    public override EventSubscription Map(DebugNode next) => Map((DebugNode<T1>)next);
    public EventSubscription Map(DebugNode<T1> next) => Map(next, DefaultMapping(next));

    public Action<T1> DefaultMapping(DebugNode<T1> next)
      => (T1 arg1) => next.Event.Raise(arg1);

    private EventBridge<T1> RealPinBridge;
    protected override IEventBridge PinBridge => RealPinBridge;
    protected override EventSubscription AddToEvent(Delegate callback)
      => Event.Add((Action<T1>)callback);

    public DebugNode() : base()
    {
      RealPinBridge = Event.CreateBridge();
      RealPinBridge.Action = (arg1) => Pin.Raise(
        new DebugEvent() { Args = new object[] { arg1 } }
      );
    }

  }
}