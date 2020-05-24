using System;
using System.Linq;

namespace BitVm.Lib
{
    public static class ByteArrayExtensions
    {
        public static short GetInt16(this byte[] src, short address)
        {
            var bytes = src.Skip(address).Take(2).ToArray();

            return BitConverter.ToInt16(bytes, 0);
        }

        public static void SetInt16(this byte[] src, short address, short value)
        {
            var bytes = BitConverter.GetBytes(value);

            Array.Copy(bytes, 0, src, address, bytes.Length);
        }
    }
}