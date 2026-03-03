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
    public partial class NestedAccessorTest : ComponentModulesTest
    {

      public partial class Component
      {
        //generated wrapper
        public class PropsWrapper
        {
          public string Prop
          {
            get => Host.PropContainer.Prop;
            set => Host.PropContainer.Prop = value;
          }

          private Component Host;
          public PropsWrapper(Component host) => Host = host;
        }
      }


      public partial class Component
      {
        //Props is accessed as Props.Prop
        public PropsWrapper Props { get; private set; }


        //generated method
        private void CreateAccessors()
        {
          Props = new PropsWrapper(this);
        }
      }

      public class PropContainer
      {
        public string Prop { get; set; } = "123";
      }

      public partial class Component
      {
        protected PropContainer PropContainer { get; } = new();
      }



      public override void CreateTests()
      {
        Component component = new Component();
        Logger.Default.Log(component.Props.Prop);
      }
    }
  }
}