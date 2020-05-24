using System;
namespace BitVm.Lib.Instructions
{
    public class HltInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.HLT;

        public bool Invoke(CPU cpu)
        {
            return true;
        }
    }
}