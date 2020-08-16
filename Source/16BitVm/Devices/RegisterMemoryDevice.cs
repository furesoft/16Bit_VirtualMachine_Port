using System;

namespace BitVm.Lib.Devices
{
    public class RegisterMemoryDevice : IDevice
    {
        public RegisterMemoryDevice()
        {
            Buffer = new byte[Enum.GetNames(typeof(Registers)).Length * 2];
        }

        public byte[] Buffer { get; }

        public IDevice Create(int size)
        {
            return new RegisterMemoryDevice();
        }

        public ushort GetUInt16(ushort address, CPU cpu)
        {
            return Buffer.GetUInt16(address);
        }

        public byte GetUInt8(ushort address, CPU cpu)
        {
            return Buffer.GetUInt8(address);
        }

        public void SetUInt16(ushort address, ushort value, CPU cpu)
        {
            Buffer.SetUInt16(address, value);
        }

        public void SetUInt8(ushort address, byte value, CPU cpu)
        {
            Buffer.SetUInt8(address, value);
        }
    }
}