namespace BitVm.Lib.Instructions.Move
{
    public class MovMemRegInstruction : IInstruction
    {
        public OpCodes Instruction => OpCodes.MOV_MEM_REG;

        public void Invoke(CPU cpu)
        {
            var address = cpu.Fetch16();
            var registerTo = cpu.FetchRegister();
            var value = cpu.Memory.GetUInt16(address);

            cpu.SetRegister(registerTo, value);
        }
    }
}