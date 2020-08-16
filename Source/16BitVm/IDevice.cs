namespace BitVm.Lib
{
    public interface IDevice
    {
        void SetUInt8(ushort address, byte value, CPU cpu);
        void SetUInt16(ushort address, ushort value, CPU cpu);

        byte GetUInt8(ushort address, CPU cpu);
        ushort GetUInt16(ushort address, CPU cpu);

    }
}