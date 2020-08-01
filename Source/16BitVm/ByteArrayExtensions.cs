using System;
using System.Linq;

namespace BitVm.Lib
{
    public static class ByteArrayExtensions
    {
        public static ushort GetUInt16(this byte[] src, ushort address)
        {
            var bytes = src.Skip(address).Take(sizeof(ushort)).ToArray();

            return BitConverter.ToUInt16(bytes, 0);
        }

        public static void SetUInt16(this byte[] src, ushort address, ushort value)
        {
            var bytes = BitConverter.GetBytes(value);

            Array.Copy(bytes, 0, src, address, bytes.Length);
        }
    }
}