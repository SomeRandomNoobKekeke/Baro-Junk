using System;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public partial class ComponentTest
  {

  public partial class Component
  {
    public String Value
    {
      get { return Props.ModuleA.Value; }
    }
    public void SayBruh(String args = "123")
    {
      Props.ModuleA.SayBruh(args);
    }
  }
  }
}
