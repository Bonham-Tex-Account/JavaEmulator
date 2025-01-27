using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace JavaByteReader
{
    public class Emunlator
    {
        long[] stack;
        int stackpostion = 0;
        long[] locals;
       
        public Emunlator(ushort maxstack, ushort maxlocal)
        {
            stack = new long[maxstack];
            locals = new long[maxlocal];
        }
        public enum OpCodes : uint
        {
            NOP = 0x00,

            GetStatic = 0xb2,
            iconstm1 = 0x02,
            idc = 0x12,
            fconst_2 = 0x0d,
            invokevirtua = 0xb6,
            dconst_1 = 0x0f,
            atreturn = 0xb1,
            iconst_0 = 0x03,
            iconst_1 = 0x04,
            iconst_2 = 0x05,
            iconst_3 = 0x06,
            iconst_4 = 0x07,
            iconst_5 = 0x08,
            istore_0 = 0x3b,
            istore_1 = 0x3c,
            istore_2 = 0x3d,
            istore_3 = 0x3e,
            Istore = 0x37,
            iload_0 = 0x1a,
            iload_1 = 0x1b,
            iload_2 = 0x1c,
            iload_3 = 0x1d,
            if_icmpge = 0xa2,
            aconst_null = 0x01,
            Goto = 0xa7,
            sipush = 0x11,
            iinc = 0x84,
            bipush = 0x10,
            newarray = 0xbc,
            dup = 0x59,
            iastore = 0x4f,
            astore_0 = 0x4b,
            astore_1 =0x4c,
            astore_2 = 0x4d,
            astore_3 = 0x4e,
            ifne=0x9a,
            iadd= 0x60,
            aload_0=0x2a,
            aload_1 = 0x2b,
            aload_2 = 0x2c,
            aload_3 = 0x2d,
            iaload=0x2e,
            istore=0x36,
            iload=0x15,
            invokestatic=0xb8,
            arraylength=0xbe,
            isub=0x64,
            areturn=0xb0,
            pop=0x57,
            if_icmplt=0xa1,
            if_icmpgt=0xa3,
            ireturn=0xac,
        }
        public (bool,long?) RunOpcode(AttributeCode code, ConstantBase[] constpool, byte opcode, ref int i,JavaClass Java, List<long[]> araara)
        {
            byte byteforopcode = opcode;

            OpCodes Opcode = (OpCodes)opcode;
            switch (Opcode)
            {
                case OpCodes.NOP:
                    break;
                case OpCodes.GetStatic:
                    i++;
                    i++;
                    stack[stackpostion] = 0;
                    stackpostion++;
                    break;



                case OpCodes.iconstm1:
                    stack[stackpostion] = -1;
                    stackpostion++;
                    break;
                case OpCodes.idc:
                    i++;
                    stack[stackpostion] = constpool[code.code[i] - 1].Getlongforstack(this);
                    stackpostion++;
                    break;
                case OpCodes.fconst_2:
                    stack[stackpostion] = floattolong(2f);
                    stackpostion++;
                    break;
                case OpCodes.invokevirtua:
                    {
                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];

                        if (((ConstantUtf8)constpool[((ConstantNameType)constpool[((ConstantRef)constpool[adder - 1]).nametype - 1]).nameindex - 1]).content == "println")
                        {
                            Console.WriteLine(((ConstantUtf8)constpool[stack[stackpostion - 1] - 1]).content);
                            stackpostion--;
                            stackpostion--;
                        }
                    }
                    break;
                case OpCodes.dconst_1:
                    stack[stackpostion] = doubletolong(1d);
                    stackpostion++;
                    break;
                case OpCodes.atreturn:
                    return (true,null);
                case OpCodes.iconst_0:
                case OpCodes.iconst_1:
                case OpCodes.iconst_2:
                case OpCodes.iconst_3:
                case OpCodes.iconst_4:
                case OpCodes.iconst_5:
                    stack[stackpostion] = opcode - 3;
                    stackpostion++;
                    break;
                case OpCodes.istore_0:
                case OpCodes.istore_1:
                case OpCodes.istore_2:
                case OpCodes.istore_3:
                    locals[opcode - 0x3b] = stack[stackpostion - 1];
                    stackpostion--;
                    break;
                case OpCodes.iload_0:
                case OpCodes.iload_1:
                case OpCodes.iload_2:
                case OpCodes.iload_3:
                    stack[stackpostion] = locals[opcode - 0x1a];
                    stackpostion++;
                    break;
                case OpCodes.if_icmpge:
                    {
                        long val2 = stack[stackpostion - 1];
                        stackpostion--;
                        long val1 = stack[stackpostion - 1];
                        stackpostion--;

                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];

                        if (val1 >= val2)
                        {

                            i += (short)adder - 3;
                        }
                    }
                    break;
                case OpCodes.if_icmplt:
                    {
                        long val2 = stack[stackpostion - 1];
                        stackpostion--;
                        long val1 = stack[stackpostion - 1];
                        stackpostion--;

                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];

                        if (val1 < val2)
                        {

                            i += (short)adder - 3;
                        }
                    }
                    break;
                case OpCodes.if_icmpgt:
                    {
                        long val2 = stack[stackpostion - 1];
                        stackpostion--;
                        long val1 = stack[stackpostion - 1];
                        stackpostion--;

                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];

                        if (val1 > val2)
                        {

                            i += (short)adder - 3;
                        }
                    }
                    break;
                case OpCodes.Goto:
                    {
                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];
                        i += (short)adder - 3;

                    }
                    break;
                case OpCodes.aconst_null:
                    stack[stackpostion] = 0;
                    stackpostion++;
                    break;
                case OpCodes.sipush:
                    {
                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];
                        stack[stackpostion] = adder;
                        stackpostion++;
                    }
                    break;
                case OpCodes.iinc:
                    {
                        i++;
                        uint adder = code.code[i];
                        i++;
                        uint constant = code.code[i];
                        locals[adder] += (sbyte)constant;
                    }
                    break;
                case OpCodes.bipush:
                    {
                        i++;
                        uint adder = code.code[i];
                        stack[stackpostion] = adder;
                        stackpostion++;
                    }
                    break;
                case OpCodes.newarray:
                    i++;
                    araara.Add(new long[stack[stackpostion - 1]]);
                    stackpostion--;
                    stack[stackpostion] = araara.Count-1;
                    stackpostion++;
                    break;
                case OpCodes.dup:
                    stack[stackpostion] = stack[stackpostion - 1];
                    stackpostion++;
                    break;
                case OpCodes.iastore:
                    {
                        long value = stack[stackpostion - 1];
                        stackpostion--;
                        long index = stack[stackpostion - 1];
                        stackpostion--;
                        long arrayref = stack[stackpostion - 1];
                        stackpostion--;
                        araara[(int)arrayref][index] = value;
                    }                
                    break;
                case OpCodes.astore_0:
                case OpCodes.astore_1:
                case OpCodes.astore_2:
                case OpCodes.astore_3:
                    locals[opcode-0x4b]=stack[stackpostion-1];
                    stackpostion--;
                    break;
                case OpCodes.ifne:
                    {
                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];
                        if(stack[stackpostion-1]!=0)
                        {
                            i += (short)adder - 3;
                        }
                        stackpostion--;
                    }
                    break;
                case OpCodes.iadd:
                    {
                        int val1 = (int)stack[stackpostion - 1];
                        stackpostion--;
                        int val2 = (int)stack[stackpostion - 1];
                        stackpostion--;
                        stack[stackpostion] = (long)(val1 + val2);
                        stackpostion++;
                    }
                    break;
                case OpCodes.isub:
                    {
                        int val1 = (int)stack[stackpostion - 1];
                        stackpostion--;
                        int val2 = (int)stack[stackpostion - 1];
                        stackpostion--;
                        stack[stackpostion] = (long)(val2 - val1);
                        stackpostion++;
                    }
                    break;
                case OpCodes.aload_0:
                case OpCodes.aload_1:
                case OpCodes.aload_2:
                case OpCodes.aload_3:
                    stack[stackpostion]=locals[opcode - 0x2a];
                    stackpostion++;
                    break;
                case OpCodes.iaload:
                    {
                       
                        long index = stack[stackpostion - 1];
                        stackpostion--;
                        long arrayref = stack[stackpostion - 1];
                        stackpostion--;
                        stack[stackpostion]=araara[(int)arrayref][index];
                        stackpostion++;
                    }                 
                    break;
                case OpCodes.istore:
                    {
                        i++;
                        uint adder = code.code[i];
                        locals[adder] = stack[stackpostion - 1];
                        stackpostion--;
                    }
                    break;
                case OpCodes.iload:
                    {
                        i++;
                        uint adder = code.code[i];
                        stack[stackpostion]=locals[adder] ;
                        stackpostion++;
                    }
                    break;
                case OpCodes.invokestatic:
                    {
                        i++;
                        uint adder = code.code[i];
                        adder <<= 8;
                        i++;
                        adder += code.code[i];
                        string name = ((ConstantUtf8)constpool[((ConstantNameType)constpool[((ConstantMethodRef)constpool[adder - 1]).nametype - 1]).nameindex - 1]).content;                      
                        string descript = ((ConstantUtf8)constpool[((ConstantNameType)constpool[((ConstantMethodRef)constpool[adder - 1]).nametype - 1]).desindex - 1]).content;
                        int numofparameters = 0;
                        if (!descript.StartsWith("()"))
                        {
                            foreach (char commas in descript)
                            {
                                if (commas == '(') continue;
                                if (commas == ')') break;
                                if (commas != '[')
                                {
                                    numofparameters++;
                                }
                            }                            
                        }
                        ;
                        foreach (Method method in Java.methodarray)
                        {               
                            if (((ConstantUtf8)Java.constantarray[method.nameindex - 1]).content == name)
                            {
                                long[] parameters = new long[numofparameters];
                                for (int j = parameters.Length-1; j >=0; j--)
                                {
                                    parameters[j] = stack[stackpostion - 1];
                                    stackpostion--;
                                }
                                long? nulllong = Emunlator.RunMethod(method, Java.constantarray, Java, araara,parameters);
                                if (nulllong != null)
                                {
                                    stack[stackpostion] = nulllong.Value;
                                    stackpostion++;
                                }
                            }                
                           
                        }
                       
                        ;
                    }
                    break;
                case OpCodes.arraylength:
                    {
                        long index = stack[stackpostion - 1];
                        stackpostion--;
                        stack[stackpostion] = araara[(int)index].Length;
                        stackpostion++;
                    }                
                    break;
                case OpCodes.ireturn:
                case OpCodes.areturn:                   
                        return (true, stack[stackpostion-1]);
                case OpCodes.pop:
                    stackpostion--;
                    break;
                   
                default: break;


            }
            return (false,null);
        }
        public static long? RunMethod(Method method, ConstantBase[] constantpool,JavaClass Java, List<long[]> araara,long[] parameters)
        {
            foreach (Attribute atrri in method.attributes)
            {
                if (atrri is AttributeCode code)
                {
                    return  RunCode(code, constantpool,Java,araara,parameters);
                }
            }
            return null;
        }
        public static long? RunCode(AttributeCode code, ConstantBase[] constants,JavaClass Java, List<long[]> araara,long[] para)
        {
            bool isdone = false;
            Emunlator emulator = new Emunlator(code.maxstack, code.maxlocals);
            
            for(int i=0;i<para.Length;i++)
            {
                emulator.locals[i] = para[i];
            }
            ;
            for (int i = 0; isdone==false; i++)
            {
                byte opcode = code.code[i];

               (bool,long?) tuble= emulator.RunOpcode(code, constants, opcode, ref i,Java,araara);
                isdone = tuble.Item1;
                if(tuble.Item2!=null)
                {
                    return tuble.Item2;
                }
            }
            return null;
            ;
        }
        public static double longtodouble(long longer)
        {
            Span<double> doubler = MemoryMarshal.Cast<long, double>(new long[1] { longer });
            return doubler[0];
        }
        public static float longtofloat(long longer)
        {
            Span<float> doubler = MemoryMarshal.Cast<long, float>(new long[1] { longer });
            return doubler[0];
        }
        public static long floattolong(float floater)
        {
            Span<uint> longer = MemoryMarshal.Cast<float, uint>(new float[1] { floater });
            return longer[0];
        }
        public static long doubletolong(double doubler)
        {
            Span<long> longer = MemoryMarshal.Cast<double, long>(new double[1] { doubler });
            return longer[0];
        }
    }
}
