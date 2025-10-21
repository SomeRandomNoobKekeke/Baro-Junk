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

using BaroJunk_Config;

namespace BaroJunk
{
  public class FakeHooksFacade : IHooksFacade
  {
    public Dictionary<string, Dictionary<string, LuaCsFunc>> Hooks = new();

    public void CallHook(string name, params object[] args)
    {
      if (!Hooks.ContainsKey(name)) return;
      foreach (var (id, func) in Hooks[name])
      {
        func?.Invoke(args);
      }
    }
    public void CallPatch(MethodBase method) { }
    public void AddHook(string name, string identifier, LuaCsFunc func)
    {
      if (!Hooks.ContainsKey(name)) Hooks[name] = new Dictionary<string, LuaCsFunc>();
      Hooks[name][identifier] = func;
    }
    public void Patch(
      string identifier,
      MethodBase method,
      LuaCsPatchFunc patch,
      LuaCsHook.HookMethodType hookType = LuaCsHook.HookMethodType.Before
    )
    {

    }
  }
}