using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class ReadOnlyListAdapterTest : ListAdapterTest
  {
    public List<UTest> ChainedAdapter()
    {
      List<B> listB = new(){
        new B(){Prop1 = 321}, new B(){Prop1 = 123}
      };

      var adapter = new ReadOnlyListAdapter<A>(listB);

      var adapter2 = new ReadOnlyListAdapter<ListAdapterTest>(listB);
      var adapter3 = new ReadOnlyListAdapter<int>(adapter2);
      var adapter4 = new ReadOnlyListAdapter<System.Net.Http.HttpClient>(adapter3);
      var adapter5 = new ReadOnlyListAdapter<A>(adapter4);

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