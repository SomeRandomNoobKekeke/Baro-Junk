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
  public class SimpleResult
  {
    public bool Success;
    public string Details;
    public Exception Exception;

    public SimpleResult(bool success) => Success = success;
    public override string ToString() => $"{(Success ? "Ok" : Details)}";
  }
}