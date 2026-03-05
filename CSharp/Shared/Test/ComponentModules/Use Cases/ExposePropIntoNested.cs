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
    public class ExposePropIntoNested
    {
      public enum PropAccess
      {
        None, ReadOnly, WriteOnly, Both
      }

      public interface IAttached { }

      public class GeneratedAttribute : Attribute { }
      public class ExposeAttribute : Attribute
      {
        public string Name { get; }
        public PropAccess Access { get; }

        public ExposeAttribute(string name = null, PropAccess access = PropAccess.Both) => (Name, Access) = (name, access);
      }

      public class ExposeAsAttribute : ExposeAttribute
      {
        public ExposeAsAttribute(string name = null, PropAccess access = PropAccess.Both) : base(name, access) { }
      }


      public partial class Component
      {
        [ExposeAs("Prop")]
        protected string Secret { get; set; }

        [ExposeAs("Cringe.Bruh", PropAccess.ReadOnly)]
        protected string ReadOnlySecret { get; set; }
      }


      public partial class Component
      {
        public class __PropWrapper__1 : IAttached
        {
          public string Bruh
          {
            get => Host.ReadOnlySecret;
          }

          private Component Host;
          public __PropWrapper__1(Component host) => Host = host;
        }

        private void InjectAttached()
        {
          Cringe = new(this);
        }

        public __PropWrapper__1 Cringe { get; private set; }

        public string Prop
        {
          get => Secret;
          set => Secret = value;
        }
      }
    }

  }
}