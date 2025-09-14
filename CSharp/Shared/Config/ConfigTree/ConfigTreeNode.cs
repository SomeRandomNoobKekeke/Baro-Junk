using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;

using Barotrauma;

namespace BaroJunk
{
  public class ConfigTreeNode : ConfigEntry
  {
    public static ConfigTreeNode Empty => new ConfigTreeNode();

    public ConfigTree Tree;
    public ConfigTreeNode Parent;
    public Dictionary<string, ConfigTreeNode> Children = new();

    public ConfigTreeNode TryGetChild(string name)
      => Children.ContainsKey(name) ? Children[name] : ConfigTreeNode.Empty;


    // public override object Value
    // {
    //   get => Property?.GetValue(Target);
    //   set { if (IsValid) Property.SetValue(Target, value); }
    // }

    public override ConfigEntry Get(string entryPath)
    {
      if (entryPath is null) return ConfigTreeNode.Empty;

      string[] names = entryPath.Split('.');
      if (names.Length == 0) return ConfigTreeNode.Empty;

      ConfigTreeNode node = this;

      foreach (string name in names)
      {
        node = node.TryGetChild(name);
      }

      return node;
    }

    public override IEnumerable<ConfigEntry> Entries => Children.Values;


    public ConfigTreeNode(ConfigEntry entry, ConfigTree tree, ConfigTreeNode parent = null) : base(entry.Target, entry.Property)
    {
      Tree = tree;
      Parent = parent;
    }


  }




}