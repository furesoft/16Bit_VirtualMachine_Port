using System;
namespace BitVm.Lib.Instructions.Stack
{
    public class PopInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.POP;

        public InstructionTypeSizes Size => InstructionTypeSizes.SingleReg;
        public string Mnemonic => "pop";

        public bool Invoke(CPU cpu)
        {
            var register = cpu.FetchRegister();
            var value = cpu.Pop();
            cpu.SetRegister(register, value);

            return false;
        }
    }
}