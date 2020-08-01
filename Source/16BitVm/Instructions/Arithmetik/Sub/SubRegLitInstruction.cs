namespace BitVm.Lib.Instructions.Arithmetik.Sub
{
    public class SubRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.SUB_REG_LIT;

        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit;
        public string Mnemonic => "sub";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch16();
            var registerValue = cpu.GetRegister(r1);
            var res = literal - registerValue;

            cpu.SetRegister(Registers.Acc, (ushort)res);

            return false;
        }
    }
}
