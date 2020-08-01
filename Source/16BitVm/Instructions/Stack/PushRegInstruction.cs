namespace BitVm.Lib.Instructions.Stack
{
    public class PushRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.PSH_REG;

        public InstructionTypeSizes Size => InstructionTypeSizes.SingleReg;
        public string Mnemonic => "push";

        public bool Invoke(CPU cpu)
        {
            var register = cpu.FetchRegister();
            cpu.Push(cpu.GetRegister(register));

            return false;
        }
    }
}