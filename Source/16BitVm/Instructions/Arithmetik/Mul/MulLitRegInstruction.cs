using System;
namespace BitVm.Lib.Instructions.Arithmetik.Mul
{
    public class MulLitRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MUL_LIT_REG;

        public bool Invoke(CPU cpu)
        {
            var literal = cpu.Fetch16();
            var r1 = cpu.FetchRegister();
            var registerValue = cpu.GetRegister(r1);

            cpu.SetRegister(Registers.Acc, (ushort)(literal * registerValue));

            return false;
        }
    }
}
