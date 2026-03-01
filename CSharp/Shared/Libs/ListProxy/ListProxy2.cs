using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public class ListProxy<TSource, TResult> : IReadOnlyList<TResult>
  {
    public struct ProxyEnumerator : IEnumerator<TResult>, IEnumerator
    {
      private IEnumerator<TSource> Enumerator;
      private Func<TSource, TResult> Transform;
      public ProxyEnumerator(IEnumerator<TSource> enumerator, Func<TSource, TResult> transform)
      {
        Enumerator = enumerator;
        Transform = transform;
      }

      public bool MoveNext() => Enumerator.MoveNext();
      public TResult Current => Transform(Enumerator.Current);
      object? IEnumerator.Current => Transform(Enumerator.Current);
      void IEnumerator.Reset() => Enumerator.Reset();
      public void Dispose() { }
    }


    public TResult this[int i]
    {
      get => Transform(Source[i]);
    }
    public int Count => Source.Count;

    public ProxyEnumerator GetEnumerator() => new ProxyEnumerator(Source.GetEnumerator(), Transform);

    IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TResult>)this).GetEnumerator();


    private IList<TSource> Source;
    private Func<TSource, TResult> Transform;
    public ListProxy(IList<TSource> source, Func<TSource, TResult> transform)
    {
      ArgumentNullException.ThrowIfNull(source);
      ArgumentNullException.ThrowIfNull(transform);

      Source = source;
      Transform = transform;
    }
  }

  // public static class IListTExtensions
  // {
  //   public static ListProxy<TSource, TResult> As<TSource, TResult>(this IList<TSource> list) => new ListProxy<T>(list);
  // }
}