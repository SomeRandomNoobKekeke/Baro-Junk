using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaroJunk
{
  public class ConfigSerializationTest : ConfigTest
  {
    public UTest ToTextTest()
    {
      ExampleConfigs.ConfigA config = new();

      // UTestLogger.Log($"{config.ToText()}");

      return new UTest("it good enough", "it good enough");
    }

    public UTest ToXMLTest()
    {
      ExampleConfigs.ConfigA config = new();

      return new UTest(config.ToXML().ToString(),
@"<ConfigA>
  <NestedConfigB>
    <NestedConfigC>
      <IntProp>6</IntProp>
      <FloatProp>7</FloatProp>
      <StringProp>bruh</StringProp>
      <NullStringProp>{{null}}</NullStringProp>
    </NestedConfigC>
    <EmptyConfig />
    <IntProp>4</IntProp>
    <FloatProp>5</FloatProp>
    <StringProp>bruh</StringProp>
    <NullStringProp>{{null}}</NullStringProp>
  </NestedConfigB>
  <EmptyConfig />
  <IntProp>2</IntProp>
  <FloatProp>3</FloatProp>
  <StringProp>bruh</StringProp>
  <NullStringProp>{{null}}</NullStringProp>
  <ShouldNotBeDugInto>ShouldNotBeDugInto</ShouldNotBeDugInto>
</ConfigA>");
    }

  }
}