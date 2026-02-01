using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public interface ITreeNode
  {
    public ITreeNode Parent { get; }
    public IList<ITreeNode> Children { get; }
  }
}