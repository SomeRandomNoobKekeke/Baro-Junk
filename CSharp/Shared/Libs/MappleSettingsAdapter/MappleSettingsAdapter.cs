using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using Barotrauma.LuaCs;
using Barotrauma.LuaCs.Data;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections.Concurrent;

namespace BaroJunk
{
  public partial class MappleSettingsAdapter
  {
    public ContentPackage Package { get; } = ThisPackage;
    public event Action<string, string> OnPropChanged;
  }
}