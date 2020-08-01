namespace BitVm.Lib.Instructions.Logical
{
    public class XOrRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.XOR_REG_LIT;
        public InstructionTypeSizes Size => InstructionTypeSizes.RegLit;
        public string Mnemonic => "xor";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch16();
            var registerValue = cpu.GetRegister(r1);

            var res = registerValue ^ literal;
            cpu.SetRegister(Registers.Acc, (ushort)res);

            return false;
        }
    }
}