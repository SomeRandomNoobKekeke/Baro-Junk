using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class ListProxyTest : UTestPack
  {
    public interface IA
    {
      public int Prop1 { get; set; }
    }
    public class A : IA
    {
      public int Prop1 { get; set; }
      public int Prop2 { get; set; }
    }

    public class B : A
    {
      public int Prop3 { get; set; }
    }

    public class C : A
    {
      public int Prop4 { get; set; }
    }


    public List<UTest> MatchTest()
    {
      List<A> source = Enumerable.Range(0, 10).Select(i => new A() { Prop1 = i }).ToList();

      IReadOnlyList<IA> ints = new ListProxy<IA>(source);

      return new List<UTest>(){
        new UTest(ints[5] is IA,true)
      };
    }


  }
}