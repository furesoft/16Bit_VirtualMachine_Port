using System;
namespace BitVm.Lib.Instructions.Logical
{
    public class AndRegLitInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.AND_REG_LIT;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var literal = cpu.Fetch16();
            var registerValue = cpu.GetRegister(r1);

            var res = registerValue & literal;
            cpu.SetRegister(Registers.Acc, (ushort)res);

            return false;
        }
    }
}