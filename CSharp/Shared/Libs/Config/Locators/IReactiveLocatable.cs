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
  /// <summary>
  /// This object is exposing ReactiveEntryLocator and is mapping location calls to it
  /// </summary>
  public interface IReactiveLocatable
  {
    public ReactiveEntryLocator ReactiveLocator { get; }


    public IConfiglike Host => ReactiveLocator.Host;
    public ReactiveEntry ReactiveGetEntry(string propPath) => ReactiveLocator.ReactiveGetEntry(propPath);
    public object ReactiveGetValue(string propPath) => ReactiveLocator.ReactiveGetValue(propPath);
    public bool ReactiveSetValue(string propPath, object value) => ReactiveLocator.ReactiveSetValue(propPath, value);
    public IEnumerable<ReactiveEntry> ReactiveGetEntries() => ReactiveLocator.ReactiveGetEntries();
    public IEnumerable<ReactiveEntry> ReactiveGetAllEntries() => ReactiveLocator.ReactiveGetAllEntries();
    public IEnumerable<ReactiveEntry> ReactiveGetEntriesRec() => ReactiveLocator.ReactiveGetEntriesRec();
    public IEnumerable<ReactiveEntry> ReactiveGetAllEntriesRec() => ReactiveLocator.ReactiveGetAllEntriesRec();
    public Dictionary<string, ReactiveEntry> ReactiveGetFlat() => ReactiveLocator.ReactiveGetFlat();
    public Dictionary<string, ReactiveEntry> ReactiveGetAllFlat() => ReactiveLocator.ReactiveGetAllFlat();
    public Dictionary<string, object> ReactiveGetFlatValues() => ReactiveLocator.ReactiveGetFlatValues();
    public Dictionary<string, object> ReactiveGetAllFlatValues() => ReactiveLocator.ReactiveGetAllFlatValues();
  }

  // public static class IDirectlyLocatableExtensions
  // {
  //   public static IConfiglike GetHost(this IDirectlyLocatable locatable) => locatable.Host;
  //   public static ConfigEntry GetEntry(this IDirectlyLocatable locatable, string propPath) => locatable.GetEntry(propPath);
  //   public static object GetValue(this IDirectlyLocatable locatable, string propPath) => locatable.GetValue(propPath);
  //   public static bool SetValue(this IDirectlyLocatable locatable, string propPath, object value) => locatable.SetValue(propPath, value);
  //   public static IEnumerable<ConfigEntry> GetEntries(this IDirectlyLocatable locatable) => locatable.GetEntries();
  //   public static IEnumerable<ConfigEntry> GetAllEntries(this IDirectlyLocatable locatable) => locatable.GetAllEntries();
  //   public static IEnumerable<ConfigEntry> GetEntriesRec(this IDirectlyLocatable locatable) => locatable.GetEntriesRec();
  //   public static IEnumerable<ConfigEntry> GetAllEntriesRec(this IDirectlyLocatable locatable) => locatable.GetAllEntriesRec();
  //   public static Dictionary<string, ConfigEntry> GetFlat(this IDirectlyLocatable locatable) => locatable.GetFlat();
  //   public static Dictionary<string, ConfigEntry> GetAllFlat(this IDirectlyLocatable locatable) => locatable.GetAllFlat();
  //   public static Dictionary<string, object> GetFlatValues(this IDirectlyLocatable locatable) => locatable.GetFlatValues();
  //   public static Dictionary<string, object> GetAllFlatValues(this IDirectlyLocatable locatable) => locatable.GetAllFlatValues();
  // }
}