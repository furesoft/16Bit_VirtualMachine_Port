using System;
using BitVm.Lib;
using BitVm.Lib.Devices;

namespace VmRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new byte[] {
                (byte)OpCodes.MOV_LIT_REG, 0x02, 65, (byte)Registers.R5,
                (byte)OpCodes.MOV_REG_MEM, (byte)Registers.R5, 0x30, 0,
                (byte)OpCodes.HLT
             };
             
            var memory = new MemoryDevice();

            MemoryMapper.Map(memory, 0, 0xffff);

            // Map 0xFF bytes of the address space to an "output device" - just stdout
            MemoryMapper.Map(new ScreenDevice(), 0x3000, 0x30ff, true);

            var cpu = new CPU(memory, program);
            cpu.Run();

            cpu.DumpRegisters();
            Console.WriteLine();
        }
    }
}