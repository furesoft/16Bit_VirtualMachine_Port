namespace BitVm.Lib.Instructions.Move
{
    public class MovRegPtrRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_PTR_REG;
        public InstructionTypeSizes Size => InstructionTypeSizes.RegPtrReg;
        public string Mnemonic => "move";

        public bool Invoke(CPU cpu)
        {
            var r1 = cpu.FetchRegister();
            var r2 = cpu.FetchRegister();
            var ptr = cpu.GetRegister(r1);
            var value = MemoryMapper.GetUInt16(ptr);

            cpu.SetRegister(r2, value);

            return false;
        }
    }
}