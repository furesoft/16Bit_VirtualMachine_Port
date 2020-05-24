namespace BitVm.Lib.Instructions.Arithmetik
{
    public class DecReg : IInstruction
    {
        public OpCodes Instruction => OpCodes.DEC_REG;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var oldValue = cpu.GetRegister(r1);
            var newValue = oldValue - 1;

            cpu.SetRegister(r1, (ushort)newValue);

            return false;
        }
    }
}