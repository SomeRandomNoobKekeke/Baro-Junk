using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{

  public partial class TreeAdapter<T> where T : ITreeNode
  {
    public ITreeNode Node;

    public TreeAdapter<T> Parent => Node.Parent is null ? null : new TreeAdapter<T>(Node.Parent);
    public IList<TreeAdapter<T>> Children { get; }

    public TreeAdapter(ITreeNode node)
    {
      Node = node;
      Children = new ListAdapter<TreeAdapter<T>>((node.Children as IList));
    }

    public static explicit operator TreeAdapter<T>(ITreeNode node) => new TreeAdapter<T>(node);


  }
}