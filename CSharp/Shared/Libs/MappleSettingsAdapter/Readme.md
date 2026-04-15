Rofl wrapper around ConfigService that can be used as IDictionary<string, string>

Very cursed

Usage:
```C#
MappleSettingsAdapter settings = new MappleSettingsAdapter();

settings.OnPropChanged += (key, value) =>
{
  Logger.Default.Log($"setting set {key} {value}");
};

settings["bruh"] = "balls";
Logger.Default.Log(Logger.Wrap.IEnumerable(settings.Keys)); // ["bruh"]

Logger.Default.Log(settings["bruh"]); // balls
```
