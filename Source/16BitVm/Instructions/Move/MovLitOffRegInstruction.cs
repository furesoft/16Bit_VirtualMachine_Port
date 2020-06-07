namespace BitVm.Lib.Instructions.Move
{
    public class MovLitOffRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_LIT_OFF_REG;
        public InstructionTypeSizes Size => InstructionTypeSizes.LitOffReg;
        public string Mnemonic => "move";

        public bool Invoke(CPU cpu)
        {
            var baseAddress = cpu.Fetch16();
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var offset = cpu.GetRegister(r1);

            var value = MemoryMapper.GetUInt16((ushort)(baseAddress + offset));
            cpu.SetRegister(r2, value);

            return false;
        }
    }
}