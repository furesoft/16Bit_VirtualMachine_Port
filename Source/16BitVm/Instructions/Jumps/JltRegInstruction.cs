namespace BitVm.Lib.Instructions.Jumps
{
    public class JltRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JLT_REG;
        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit;
        public string Mnemonic => "jlt";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var value = cpu.GetRegister(r1);
            var address = cpu.Fetch16();

            if (value < cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
