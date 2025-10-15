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
  // public class ReactiveEntry : IConfigEntry
  // {
  //   public IEntryLocator Locator { get; }

  //   public ReactiveCore ReactiveCore;
  //   public IConfigEntry Entry { get; private set; }

  //   public object Value
  //   {
  //     get => Entry.Value;
  //     set
  //     {
  //       Entry.Value = value;
  //       Model.RaiseOnPropChanged(Path, value);
  //     }
  //   }
  //   public bool IsConfig => Entry.IsConfig;
  //   public bool IsValid => Entry.IsValid;
  //   public string Name => Entry.Name;
  //   public Type Type => Entry.Type;


  //   public ReactiveEntryProxy(ReactiveCore core, IConfigEntry entry)
  //   {
  //     Entry = entry;
  //     ReactiveCore = core;
  //   }
  //   public override string ToString() => Entry.ToString();
  // }

}