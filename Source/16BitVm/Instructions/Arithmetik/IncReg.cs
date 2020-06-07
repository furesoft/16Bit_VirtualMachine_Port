using System;
namespace BitVm.Lib.Instructions.Arithmetik
{
    public class IncReg : IInstruction
    {
        public OpCodes Instruction => OpCodes.INC_REG;

        public InstructionTypeSizes Size => InstructionTypeSizes.SingleReg;
        public string Mnemonic => "inc";
        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var oldValue = cpu.GetRegister(r1);
            var newValue = oldValue + 1;

            cpu.SetRegister(r1, (ushort)newValue);

            return false;
        }
    }
}