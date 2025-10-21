using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;

using BaroJunk_Config;

namespace BaroJunk
{
  public class FakeFacades : IConfigFacades
  {
    public IIOFacade IOFacade { get; set; } = new FakeIOFacade();
    public INetFacade NetFacade { get; set; } = new NetFacade();
    public IHooksFacade HooksFacade { get; set; } = new HooksFacade();
    public IConsoleFacade ConsoleFacade { get; set; } = new ConsoleFacade();

  }
}