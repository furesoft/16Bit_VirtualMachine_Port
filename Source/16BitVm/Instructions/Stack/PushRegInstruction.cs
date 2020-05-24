namespace BitVm.Lib.Instructions.Stack
{
    public class PushRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.PSH_REG;

        public bool Invoke(CPU cpu)
        {
            var register = cpu.FetchRegister();
            cpu.Push(cpu.GetRegister(register));

            return false;
        }
    }
}