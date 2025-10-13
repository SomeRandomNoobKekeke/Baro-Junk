using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public interface IConfiglikeProvider
  {
    public IConfiglike Target { get; }
  }
}