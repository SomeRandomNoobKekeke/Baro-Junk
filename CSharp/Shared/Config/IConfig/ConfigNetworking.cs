#define CLIENT

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
using Barotrauma.Networking;

namespace BaroJunk
{

  public partial interface IConfig
  {
    public string NetHeader => ID;
    public void NetEncode(IWriteMessage msg)
    {
      foreach (ConfigEntry entry in GetEntriesRec())
      {
        NetParser.Encode(msg, entry);
      }
    }

    public void NetDecode(IReadMessage msg)
    {
      foreach (ConfigEntry entry in GetEntriesRec())
      {
        entry.Value = NetParser.Decode(msg, entry.Type);
      }
    }


  }

}