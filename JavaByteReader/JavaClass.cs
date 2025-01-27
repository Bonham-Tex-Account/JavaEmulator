using System;
using System.Collections.Generic;
using System.Text;

namespace JavaByteReader
{
    public class JavaClass
    {
        public uint magic;
        public ushort minver;
        public ushort majver;
        public ushort constcount;
        public ConstantBase[] constantarray;
        public ushort accessflags;
        public ushort thisclass;
        public ushort superclass;
        public ushort interfacecount;
        public ushort[] interfacearry; //isarray
        public ushort fieldscount;
        public Field[] feildarray; //isarray
        public ushort methodcount;
        public Method[] methodarray; //isarray
        public ushort attributescount;
        public Attribute[] attributesarray; //isarray

        public JavaClass(uint magic, ushort minver, ushort majver, ushort constcount, ConstantBase[] constantarray, ushort accessflags, ushort thisclass, ushort superclass, ushort interfacecount, ushort[] interfacearry, ushort fieldscount, Field[] feildarray, ushort methodcount, Method[] methodarray, ushort attributescount, Attribute[] attributesarray)
        {
            this.magic = magic;
            this.minver = minver;
            this.majver = majver;
            this.constcount = constcount;
            this.constantarray = constantarray;
            this.accessflags = accessflags;
            this.thisclass = thisclass;
            this.superclass = superclass;
            this.interfacecount = interfacecount;
            this.interfacearry = interfacearry;
            this.fieldscount = fieldscount;
            this.feildarray = feildarray;
            this.methodcount = methodcount;
            this.methodarray = methodarray;
            this.attributescount = attributescount;
            this.attributesarray = attributesarray;
        }
    }
}
