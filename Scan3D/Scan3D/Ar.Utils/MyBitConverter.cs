using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D.Ar.Utils
{
    public static class MyBitConverter
    {
        private static byte[] BigEndian(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return bytes;
        }
        private static byte[] LittleEndian(byte[] bytes)
        {
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return bytes;
        }

        private static byte[] BEToNativeEndianness(byte[] bytes, int offset, int count)
        {
            byte[] res = new byte[count];
            Array.Copy(bytes, offset, res, 0, count);
            if (BitConverter.IsLittleEndian) Array.Reverse(res);
            return res;
        }
        private static byte[] LEToNativeEndianness(byte[] bytes, int offset, int count)
        {
            byte[] res = new byte[count];
            Array.Copy(bytes, offset, res, 0, count);
            if (!BitConverter.IsLittleEndian) Array.Reverse(res);
            return res;
        }

        public static byte[] GetBytesBE(bool value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(char value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(short value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(int value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(long value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(ushort value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(uint value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(ulong value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(float value) => BigEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesBE(double value) => BigEndian(BitConverter.GetBytes(value));

        public static byte[] GetBytesLE(bool value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(char value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(short value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(int value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(long value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(ushort value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(uint value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(ulong value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(float value) => LittleEndian(BitConverter.GetBytes(value));
        public static byte[] GetBytesLE(double value) => LittleEndian(BitConverter.GetBytes(value));

        public static bool BEToBoolean(byte[] value, int startIndex) => BitConverter.ToBoolean(BEToNativeEndianness(value, startIndex, 1), 0);
        public static char BEToChar(byte[] value, int startIndex) => BitConverter.ToChar(BEToNativeEndianness(value, startIndex, 2), 0);
        public static short BEToInt16(byte[] value, int startIndex) => BitConverter.ToInt16(BEToNativeEndianness(value, startIndex, 2), 0);
        public static int BEToInt32(byte[] value, int startIndex) => BitConverter.ToInt32(BEToNativeEndianness(value, startIndex, 4), 0);
        public static long BEToInt64(byte[] value, int startIndex) => BitConverter.ToInt64(BEToNativeEndianness(value, startIndex, 8), 0);
        public static ushort BEToUInt16(byte[] value, int startIndex) => BitConverter.ToUInt16(BEToNativeEndianness(value, startIndex, 2), 0);
        public static uint BEToUInt32(byte[] value, int startIndex) => BitConverter.ToUInt32(BEToNativeEndianness(value, startIndex, 4), 0);
        public static ulong BEToUInt64(byte[] value, int startIndex) => BitConverter.ToUInt64(BEToNativeEndianness(value, startIndex, 8), 0);
        public static float BEToSingle(byte[] value, int startIndex) => BitConverter.ToSingle(BEToNativeEndianness(value, startIndex, 4), 0);
        public static double BEToDouble(byte[] value, int startIndex) => BitConverter.ToDouble(BEToNativeEndianness(value, startIndex, 8), 0);

        public static bool LEToBoolean(byte[] value, int startIndex) => BitConverter.ToBoolean(LEToNativeEndianness(value, startIndex, 1), 0);
        public static char LEToChar(byte[] value, int startIndex) => BitConverter.ToChar(LEToNativeEndianness(value, startIndex, 2), 0);
        public static short LEToInt16(byte[] value, int startIndex) => BitConverter.ToInt16(LEToNativeEndianness(value, startIndex, 2), 0);
        public static int LEToInt32(byte[] value, int startIndex) => BitConverter.ToInt32(LEToNativeEndianness(value, startIndex, 4), 0);
        public static long LEToInt64(byte[] value, int startIndex) => BitConverter.ToInt64(LEToNativeEndianness(value, startIndex, 8), 0);
        public static ushort LEToUInt16(byte[] value, int startIndex) => BitConverter.ToUInt16(LEToNativeEndianness(value, startIndex, 2), 0);
        public static uint LEToUInt32(byte[] value, int startIndex) => BitConverter.ToUInt32(LEToNativeEndianness(value, startIndex, 4), 0);
        public static ulong LEToUInt64(byte[] value, int startIndex) => BitConverter.ToUInt64(LEToNativeEndianness(value, startIndex, 8), 0);
        public static float LEToSingle(byte[] value, int startIndex) => BitConverter.ToSingle(LEToNativeEndianness(value, startIndex, 4), 0);
        public static double LEToDouble(byte[] value, int startIndex) => BitConverter.ToDouble(BEToNativeEndianness(value, startIndex, 8), 0);
    }
}
