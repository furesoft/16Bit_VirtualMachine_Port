namespace BitVm.Lib.Instructions.Arithmetik.Add
{
    public class AddLitRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.ADD_LIT_REG;

        public InstructionTypeSizes Size => InstructionTypeSizes.LitReg;

        public string Mnemonic => "add";

        public bool Invoke(CPU cpu)
        {
            var literal = cpu.Fetch16();
            var r1 = cpu.FetchRegister();
            var registerValue = cpu.GetRegister(r1);

            cpu.SetRegister(Registers.Acc, (ushort)(literal + registerValue));

            return false;
        }
    }
}
