using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace BaroJunk
{
  public class FakeConsoleFacade : IConsoleFacade
  {
    public List<DebugConsole.Command> Commands = new();

    public void Remove(DebugConsole.Command command)
      => Commands.Remove(command);

    public void Insert(DebugConsole.Command command)
      => Commands.Insert(0, command);

  }

}