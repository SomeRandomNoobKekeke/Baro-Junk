using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  /// <summary>
  /// All IDebugNodeBase are derived from ClearableEventBase
  /// This could be an abstract class if double inheritance was a thing
  /// </summary>
  public interface IDebugNodeBase // :OhMyPeka:
  {
    public string Name { get; set; }
    public ClearableEvent<DebugEvent> Pin { get; }


    public EventSubscription Map(IDebugNodeBase next, Delegate callback)
      => (this as ClearableEventBase).Map((next as ClearableEventBase), callback);
    public void Unmap(IDebugNodeBase node)
      => (this as ClearableEventBase).Unmap((node as ClearableEventBase));

    public EventSubscription Route(IDebugNodeBase prev, Delegate callback)
      => (this as ClearableEventBase).Route((prev as ClearableEventBase), callback);
    public void Unroute(IDebugNodeBase node)
      => (this as ClearableEventBase).Unroute((node as ClearableEventBase));


    public EventSubscription Map(IDebugNodeBase next)
      => (this as ClearableEventBase).Map((next as ClearableEventBase));
    public EventSubscription Route(IDebugNodeBase prev)
      => (this as ClearableEventBase).Route((prev as ClearableEventBase));


    public void Send() { }
    public void Send(object arg1) { }
    public void Send(object arg1, object arg2) { }
    public void Send(object arg1, object arg2, object arg3) { }
    public void Send(object arg1, object arg2, object arg3, object arg4) { }
    public void Send(object arg1, object arg2, object arg3, object arg4, object arg5) { }
  }

  public interface IDebugNode : IDebugNodeBase
  {
    public void Send()
      => (this as ClearableEvent).Raise();
  }

  public interface IDebugNode<T1> : IDebugNodeBase
  {
    void IDebugNodeBase.Send(object arg1)
      => Send((T1)arg1);
    public void Send(T1 arg1)
      => (this as ClearableEvent<T1>).Raise(arg1);
  }

  public interface IDebugNode<T1, T2> : IDebugNodeBase
  {
    void IDebugNodeBase.Send(object arg1, object arg2)
      => Send((T1)arg1, (T2)arg2);
    public void Send(T1 arg1, T2 arg2)
      => (this as ClearableEvent<T1, T2>).Raise(arg1, arg2);
  }

  public interface IDebugNode<T1, T2, T3> : IDebugNodeBase
  {
    void IDebugNodeBase.Send(object arg1, object arg2, object arg3)
      => Send((T1)arg1, (T2)arg2, (T3)arg3);
    public void Send(T1 arg1, T2 arg2, T3 arg3)
      => (this as ClearableEvent<T1, T2, T3>).Raise(arg1, arg2, arg3);
  }

  public interface IDebugNode<T1, T2, T3, T4> : IDebugNodeBase
  {
    void IDebugNodeBase.Send(object arg1, object arg2, object arg3, object arg4)
      => Send((T1)arg1, (T2)arg2, (T3)arg3, (T4)arg4);
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
      => (this as ClearableEvent<T1, T2, T3, T4>).Raise(arg1, arg2, arg3, arg4);
  }

  public interface IDebugNode<T1, T2, T3, T4, T5> : IDebugNodeBase
  {
    void IDebugNodeBase.Send(object arg1, object arg2, object arg3, object arg4, object arg5)
      => Send((T1)arg1, (T2)arg2, (T3)arg3, (T4)arg4, (T5)arg5);
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
      => (this as ClearableEvent<T1, T2, T3, T4, T5>).Raise(arg1, arg2, arg3, arg4, arg5);
  }
}