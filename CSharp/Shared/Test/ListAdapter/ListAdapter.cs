using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class ListAdapterTest : UTestPack
  {
    public class A
    {
      public int Prop1 { get; set; }
    }

    public class B : A
    {
      public int Prop2 { get; set; }
    }

    public class C : A
    {
      public int Prop3 { get; set; }
    }

    public List<UTest> Set()
    {
      List<B> listB = new(){
        new B(){Prop1 = 321}, new B(){Prop1 = 123}
      };

      ListAdapter<A> adapter = new(listB);
      ListAdapter<B> adapter2 = new(listB);


      adapter.Add(new B() { Prop2 = 444 });

      return new List<UTest>()
      {
        new UTest(listB[2].Prop2, 444),
        new UTest(adapter2[2].Prop2, 444),
      };
    }

    public List<UTest> InvalidSet()
    {
      List<B> listB = new(){
        new B(){Prop1 = 321}, new B(){Prop1 = 123}
      };

      ListAdapter<A> adapter = new(listB);
      ListAdapter<B> adapter2 = new(listB);

      Exception exception = null;
      try
      {
        adapter.Add(new C() { Prop3 = 404 });
      }
      catch (Exception e)
      {
        exception = e;
      }

      return new List<UTest>()
      {
        new UTest(exception is ArgumentException, true),
      };
    }

    public List<UTest> ChainedAdapter()
    {
      List<B> listB = new(){
        new B(){Prop1 = 321}, new B(){Prop1 = 123}
      };

      ListAdapter<A> adapter = new ListAdapter<A>(listB);

      var adapter2 = new ListAdapter<ListAdapterTest>(listB);
      var adapter3 = new ListAdapter<int>(adapter2);
      var adapter4 = new ListAdapter<System.Net.Http.HttpClient>(adapter3);
      var adapter5 = new ListAdapter<A>(adapter4);

      return new List<UTest>()
      {
        new UTest(adapter, adapter5),
        new UTest(adapter[1].Prop1, 123),
        new UTest(adapter5[1].Prop1, 123),
        new UTest(adapter[1].Prop1, adapter5[1].Prop1),
      };
    }
  }
}