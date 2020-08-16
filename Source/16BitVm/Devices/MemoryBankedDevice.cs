using System;
using System.Linq;

namespace BitVm.Lib.Devices
{
    public class MemoryBankedDevice : IDevice
    {
        private byte[][] buffers;
        public MemoryBankedDevice(int bankCount)
        {
            BankCount = bankCount;
        }

        public int BankCount { get; }

        public IDevice Create(int size) //bank size
        {
            buffers = (from b in Enumerable.Range(0, BankCount)
                          select new byte[size]).ToArray();

            return new MemoryBankedDevice(BankCount);
        }

        public ushort GetUInt16(ushort address, CPU cpu)
        {
            var bankIndex = cpu.GetRegister(Registers.MB);

            return buffers[bankIndex].GetUInt16(address);
        }

        public byte GetUInt8(ushort address, CPU cpu)
        {
            var bankIndex = cpu.GetRegister(Registers.MB);

            return buffers[bankIndex].GetUInt8(address);
        }

        public void SetUInt16(ushort address, ushort value, CPU cpu)
        {
            var bankIndex = cpu.GetRegister(Registers.MB);

            buffers[bankIndex].SetUInt16(address, value);
        }

        public void SetUInt8(ushort address, byte value, CPU cpu)
        {
            var bankIndex = cpu.GetRegister(Registers.MB);

            buffers[bankIndex].SetUInt8(address, value);
        }
    }
}