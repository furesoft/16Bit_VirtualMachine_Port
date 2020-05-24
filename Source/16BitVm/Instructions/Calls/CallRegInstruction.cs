﻿namespace BitVm.Lib.Instructions.Calls
{
    public class CallRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.CAL_REG;

        public bool Invoke(CPU cpu)
        {
            var register = cpu.FetchRegister();
            var address = cpu.GetRegister(register);
            cpu.PushState();
            cpu.SetRegister(Registers.IP, address);

            return false;
        }
    }
}
