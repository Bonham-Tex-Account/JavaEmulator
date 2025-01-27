using System;
using System.Collections.Generic;
using System.Text;

namespace JavaByteReader
{
    public class Method
    {
        public ushort accessflags;
        public ushort nameindex;
        public ushort desindex;
        public ushort attributecount;
        public Attribute[] attributes;

        public void Parse(ref Span<byte> span, Dictionary<string, Func<Attribute>> dict, ConstantBase[] constantBases)
        {
            accessflags = Parser.ushortadder(ref span);
            nameindex = Parser.ushortadder(ref span);
            desindex = Parser.ushortadder(ref span);
            attributecount = Parser.ushortadder(ref span);
            attributes = new Attribute[attributecount];
            for (int i = 0; i < attributecount; i++)
            {
                attributes[i] = Parser.GetAttribute(ref span, dict, constantBases);
            }
        }
    }
}
