namespace BitVm.Lib.Instructions.Move
{
    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;

        public bool Invoke(CPU cpu)
        {
            var registerFrom = cpu.FetchRegister();
            var address = cpu.Fetch16();
            var value = cpu.GetRegister(registerFrom);

            MemoryMapper.SetUInt16(address, value);

            return false;
        }
    }
}