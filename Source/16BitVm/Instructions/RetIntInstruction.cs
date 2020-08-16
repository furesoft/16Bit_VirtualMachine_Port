using System;
namespace BitVm.Lib.Instructions
{

    public class RetIntInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.RET_INT;
        public string Mnemonic => "rti";

        public InstructionTypeSizes Size => InstructionTypeSizes.NoArg;

        public bool Invoke(CPU cpu)
        {
            cpu.IsInInterruptHandler = false;
            cpu.PopState();
            return false;
        }
    }
}