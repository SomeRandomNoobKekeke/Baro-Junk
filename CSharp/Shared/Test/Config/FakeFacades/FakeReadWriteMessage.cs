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

using BaroJunk_Config;

namespace BaroJunk
{

  public class FakeReadWriteMessage : IWriteMessage, IReadMessage
  {
    public Queue<object> Values = new();

    #region NotImplemented
    public int BitPosition { get => 0; set { } }
    public int BytePosition => 0;
    public byte[] Buffer => null;
    public int LengthBits { get => 0; set { } }
    public int LengthBytes => 0;
    public NetworkConnection Sender => null;
    public void WritePadBits() { }
    public void WriteRangedInteger(int val, int min, int max) { }
    public void WriteRangedSingle(Single val, Single min, Single max, int bitCount) { }
    public void WriteBytes(byte[] val, int startPos, int length) { }
    #endregion

    public void WriteBoolean(bool val) => Values.Enqueue(val);
    public void WriteByte(byte val) => Values.Enqueue(val);
    public void WriteUInt16(UInt16 val) => Values.Enqueue(val);
    public void WriteInt16(Int16 val) => Values.Enqueue(val);
    public void WriteUInt32(UInt32 val) => Values.Enqueue(val);
    public void WriteInt32(Int32 val) => Values.Enqueue(val);
    public void WriteUInt64(UInt64 val) => Values.Enqueue(val);
    public void WriteInt64(Int64 val) => Values.Enqueue(val);
    public void WriteSingle(Single val) => Values.Enqueue(val);
    public void WriteDouble(Double val) => Values.Enqueue(val);
    public void WriteColorR8G8B8(Color val) => Values.Enqueue(val);
    public void WriteColorR8G8B8A8(Color val) => Values.Enqueue(val);
    public void WriteVariableUInt32(UInt32 val) => Values.Enqueue(val);
    public void WriteString(String val) => Values.Enqueue(val);
    public void WriteIdentifier(Identifier val) => Values.Enqueue(val);


    #region NotImplemented
    public void ReadPadBits() { }
    public byte PeekByte() => 0;
    public int ReadRangedInteger(int min, int max) => 0;
    public Single ReadRangedSingle(Single min, Single max, int bitCount) => 0;
    public byte[] ReadBytes(int numberOfBytes) => null;
    public byte[] PrepareForSending(bool compressPastThreshold, out bool isCompressed, out int outLength)
    {
      isCompressed = false;
      outLength = 0;
      return null;
    }
    #endregion


    public bool ReadBoolean() => (bool)Values.Dequeue();
    public byte ReadByte() => (byte)Values.Dequeue();
    public UInt16 ReadUInt16() => (UInt16)Values.Dequeue();
    public Int16 ReadInt16() => (Int16)Values.Dequeue();
    public UInt32 ReadUInt32() => (UInt32)Values.Dequeue();
    public Int32 ReadInt32() => (Int32)Values.Dequeue();
    public UInt64 ReadUInt64() => (UInt64)Values.Dequeue();
    public Int64 ReadInt64() => (Int64)Values.Dequeue();
    public Single ReadSingle() => (Single)Values.Dequeue();
    public Double ReadDouble() => (Double)Values.Dequeue();
    public UInt32 ReadVariableUInt32() => (UInt32)Values.Dequeue();
    public String ReadString() => (String)Values.Dequeue();
    public Identifier ReadIdentifier() => (Identifier)Values.Dequeue();
    public Color ReadColorR8G8B8() => (Color)Values.Dequeue();
    public Color ReadColorR8G8B8A8() => (Color)Values.Dequeue();

    public FakeReadWriteMessage Copy() => new FakeReadWriteMessage()
    {
      Values = new Queue<object>(Values)
    };
  }



}
