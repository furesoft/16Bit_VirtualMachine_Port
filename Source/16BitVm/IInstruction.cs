using System;
namespace BitVm.Lib
{
    public interface IInstruction
    {
        OpCodes Instruction { get; }
        bool Invoke(CPU cpu);
    }
}