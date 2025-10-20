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
using Microsoft.Xna.Framework;

namespace BaroJunk
{
  public class ConfigSerializationTest : ConfigTest
  {
    public List<UTest> ToTextTest()
    {
      ExampleConfigs.ConfigA config = new();
      // config.Settings().PrintAsXML = true;

      return new List<UTest>()
      {
        new UTest(config.ToText(),"bebebe"),
      };
    }



  }
}