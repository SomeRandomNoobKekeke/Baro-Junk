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
  public class FakeIOFacade : IIOFacade
  {
    public Dictionary<string, string> Storage = new();

    public event Action<string> SomethingHappened;

    public XDocument LoadXDoc(string path)
    {
      ArgumentNullException.ThrowIfNull(path);
      if (path.Trim() == "") throw new ArgumentException(path);
      if (!Storage.ContainsKey(path)) throw new Exception("File not found"); //bruh

      SomethingHappened?.Invoke($"xdoc loaded from {path}");

      return XDocument.Parse(Storage[path]);
    }
    public void SaveXDoc(XDocument xdoc, string path)
    {
      ArgumentNullException.ThrowIfNull(path);
      if (path.Trim() == "") throw new ArgumentException(path);
      Storage[path] = xdoc.ToString();

      SomethingHappened?.Invoke($"xdoc saved to {path}");
    }
    public bool FileExists(string path)
    {
      // SomethingHappened?.Invoke($"checked file at {path}");  // not important
      return Storage.ContainsKey(path);
    }

    public void EnsureDirectory(string path)
    {
      // SomethingHappened?.Invoke($"dir ensured {path}"); // not important
      Storage[path] = "dir";
    }
  }
}