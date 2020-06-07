namespace BitVm.Lib.Instructions.Move
{
    public class MovLitMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_LIT_MEM;
        public InstructionTypeSizes Size => InstructionTypeSizes.LitMem;
        public string Mnemonic => "move";

        public bool Invoke(CPU cpu)
        {
            var value = cpu.Fetch16();
            var address = cpu.Fetch16();
            MemoryMapper.SetUInt16(address, value);

            return false;
        }
    }
}