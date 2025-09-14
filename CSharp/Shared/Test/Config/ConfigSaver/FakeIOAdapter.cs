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
  public class FakeIOAdapter : IIOAdapter
  {
    public Dictionary<string, string> Storage = new();

    public XDocument LoadXDoc(string path)
    {
      ArgumentNullException.ThrowIfNull(path);
      if (path.Trim() == "") throw new ArgumentException(path);
      if (!Storage.ContainsKey(path)) throw new Exception("File not found");//bruh

      return XDocument.Parse(Storage[path]);
    }
    public void SaveXDoc(XDocument xdoc, string path)
    {
      ArgumentNullException.ThrowIfNull(path);
      if (path.Trim() == "") throw new ArgumentException(path);
      Storage[path] = xdoc.ToString();
    }
    public bool FileExists(string path)
      => Storage.ContainsKey(path);
    public void EnsureDirectory(string path)
      => Storage[path] = "dir";
  }
}