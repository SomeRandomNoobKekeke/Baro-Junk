using System;
using System.Collections.Generic;
using System.IO;

namespace CsCodeGenerator
{
  public class CsGenerator
  {
    public static int DefaultTabSize = 2;

    public static string IndentSingle => new string(' ', DefaultTabSize);

    public string OutputDirectory { get; set; }

    public List<FileModel> Files { get; set; } = new List<FileModel>();

    public void CreateFiles()
    {
      ArgumentNullException.ThrowIfNull(OutputDirectory);

      if (!Directory.Exists(OutputDirectory)) Directory.CreateDirectory(OutputDirectory);

      foreach (var file in Files)
      {
        using (StreamWriter writer = File.CreateText(Path.Combine(OutputDirectory, file.FullName)))
        {
          writer.Write(file);
        }
      }
    }
  }
}
