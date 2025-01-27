using System;
using System.Collections.Generic;
using System.Text;

namespace JavaByteReader
{
    public abstract class ConstantBase
    {
        public static byte tag;
        public int totalbytes;        
        public abstract void GetElements(ref Span<byte> span);
        public abstract long Getlongforstack(Emunlator emunlator);
    }
    public class ConstantClass : ConstantBase
    {
        public ushort nametype;
        
        public ConstantClass()
        {
            tag = (byte)7;
            totalbytes = 3;
        }

        public override void GetElements(ref Span<byte> span)
        {
            nametype = Parser.ushortadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            throw new NotImplementedException();
        }
    }
    public class ConstantRef : ConstantBase
    {
        public ushort classindex;
        public ushort nametype;
        public ConstantRef()
        {
            totalbytes = 5;
        }

        public override void GetElements(ref Span<byte> span)
        {
           classindex = Parser.ushortadder(ref span);
           nametype = Parser.ushortadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            throw new NotImplementedException();
        }
    }

    public class ConstantFieldRef : ConstantRef
    {

        public ConstantFieldRef()
        {
            tag = (byte)9;
        }

    }
    public class ConstantMethodRef : ConstantRef
    {

        public ConstantMethodRef()
        {
            tag = (byte)10;
        }



    }
    public class ConstantInterfaceMethodRef : ConstantRef
    {

        public ConstantInterfaceMethodRef()
        {
            tag = (byte)11;
        }

    }
    public class ConstantString : ConstantBase
    {
        public ushort stringindex;
        public ConstantString()
        {
            tag = (byte)8;
            totalbytes = 3;
        }

        public override void GetElements(ref Span<byte> span)
        {
            stringindex = Parser.ushortadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            return stringindex;
        }
    }
    public abstract class ConstantNum : ConstantBase
    {
        public uint num;
        public ConstantNum()
        {
            totalbytes = 5;
        }
        public abstract long Getlong(uint num);
        public override void GetElements(ref Span<byte> span)
        {
            num = Parser.uintadder(ref span);
        }
        public override long Getlongforstack(Emunlator emunlator)
        {
            return Getlong(num);
        }

    }
    public class ConstantInt : ConstantNum
    {
        public ConstantInt()
        {
            tag = 3;
        }
        public  override long Getlong(uint num)
        {
            return (long)num;
        }
    }
    public class ConstantFloat : ConstantNum
    {
        public ConstantFloat()
        {
            tag = 4;
        }
        public override long Getlong(uint num)
        {
            return num;
        }
    }
    public abstract class ConstantLongerNum : ConstantBase
    {
        public uint highnum;
        public uint lownum;
        public ConstantLongerNum()
        {
            totalbytes = 9;
        }
        public abstract long Getlong(uint highnum,uint lownum);
        public override void GetElements(ref Span<byte> span)
        {
           highnum = Parser.uintadder(ref span);
            lownum = Parser.uintadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            return Getlong(highnum,lownum);
        }
    }
    public class ConstantLong : ConstantLongerNum
    {

        public ConstantLong()
        {
            tag = 5;
        }

        

        public override long Getlong(uint highnum, uint lownum)
        {
            long newlong = highnum;
            newlong <<= 32;
            newlong += lownum;
            return newlong;
        }
    }
    public class ConstantDouble : ConstantLongerNum
    {

        public ConstantDouble()
        {
            tag = 6;
        }

        

        public override long Getlong(uint highnum, uint lownum)
        {
            long newlong = highnum;
            newlong <<= 32;
            newlong += lownum;
            return newlong;
        }
    }
    public class ConstantNameType : ConstantBase
    {
        public ushort nameindex;
        public ushort desindex;
        public ConstantNameType()
        {
            tag = 12;
            totalbytes = 5;
        }

        public override void GetElements(ref Span<byte> span)
        {
           nameindex = Parser.ushortadder(ref span);
            desindex = Parser.ushortadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            throw new NotImplementedException();
        }
    }
    public class ConstantMethodHandel : ConstantBase
    {
        public ushort refindex;
        public byte refkind;
        public ConstantMethodHandel()
        {
            tag = 15;
            totalbytes = 4;
        }

        public override void GetElements(ref Span<byte> span)
        {
            refkind += span[0];
            span = span.Slice(1);
            refindex = Parser.ushortadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            return refindex;
        }
    }
    public class ConstantMethodType : ConstantBase
    {
        public ushort desindex;

        public ConstantMethodType()
        {
            tag = 16;
            totalbytes = 3;
        }

        public override void GetElements(ref Span<byte> span)
        {
            desindex = Parser.ushortadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            return desindex;
        }
    }
    public class ConstantInvokeDynamic : ConstantBase
    {
        public ushort bootmethodattrindex;
        public ushort nametype;
        public ConstantInvokeDynamic()
        {
            tag = 18;
            totalbytes = 5;
        }

        public override void GetElements(ref Span<byte> span)
        {
            bootmethodattrindex = Parser.ushortadder(ref span);
            nametype = Parser.ushortadder(ref span);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            throw new NotImplementedException();
        }
    }
    public class ConstantUtf8 : ConstantBase
    {
        public ushort length;
        public byte[] bytesoflength;
        public string content;
        public ConstantUtf8()
        {
            tag = 1;
            totalbytes = 4;
        }

        public override void GetElements(ref Span<byte> span)
        {
            length = Parser.ushortadder(ref span);
            bytesoflength = Parser.bytearradder(ref span, (int)length);
            content = Encoding.UTF8.GetString(bytesoflength);
        }

        public override long Getlongforstack(Emunlator emunlator)
        {
            throw new NotImplementedException();
        }
    }
}