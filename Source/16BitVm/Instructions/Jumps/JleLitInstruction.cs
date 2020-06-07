namespace BitVm.Lib.Instructions.Jumps
{
    public class JleLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JLE_LIT;
        public InstructionTypeSizes Size => InstructionTypeSizes.LitMem;
        public string Mnemonic => "jle";

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value <= cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
