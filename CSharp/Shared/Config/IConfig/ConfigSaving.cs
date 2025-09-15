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
    public ConfigSaveResult LoadSave(string path)
    {
      ConfigSaveResult result = Load(path);
      Save(path);
      return result;
    }

    public ConfigSaveResult Save(string path)
    {
      try
      {
        IOAccess.EnsureDirectory(Path.GetDirectoryName(path));

        XDocument xdoc = new XDocument();
        xdoc.Add(ToXML());
        IOAccess.SaveXDoc(xdoc, path);
      }
      catch (Exception e)
      {
        return new ConfigSaveResult(false)
        {
          Details = $"Can't save config: {e.Message}",
          Exception = e,
        };
      }

      return new ConfigSaveResult(true);
    }


    public ConfigSaveResult Load(string path)
    {
      if (!IOAccess.FileExists(path)) return new ConfigSaveResult(false)
      {
        Details = $"Can't load config: [{path}] not found",
      };

      try
      {
        XDocument xdoc = IOAccess.LoadXDoc(path);
        this.FromXML(xdoc.Root);
      }
      catch (Exception e)
      {
        return new ConfigSaveResult(false)
        {
          Details = $"Can't load config: {e.Message}",
          Exception = e,
        };
      }

      return new ConfigSaveResult(true);
    }
  }

}