using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using BaroJunk;
using CsCodeGenerator;

namespace BaroJunk.ComponentModules
{
  public partial class CodeGenerator
  {
    public static Method CreateInjectPartsMethod(PartsInfo parts)
    {
      return new Method("void", "InjectParts")
      {
        BodyLines = parts.Parts.Select(p => $"{p.StringPath}.Self = this;").ToList()
      };
    }
  }
}