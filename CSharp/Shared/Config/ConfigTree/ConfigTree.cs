using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace BaroJunk
{


  public class ConfigTree
  {
    public Dictionary<string, ConfigTreeNode> Nodes = new();
    public ConfigTreeNode Root;


    public ConfigTree(object o)
    {

    }
  }



}