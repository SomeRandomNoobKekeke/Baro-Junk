using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class VersionTest : UTestPack
  {
    public List<UTest> CompatibilityTest()
    {
      Version v1 = new Version(3, 8, 4)
      {
        Branch = "main"
      };

      Version v2 = new Version(0, 4, 4)
      {
        Branch = "pathetic attempts",
        BasedOn = v1,
      };

      Version v3 = new Version(1, 2, 2)
      {
        Branch = "wet dreams",
        BasedOn = v2,
      };

      Version v4 = new Version(0, 2, 2)
      {
        Branch = "not so wet dreams",
        BasedOn = v2,
      };


      return new List<UTest>()
      {
        new UTest(
          v1.CompatibleWith(new Version(0, 4, 3){
            Branch = "pathetic attempts"
          }), false,
          "incompatible, not enough features, branch pathetic attempts doesn't exist yet"
        ),
        new UTest(
          v2.CompatibleWith(new Version(0, 4, 3){
            Branch = "pathetic attempts"
          }), true,
          "compatible, enough features, no major changes"
        ),
        new UTest(
          v3.CompatibleWith(new Version(0, 4, 3){
            Branch = "pathetic attempts"
          }), false,
          "incompatible because there was major changes in wet dreams"
        ),
        new UTest(
          v4.CompatibleWith(new Version(0, 4, 3){
            Branch = "pathetic attempts"
          }), true,
          "compatible, there wasn't any breaking changes in not so wet dreams"
        ),
        new UTest(
          v2.CompatibleWith(new Version(0, 5, 3){
            Branch = "pathetic attempts"
          }), false,
          "incompatible, it requires minor 5 and we got minor 4"
        ),
        new UTest(
          v2.CompatibleWith(new Version(0, 3, 3){
            Branch = "pathetic attempts"
          }), true,
          "compatible, it requires minor 3 and we got minor 4"
        ),
        new UTest(
          v2.CompatibleWith(new Version(3, 8, 6){
            Branch = "main"
          }), true,
          "compatible, depends on deeper version, but deeper version is compatible"
        ),
        new UTest(
          v2.CompatibleWith(new Version(3, 7, 6){
            Branch = "main"
          }), true,
          "compatible, there are new features in deeper verions, still compatible"
        ),
        new UTest(
          v2.CompatibleWith(new Version(2, 8, 6){
            Branch = "main"
          }), false,
          "incompatible, there are breaking changes in deeper version"
        ),
      };
    }
  }
}