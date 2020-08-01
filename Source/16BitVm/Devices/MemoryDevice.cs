using System;
using System.Linq;

namespace BitVm.Lib.Devices
{
    public class MemoryDevice : IDevice
    {
        byte[] data;

        public MemoryDevice(int size)
        {
            data = new byte[size];
        }

        public MemoryDevice()
        {
            data = new byte[100];
        }

        public ushort GetUInt16(ushort address)
        {
            return data.GetUInt16(address);
        }

        public byte GetUInt8(ushort address)
        {
            return 0;
        }

        public void SetUInt16(ushort address, ushort value)
        {
            data.SetUInt16(address, value);
        }

        public void SetUInt8(ushort address, ushort value)
        {

        }

        public IDevice Create(int size)
        {
            return new MemoryDevice(size);
        }

        public void ViewMemoryAt(int address)
        {
            // 0x0f01: 0x04 0x05 0xA3 0xFE 0x13 0x0D 0x44 0x0F
            var bytes = data.Skip(address).Take(8);
            var hexBytes = bytes.Select(_ => "0x" + _.ToString("x"));
            var joined = string.Join(" ", hexBytes);

            Console.WriteLine(address.ToString("x") + ": " + joined);
        }
    }
}