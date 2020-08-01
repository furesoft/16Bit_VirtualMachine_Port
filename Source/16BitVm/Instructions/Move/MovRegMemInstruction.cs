namespace BitVm.Lib.Instructions.Move
{
    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;
        public InstructionTypeSizes Size => InstructionTypeSizes.RegMem;
        public string Mnemonic => "move";

        public bool Invoke(CPU cpu)
        {
            var registerFrom = cpu.FetchRegister();
            var address = cpu.Fetch16();
            var value = cpu.GetRegister(registerFrom);

            MemoryMapper.SetUInt16(address, value, cpu);

            return false;
        }
    }
}