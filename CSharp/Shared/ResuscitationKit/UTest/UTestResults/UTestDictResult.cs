using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class UTestDictResult : UTestResultBase
  {
    private System.Collections.IDictionary Values;
    public override object Result { get => Values; set => Values = value as System.Collections.IDictionary; }

    public override bool Equals(object obj)
    {
      if (obj is not UTestDictResult other) return false;
      if (Values is not null && other.Values is not null)
      {
        foreach (System.Collections.DictionaryEntry kvp in Values)
        {
          if (!other.Values.Contains(kvp.Key)) return false;
          if (!Object.Equals(other.Values[kvp.Key], kvp.Value)) return false;
        }
        return true;
      }
      return Values == other.Values;
    }

    public UTestDictResult(System.Collections.IDictionary values) => Values = values;

    public override string ToString() => $"bruh";
  }
}