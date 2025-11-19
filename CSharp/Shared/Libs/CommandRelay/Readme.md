This is one big brainfart

There `if (!client.HasPermission(ClientPermissions.ConsoleCommands) && client.Connection != GameMain.Server.OwnerConnection)`
check in Server DebugConsole, so it's useless
https://github.com/FakeFishGames/Barotrauma/blob/21e34e5cd85ff638b94dacc9d162bf18e9f6cf5a/Barotrauma/BarotraumaServer/ServerSource/DebugConsole.cs#L2782

unless you want to implement some extra permission