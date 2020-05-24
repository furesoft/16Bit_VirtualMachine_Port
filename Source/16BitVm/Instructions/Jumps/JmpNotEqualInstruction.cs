using System;
namespace BitVm.Lib.Instructions.Jumps
{
    public class JmpNotEqualInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JMP_NOT_EQ;

        public void Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();

            if (value != cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }
        }
    }
}
