using System;
namespace BitVm.Lib.MemoryImplementations
{
    public class ArrayMemory : IMemory
    {
        public byte[] Create(int size)
        {
            return new byte[size];
        }
    }
}
