using System;
namespace BitVm.Lib.Instructions.Calls
{
    public class CallLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.CAL_LIT;
        public InstructionTypeSizes Size => InstructionTypeSizes.SingleLit;
        public string Mnemonic => "call";

        public bool Invoke(CPU cpu)
        {
            var address = cpu.Fetch16();
            cpu.PushState();
            cpu.SetRegister(Registers.IP, address);

            return false;
        }
    }
}
