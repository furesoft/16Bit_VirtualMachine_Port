using System;
using System.Linq;

namespace BitVm.Lib.Devices
{
    public class MemoryBankedDevice : IDevice
    {
        private object buffers;
        public MemoryBankedDevice(int bankCount)
        {
            BankCount = bankCount;
        }

        public int BankCount { get; }

        public IDevice Create(int size) //bank size
        {
            buffers = from b in Enumerable.Range(0, BankCount)
                          select new byte[size];

            return new MemoryBankedDevice(BankCount);
        }

        public ushort GetUInt16(ushort address, CPU cpu)
        {
            throw new NotImplementedException();
        }

        public byte GetUInt8(ushort address, CPU cpu)
        {
            throw new NotImplementedException();
        }

        public void SetUInt16(ushort address, ushort value, CPU cpu)
        {
            throw new NotImplementedException();
        }

        public void SetUInt8(ushort address, ushort value, CPU cpu)
        {
            throw new NotImplementedException();
        }
    }
}