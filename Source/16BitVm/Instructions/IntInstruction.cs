using System;
namespace BitVm.Lib.Instructions
{
    public class IntInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.INT;
        public string Mnemonic => "int";

        public InstructionTypeSizes Size => InstructionTypeSizes.SingleLit;

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();

            cpu.HandleInterrupt(value);
            return false;
        }
    }
}