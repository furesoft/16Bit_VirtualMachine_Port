using System;
namespace BitVm.Lib.Instructions.Stack
{
    public class PopInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.POP;

        public void Invoke(CPU cpu)
        {
            var register = cpu.FetchRegister();
            var value = cpu.Pop();
            cpu.SetRegister(register, value);
        }
    }
}