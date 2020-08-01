using System;
namespace BitVm.Lib.Instructions.Jumps
{
    public class JneLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JNE_Lit;
        public InstructionTypeSizes Size => InstructionTypeSizes.LitMem;
        public string Mnemonic => "jne";

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value != cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
