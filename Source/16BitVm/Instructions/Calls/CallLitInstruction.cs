using System;
namespace BitVm.Lib.Instructions.Calls
{
    public class CallLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.CAL_LIT;

        public void Invoke(CPU cpu)
        {
            var address = cpu.Fetch16();
            cpu.PushState();
            cpu.SetRegister(Registers.IP, address);
        }
    }
}
