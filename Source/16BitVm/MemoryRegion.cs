namespace BitVm.Lib
{
    public struct MemoryRegion
    {
        public ushort Start;
        public ushort End;
        public bool Remap;
        public IDevice Device;

        public MemoryRegion(IDevice device, ushort start, ushort end, bool remap)
        {
            this.Device = device;
            this.Start = start;
            this.End = end;
            this.Remap = remap;
        }
    }
}