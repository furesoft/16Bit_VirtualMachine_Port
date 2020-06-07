using System;
namespace BitVm.Lib
{
    public interface IInstruction
    {
        InstructionTypeSizes Size { get; }
        OpCodes Instruction { get; }
        bool Invoke(CPU cpu);
    }
}