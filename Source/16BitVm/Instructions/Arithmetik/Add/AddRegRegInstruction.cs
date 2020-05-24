using System;
namespace BitVm.Lib.Instructions.Arithmetik.Add
{
    public class AddRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.ADD_REG_REG;

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var registerValue1 = cpu.GetRegister(r1);
            var registerValue2 = cpu.GetRegister(r2);

            cpu.SetRegister(Registers.Acc, (ushort)(registerValue1 + registerValue2));

            return false;
        }
    }
}
