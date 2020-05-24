namespace BitVm.Lib.Instructions.Calls
{
    public class RetInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.RET;

        public bool Invoke(CPU cpu)
        {
            cpu.PopState();

            return false;
        }
    }
}
