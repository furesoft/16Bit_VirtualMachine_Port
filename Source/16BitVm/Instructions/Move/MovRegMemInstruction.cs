namespace BitVm.Lib.Instructions.Move
{
    public class MovRegMemInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_REG_MEM;

        public void Invoke(CPU cpu)
        {
            var registerFrom = cpu.FetchRegister();
            var address = cpu.Fetch16();
            var value = cpu.GetRegister(registerFrom);

            cpu.Memory.SetUInt16(address, value);
        }
    }
}