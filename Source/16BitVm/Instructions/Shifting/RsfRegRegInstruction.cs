namespace BitVm.Lib.Instructions.Shifting
{
    public class RsfRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.RSF_REG_REG;
        public InstructionTypeSizes Size => InstructionTypeSizes.RegReg;
        public string Mnemonic => "rsh";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var oldValue = cpu.GetRegister(r1);
            var shiftBy = cpu.GetRegister(r2);
            var res = oldValue >> shiftBy;
            cpu.SetRegister(r1, (ushort)res);

            return false;
        }
    }
}