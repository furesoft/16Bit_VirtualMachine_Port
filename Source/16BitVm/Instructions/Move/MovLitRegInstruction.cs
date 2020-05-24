namespace BitVm.Lib.Instructions.Move
{
    public class MovLitRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_LIT_REG;

        public bool Invoke(CPU cpu)
        {
            var literal = cpu.Fetch16();
            var reg = cpu.Fetch();

            cpu.SetRegister(reg, literal);

            return false;
        }
    }
}