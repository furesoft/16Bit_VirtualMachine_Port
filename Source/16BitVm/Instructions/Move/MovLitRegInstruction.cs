namespace BitVm.Lib.Instructions.Move
{
    public class MovLitRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.Mov_Lit_Reg;

        public void Invoke(CPU cpu)
        {
            var literal = cpu.Fetch16();
            var reg = cpu.Fetch();

            cpu.SetRegister(reg, literal);
        }
    }
}