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
    public class ExposeProp
    {
      public enum PropAccess
      {
        None, ReadOnly, WriteOnly, Both
      }

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
        [ExposeAs("Prop")] protected string Secret { get; set; }
        [ExposeAs("Bruh", PropAccess.ReadOnly)] protected string ReadOnlySecret { get; set; }
      }


      public partial class Component
      {
        [Generated]
        public string Prop
        {
          get => Secret;
          set => Secret = value;
        }

        [Generated]
        public string Bruh
        {
          get => ReadOnlySecret;
        }
      }
    }

  }
}