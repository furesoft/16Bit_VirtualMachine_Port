namespace BitVm.Lib
{
    public interface IDevice
    {
        void SetUInt8(ushort address, ushort value);
        void SetUInt16(ushort address, ushort value);

        byte GetUInt8(ushort address);
        ushort GetUInt16(ushort address);

        IDevice Create(int size);
    }
}