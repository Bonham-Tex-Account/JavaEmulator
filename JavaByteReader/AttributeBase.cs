using System;
using System.Collections.Generic;
using System.Text;

namespace JavaByteReader
{
    public abstract class Attribute
    {
        public ushort nameindex;
        public uint length;
        public Attribute()
        {
           
        }
        
        public abstract void GetElements(ref Span<byte> span, Dictionary<string, Func<Attribute>> dict, ConstantBase[] constantBases);
    }
    public class AttributeCode : Attribute
    {
        public ushort maxstack;
        public ushort maxlocals;
        public uint codelength;
        public byte[] code;
        public ushort Exeptiontablelength;
        public ushort[] Exeptiontable;
        public ushort attributescount;
        public Attribute[] attributes;
        public AttributeCode()
        {

        }
        public override void GetElements(ref Span<byte> span, Dictionary<string, Func<Attribute>> dict, ConstantBase[] constantBases)
        {
            nameindex = Parser.ushortadder(ref span);
            length = Parser.uintadder(ref span);
            maxstack = Parser.ushortadder(ref span);
            maxlocals = Parser.ushortadder(ref span);
            codelength = Parser.uintadder(ref span);
            code = Parser.bytearradder(ref span,(int)codelength);
            Exeptiontablelength = Parser.ushortadder(ref span);
            Exeptiontable = Parser.shortarradder(ref span, Exeptiontablelength * 4);
            attributescount = Parser.ushortadder(ref span);
            attributes = new Attribute[attributescount];
            for (int i = 0; i < attributescount; i++)
            {
                attributes[i] = Parser.GetAttribute(ref span, dict, constantBases);
            }
        }
    }
    public class AttributeConstantValue : Attribute
    {
        public ushort constindex;
        public AttributeConstantValue()
        {

        }
        public override void GetElements(ref Span<byte> span,Dictionary<string, Func<Attribute>> dict, ConstantBase[] constantBases)
        {
            nameindex = Parser.ushortadder(ref span);
            length = Parser.uintadder(ref span);
            constindex = Parser.ushortadder(ref span);
        }
    }
    public class AttributeLVTTA : Attribute
    {
        public ushort lvttalength;
        public ushort[] lvtta;
        public AttributeLVTTA()
        {

        }
        public override void GetElements(ref Span<byte> span, Dictionary<string, Func<Attribute>> dict, ConstantBase[] constantBases)
        {
            nameindex = Parser.ushortadder(ref span);
            length = Parser.uintadder(ref span);
            lvttalength = Parser.ushortadder(ref span);
            lvtta = Parser.shortarradder(ref span, lvttalength * 5);
        }
    }
    public class AttributeEverythingElse : Attribute
    {
        public byte[] info;
        public AttributeEverythingElse()
        {

        }
        public override void GetElements(ref Span<byte> span, Dictionary<string, Func<Attribute>> dict, ConstantBase[] constantBases)
        {
           nameindex = Parser.ushortadder(ref span);
            length = Parser.uintadder(ref span);
            info = Parser.bytearradder(ref span, (int)length);
        }
    }
}
