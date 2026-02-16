using System;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public partial class ComponentTest
  {

  public partial class Component
  {
    private void InjectModules()
    {
      InjectModuleHost();
      InjectModuleDependencies();
    }

    private void InjectModuleHost()
    {
      Props.ModuleA.Host = this;
    }

    private void InjectModuleDependencies()
    {
      Props.ModuleA.ModuleB = ModuleB;
    }
  }
  }
}
