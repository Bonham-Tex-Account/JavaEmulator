using System;
using System.Collections.Generic;
using System.Text;

namespace JavaByteReader
{
    ref struct Parser
    {
        public JavaClass Parse(byte[] bytes, Dictionary<byte, Func<ConstantBase>> dict, Dictionary<string, Func<Attribute>> attributes)
        {
            Span<byte> span = bytes.AsSpan<byte>();
            uint cafebabe = 0xCAFEBABE;
            uint checker = 0;
            ushort constpoolcount = 0;
            ushort accessflags = 0;
            ushort thisclass = 0;
            ushort superclass = 0;
            ushort interfacecount = 0;
            ushort fieldscount = 0;
            ushort methodcount = 0;
            ushort attributescount = 0;
            ushort minorversion = 0;
            ushort majorversion = 0;
            checker = uintadder(ref span);
            if (cafebabe != checker) throw new Exception("NOT JAVA CODE");
            minorversion = ushortadder(ref span);
            majorversion = ushortadder(ref span);
            constpoolcount = ushortadder(ref span);
            ConstantBase[] constarray = new ConstantBase[constpoolcount - 1];
            for (int i = 0; i < constpoolcount - 1; i++)
            {
                constarray[i] = GetConstant(ref span, dict);
            }
            accessflags = ushortadder(ref span);
            thisclass = ushortadder(ref span);
            superclass = ushortadder(ref span);
            interfacecount = ushortadder(ref span);
            ushort[] interfaces = shortarradder(ref span, interfacecount);
            span = span.Slice(interfaces.Length * 2);
            ;
            fieldscount = ushortadder(ref span);
            Field[] fieldarray = new Field[fieldscount];
            for (int i = 0; i < fieldarray.Length; i++)
            {
                fieldarray[i] = new Field();
                fieldarray[i].Parse(ref span, attributes, constarray);
            }
            ;
            methodcount = ushortadder(ref span);
            Method[] methodarray = new Method[methodcount];
            for (int i = 0; i < methodarray.Length; i++)
            {
                methodarray[i] = new Method();
                methodarray[i].Parse(ref span, attributes, constarray);
            }
            ;
            attributescount = ushortadder(ref span);
            Attribute[] attributearray = new Attribute[attributescount];
            for (int i = 0; i < attributearray.Length; i++)
            {

                attributearray[i] = Parser.GetAttribute(ref span, attributes, constarray);
            }
            JavaClass java = new JavaClass(checker, minorversion, majorversion, constpoolcount, constarray, accessflags, thisclass, superclass, interfacecount, interfaces, fieldscount, fieldarray, methodcount, methodarray, attributescount, attributearray);
            return java;
            ;
        }
        public static ushort ushortadder(ref Span<Byte> span)
        {
            ushort Short = 0;
            for (int i = 0; i < 2; i++)
            {
                Short <<= 8;
                Short += span[0];
                span = span.Slice(1);
            }

            return Short;
        }
        public static uint uintadder(ref Span<Byte> span)
        {
            uint Uint = 0;
            for (int i = 0; i < 4; i++)
            {
                Uint <<= 8;
                Uint += span[0];
                span = span.Slice(1);
            }

            return Uint;
        }
        public static byte[] bytearradder(ref Span<Byte> span, int length)
        {
            byte[] Uint = new byte[length];
            for (int i = 0; i < length; i++)
            {
                Uint[i] = span[0];
                span = span.Slice(1);
            }

            return Uint;
        }
        public static ushort[] shortarradder(ref Span<Byte> span, int length)
        {
            ushort[] Uint = new ushort[length];
            for (int i = 0; i < length; i++)
            {
                Uint[i] = ushortadder(ref span);
            }
            return Uint;
        }
        public static ConstantBase GetConstant(ref Span<Byte> span, Dictionary<byte, Func<ConstantBase>> dict)
        {


            ConstantBase constantbase = (ConstantBase)dict[span[0]].Invoke();
            span = span.Slice(1);
            constantbase.GetElements(ref span);
            return constantbase;
        }
        public static Attribute GetAttribute(ref Span<Byte> span, Dictionary<string, Func<Attribute>> dict, ConstantBase[] constantBases)
        {
            ushort index = span[0];
            index <<= 8;
            index += span[1];
            Attribute attribute;
            if (dict.ContainsKey(((ConstantUtf8)constantBases[index - 1]).content))
            {
                attribute = (Attribute)dict[((ConstantUtf8)constantBases[index - 1]).content].Invoke();
            }
            else
            {
                attribute = new AttributeEverythingElse();
            }
            attribute.GetElements(ref span, dict, constantBases);
            return attribute;
        }
    }
}
