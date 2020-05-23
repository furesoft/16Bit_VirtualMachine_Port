using System;
namespace BitVm
{
    public interface IMemory
    {
        byte[] Create(int size);
    }
}