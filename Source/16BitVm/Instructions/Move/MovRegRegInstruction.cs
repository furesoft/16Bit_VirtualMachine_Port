namespace BitVm.Lib.Instructions.Move
{
    public class MovRegRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.Mov_Reg_Reg;

        public void Invoke(CPU cpu)
        {
            var fromReg = cpu.Fetch();
            var toReg = cpu.Fetch();

            var fromValue = cpu.GetRegister(fromReg);

            cpu.SetRegister(toReg, fromValue);
        }
    }
}