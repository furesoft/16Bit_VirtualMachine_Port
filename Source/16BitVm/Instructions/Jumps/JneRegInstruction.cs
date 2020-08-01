﻿namespace BitVm.Lib.Instructions.Jumps
{
    public class JneRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.JNE_REG;

        public InstructionTypeSizes Size => InstructionTypeSizes.RegMem;

        public string Mnemonic => "jne";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var value = cpu.GetRegister(r1);
            var address = cpu.Fetch16();

            if (value != cpu.GetRegister(Registers.Acc))
            {
                cpu.SetRegister(Registers.IP, address);
            }

            return false;
        }
    }
}
