using System;
namespace BitVm.Lib.Instructions
{
    public class HltInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.HLT;
        public string Mnemonic => "hlt";

        public InstructionTypeSizes Size => InstructionTypeSizes.NoArg;

        public bool Invoke(CPU cpu)
        {
            return true;
        }
    }
}