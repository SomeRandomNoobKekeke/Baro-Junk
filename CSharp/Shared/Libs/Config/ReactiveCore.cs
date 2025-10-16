using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;

using Barotrauma;

namespace BaroJunk
{
  /// <summary>
  /// It's just an object where you can listen for reactive events
  /// </summary>
  public class ReactiveCore
  {
    public event Action<string, object> PropChanged;

    public void RaiseOnPropChanged(string key, object value) => PropChanged?.Invoke(key, value);
  }

}