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
    public partial class DirectAccessorTest : ComponentModulesTest
    {

      // Direct generated accessor
      public partial class Component
      {
        public string Prop
        {
          get => PropContainer.Prop;
          set => PropContainer.Prop = value;
        }
      }

      public class PropContainer
      {
        public string Prop { get; set; }
      }

      public partial class Component
      {
        protected PropContainer PropContainer { get; } = new();
      }



      public override void CreateTests()
      {
        Component component = new();



      }
    }
  }
}