namespace BitVm.Lib.Instructions.Jumps
{
    public class JgtLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JGT_LIT;

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value > cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
