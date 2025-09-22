using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Barotrauma.Networking;

namespace BaroJunk
{
  public interface IHooksFacade
  {

  }

  public class HooksFacade : IHooksFacade
  {
    public event Action OnStop;
    public event Action OnRoundEnd;
  }



}
