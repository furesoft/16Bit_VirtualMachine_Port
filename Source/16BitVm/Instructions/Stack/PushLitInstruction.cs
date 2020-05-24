namespace BitVm.Lib.Instructions.Stack
{
    public class PushLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.PSH_LIT;

        public void Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            cpu.Push(value);
        }
    }
}