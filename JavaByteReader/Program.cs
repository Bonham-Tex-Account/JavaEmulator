using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace JavaByteReader
{   
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Func<Attribute>> attributes = new Dictionary<string, Func<Attribute>>()
            {
                ["Code"] = () => new AttributeCode(),
                ["ConstantValue"] = () => new AttributeConstantValue(),
                ["LocalVariableTypeTable"] = () => new AttributeLVTTA()
            };
            Dictionary<byte, Func<ConstantBase>> bases = new Dictionary<byte, Func<ConstantBase>>()
            {
                [7] = () => new ConstantClass(),
                [9] = () => new ConstantFieldRef(),
                [10] = () => new ConstantMethodRef(),
                [11] = () => new ConstantInterfaceMethodRef(),
                [8] = () => new ConstantString(),
                [3] = () => new ConstantInt(),
                [4] = () => new ConstantFloat(),
                [5] = () => new ConstantLong(),
                [6] = () => new ConstantDouble(),
                [12] = () => new ConstantNameType(),
                [1] = () => new ConstantUtf8(),
                [15] = () => new ConstantMethodHandel(),
                [16] = () => new ConstantMethodType(),
                [18] = () => new ConstantInvokeDynamic(),
            };
            byte[] bytes = File.ReadAllBytes("C:\\Users\\Tex\\Documents\\VS code\\Program.class");
            Parser parser = new Parser();
            JavaClass javacode = parser.Parse(bytes, bases, attributes);
            RunJava(javacode);
            ;
        }
       
        public static void RunJava(JavaClass Java)
        {
           List< long[]> araara = new List<long[]>();
            foreach (Method method in Java.methodarray)
            {
                if (((ConstantUtf8)Java.constantarray[method.nameindex - 1]).content == "Main")
                {
                    Emunlator.RunMethod(method, Java.constantarray,Java,araara,new long[0]);
                }
            }
            ;
        }
       
    }
}
