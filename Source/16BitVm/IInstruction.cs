using System;
namespace BitVm.Lib
{
    public interface IInstruction
    {
        InstructionTypeSizes Size { get; }
        OpCodes Instruction { get; }
        InstructionType Type { get; }
        string Mnemonic { get; }
        bool Invoke(CPU cpu);
    }
}