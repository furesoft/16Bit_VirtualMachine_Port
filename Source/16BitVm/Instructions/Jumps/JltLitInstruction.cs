namespace BitVm.Lib.Instructions.Jumps
{
    public class JltLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JLT_LIT;
        public InstructionTypeSizes Size => InstructionTypeSizes.LitMem;
        public string Mnemonic => "jlt";

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value < cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
