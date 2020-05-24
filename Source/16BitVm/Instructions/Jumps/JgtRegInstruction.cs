namespace BitVm.Lib.Instructions.Jumps
{
    public class JgtRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JGT_REG;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var value = cpu.GetRegister(r1);
            var address = cpu.Fetch16();

            if (value > cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
