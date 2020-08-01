namespace BitVm.Lib.Instructions.Logical
{
    public class NotInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.NOT;
        public InstructionTypeSizes Size => InstructionTypeSizes.SingleReg;
        public string Mnemonic => "not";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var registerValue = cpu.GetRegister(r1);

            var res = (~registerValue) & 0xffff;
            cpu.SetRegister(Registers.Acc, (ushort)res);

            return false;
        }
    }
}