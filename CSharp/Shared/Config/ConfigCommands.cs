using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using HarmonyLib;
using MoonSharp.Interpreter;

namespace BaroJunk
{
  public static class ConfigCommands
  {
    // static ConfigCommands()
    // {

    // }

    // public static void AddHooks()
    // {
    //   GameMain.LuaCs.Hook.Add("stop", $"remove {IConfig.HookId} config command", (object[] args) =>
    //   {
    //     if (Command is not null)
    //     {
    //       DebugConsole.Commands.Remove(Command);
    //     }
    //     return null;
    //   });


    //   GameMain.LuaCs.Hook.Patch(IConfig.HookId + ".PermitConfigCommand", typeof(DebugConsole).GetMethod("IsCommandPermitted", BindingFlags.NonPublic | BindingFlags.Static),
    //   (object instance, LuaCsHook.ParameterTable ptable) =>
    //   {
    //     if (Command is null) return null;

    //     if (((Identifier)ptable["command"]) == Command.Names[0])
    //     {
    //       ptable.ReturnValue = true;
    //       ptable.PreventExecution = true;
    //     }

    //     return null;
    //   });
    // }

    // public static DebugConsole.Command Command;

    // private static string commandName;
    // public static string CommandName
    // {
    //   get => commandName;
    //   set
    //   {
    //     commandName = value;
    //     UpdateCommand();
    //   }
    // }

    // public static string[][] GetHints()
    //   => IConfig.Current is null ? new string[] { } : IConfig.Current.ToHints();

    // public static void UpdateCommand()
    // {
    //   if (Command is not null) DebugConsole.Commands.Remove(Command);

    //   Command = new DebugConsole.Command(CommandName, "", EditConfig_VanillaCommand, GetHints);
    //   DebugConsole.Commands.Insert(0, Command);
    // }

    // public static void EditConfig_VanillaCommand(string[] args)
    // {
    //   //       if (ConfigManager.CurrentConfig is null)
    //   //       {
    //   //         Mod.Warning("config is null");
    //   //         return;
    //   //       }

    //   //       if (args.Length == 0)
    //   //       {
    //   //         Mod.Log(ConfigSerialization.ToText(ConfigManager.CurrentConfig));
    //   //         return;
    //   //       }

    //   //       var flat = ConfigTraverse.GetFlat(ConfigManager.CurrentConfig);

    //   //       if (args.Length == 1)
    //   //       {
    //   //         if (flat.ContainsKey(args[0]))
    //   //         {
    //   //           Mod.Log(flat[args[0]].Value);
    //   //         }
    //   //         else
    //   //         {
    //   //           Mod.Warning("No such prop");
    //   //         }
    //   //         return;
    //   //       }

    //   //       if (args.Length > 1)
    //   //       {
    //   //         if (!flat.ContainsKey(args[0]))
    //   //         {
    //   //           Mod.Warning("No such prop");
    //   //           return;
    //   //         }

    //   //         ConfigEntry entry = flat[args[0]];
    //   //         try
    //   //         {
    //   //           if (entry.Property.PropertyType.IsAssignableTo(typeof(IConfig)))
    //   //           {
    //   //             Mod.Warning("That's not a prop");
    //   //             return;
    //   //           }

    //   //           entry.Value = Parser.Parse(args[1], entry.Property.PropertyType);
    //   // #if CLIENT
    //   //           if (GameMain.IsMultiplayer) ConfigNetworking.Sync();
    //   // #endif
    //   //         }
    //   //         catch (Exception e)
    //   //         {
    //   //           Mod.Warning(e.Message);
    //   //         }
    //   //       }
    // }

  }
}