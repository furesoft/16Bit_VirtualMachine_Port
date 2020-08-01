namespace BitVm.Lib.Instructions.Move
{
    public class MovRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_REG;
        public InstructionTypeSizes Size => InstructionTypeSizes.RegReg;
        public string Mnemonic => "move";

        public bool Invoke(CPU cpu)
        {
            var fromReg = cpu.Fetch();
            var toReg = cpu.Fetch();

            var fromValue = cpu.GetRegister(fromReg);

            cpu.SetRegister(toReg, fromValue);

            return false;
        }
    }
}