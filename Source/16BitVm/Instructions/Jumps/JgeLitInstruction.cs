namespace BitVm.Lib.Instructions.Jumps
{
    public class JgeLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JGE_LIT;
        public InstructionTypeSizes Size => InstructionTypeSizes.RegMem;
        public string Mnemonic => "jge";

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value >= cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
