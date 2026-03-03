using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace BaroJunk
{
  public partial class ComponentModulesTest : UTestPack
  {
    public partial class DirectMethodAccessorTest : ComponentModulesTest
    {

      // Direct generated accessor
      public partial class Component
      {
        public void Bruh() => PrintBruh();
      }


      public partial class Component
      {
        protected void PrintBruh() { }
      }



      public override void CreateTests()
      {

      }
    }
  }
}