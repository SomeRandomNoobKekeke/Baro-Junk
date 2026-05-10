using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public interface IDebugNode
  {
    public string Name { get; set; }
    public ClearableEvent<DebugEvent> Pin { get; }
  }
}