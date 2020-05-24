using System;
namespace BitVm.Lib
{
    public interface IInstruction
    {
        OpCodes Instruction { get; }
        void Invoke(CPU cpu);
    }
}