namespace BitVm.Lib.Instructions.Stack
{
    public class PushLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.PSH_LIT;

        public InstructionTypeSizes Size => InstructionTypeSizes.SingleLit;
        public string Mnemonic => "push";

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            cpu.Push(value);

            return false;
        }
    }
}