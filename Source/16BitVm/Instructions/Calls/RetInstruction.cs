namespace BitVm.Lib.Instructions.Calls
{
    public class RetInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.RET;

        public void Invoke(CPU cpu)
        {
            cpu.PopState();
        }
    }
}
