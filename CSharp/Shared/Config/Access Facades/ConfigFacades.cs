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

namespace BaroJunk
{
  public class ConfigFacades
  {
    public IIOFacade IOFacade { get; set; } = new IOFacade();
    public INetFacade NetFacade { get; set; } = new NetFacade();
    public IHooksFacade HooksFacade { get; set; } = new HooksFacade();
    public IConsoleFacade ConsoleFacade { get; set; } = new ConsoleFacade();

  }
}