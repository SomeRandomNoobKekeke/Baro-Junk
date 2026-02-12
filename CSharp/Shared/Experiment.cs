using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using CsCodeGenerator;
using CsCodeGenerator.Enums;
namespace BaroJunk
{
  public partial class Mod : IAssemblyPlugin
  {
    public void Experiment()
    {

      var usingDirectives = new List<string>
{
    "using System;",
    "using System.ComponentModel;"
};
      string fileNameSpace = $"{Util.Namespace} CsCodeGenerator.Tests";
      string complexNumberText = "ComplexNumber";

      ClassModel complexNumberClass = new ClassModel(complexNumberText);
      complexNumberClass.SingleKeyWord = KeyWord.Partial; // one way to set single KeyWord
                                                          //complexNumberClass.KeyWords.Add(KeyWord.Partial); // or alternative way
      complexNumberClass.BaseClass = "SomeBaseClass";
      complexNumberClass.Interfaces.Add("NumbericInterface");

      var descriptionAttribute = new AttributeModel("Description")
      {
        SingleParameter = new Parameter(@"""Some class info""")
      };
      complexNumberClass.AddAttribute(descriptionAttribute);

      complexNumberClass.DefaultConstructor.IsVisible = true;

      Constructor secondConstructor = new Constructor(complexNumberClass.Name);
      secondConstructor.Parameters.Add(new Parameter(BuiltInDataType.Double, "real", "0.0"));
      secondConstructor.Parameters.Add(new Parameter(BuiltInDataType.Double, "imaginary", "0.0"));
      secondConstructor.BodyLines.Add("Real = real;");
      secondConstructor.BodyLines.Add("Imaginary = imaginary;");
      complexNumberClass.Constructors.Add(secondConstructor);

      var fields = new Field[]
      {
    new Field(BuiltInDataType.Double, "PI") { SingleKeyWord = KeyWord.Const, DefaultValue ="3.14" },
    new Field(BuiltInDataType.String, "remark") { AccessModifier = AccessModifier.Private },
      }.ToDictionary(a => a.Name, a => a);

      var properties = new Property[]
      {
    new Property(BuiltInDataType.String, "DefaultFormat")
    {
        SingleKeyWord = KeyWord.Static,
        IsGetOnly = true,
        DefaultValue = @"""a + b * i"""
    },
    new Property(BuiltInDataType.Double, "Real"),
    new Property(BuiltInDataType.Double, "Imaginary"),
    new Property(BuiltInDataType.String, "Remark")
    {
        SingleKeyWord = KeyWord.Virtual,
        IsAutoImplemented = false,
        GetterBody = "remark",
        SetterBody = "remark = value"

    },
      }.ToDictionary(a => a.Name, a => a);

      var methods = new Method[]
      {
    new Method(BuiltInDataType.Double, "Modul")
    {
        BodyLines = new List<string> { "return Math.Sqrt(Real * Real + Imaginary * Imaginary);" }
    },
    new Method(complexNumberText, "Add")
    {
        Parameters = new List<Parameter> { new Parameter("ComplexNumber", "input", null) },
        BodyLines = new List<string>
        {
            "ComplexNumber result = new ComplexNumber();",
            "result.Real = Real + input.Real;",
            "result.Imaginary = Imaginary + input.Imaginary;",
            "return result;"
        }
    },
    new Method(BuiltInDataType.String, "ToString")
    {
        Comment = "example of 2 KeyWords(new and virtual), usually here just virtual",
        KeyWords = new List<KeyWord> { KeyWord.New, KeyWord.Virtual },
        BodyLines = new List<string> { "return $\"({Real:0.00}, {Imaginary:0.00})\";" }
    }
      }.ToDictionary(a => a.Name, a => a);

      complexNumberClass.Fields = fields.Values.ToList();
      complexNumberClass.Properties = properties.Values.ToList();
      complexNumberClass.Methods = methods.Values.ToList();

      FileModel complexNumberFile = new FileModel(complexNumberText);
      complexNumberFile.LoadUsingDirectives(usingDirectives);
      complexNumberFile.Namespace = fileNameSpace;
      complexNumberFile.Classes.Add(complexNumberClass);

      CsGenerator csGenerator = new CsGenerator()
      {
        Path = @"C:\Program Files (x86)\Steam\steamapps\common\Barotrauma\LocalMods\Baro Junk\CSharp\Shared\libs\CodeGenerators",
        OutputDirectory = "Generated"
      };
      csGenerator.Files.Add(complexNumberFile);
      csGenerator.CreateFiles(); //Console.Write(complexNumberFile);

    }
  }
}