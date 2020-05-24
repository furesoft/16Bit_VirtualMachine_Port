namespace BitVm.Lib.Instructions.Arithmetik.Sub
{
    public class SubRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.SUB_REG_REG;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var registerValue1 = cpu.GetRegister(r1);
            var registerValue2 = cpu.GetRegister(r2);

            cpu.SetRegister(Registers.Acc, (ushort)(registerValue1 - registerValue2));

            return false;
        }
    }
}
