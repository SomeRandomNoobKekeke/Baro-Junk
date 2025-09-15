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

  public partial interface IConfig
  {
    public static bool ShouldSaveInMultiplayer { get; set; } = false;
    public static bool LoadOnInit { get; set; } = false;
    public static bool SaveOnQuit { get; set; } = true;
    public static bool SaveEveryRound { get; set; } = true;
    public static string SavePath { get; set; } = null;

  }

}