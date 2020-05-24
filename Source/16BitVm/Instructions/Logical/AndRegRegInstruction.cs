namespace BitVm.Lib.Instructions.Logical
{
    public class AndRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.AND_REG_REG;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var registerValue1 = cpu.GetRegister(r1);
            var registerValue2 = cpu.GetRegister(r2);

            var res = registerValue1 & registerValue2;
            cpu.SetRegister(Registers.Acc, (ushort)res);

            return false;
        }
    }
}