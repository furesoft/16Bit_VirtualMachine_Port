namespace BitVm.Lib.Instructions.Calls
{
    public class RetInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.RET;
        public InstructionTypeSizes Size => InstructionTypeSizes.NoArg;
        public string Mnemonic => "ret";

        public bool Invoke(CPU cpu)
        {
            cpu.PopState();

            return false;
        }
    }
}
