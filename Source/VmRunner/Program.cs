using System;
using BitVm.Lib;
using BitVm.Lib.Devices;
using BitVm.Lib.Parsing;

namespace VmRunner
{
    class Program
    { 
        static void Main(string[] args)
        {
            var program = new byte[] {
                (byte)OpCodes.MOV_LIT_REG, 65, 0xFF, (byte)Registers.R5, // write A
                (byte)OpCodes.MOV_REG_MEM, (byte)Registers.R5, 0xff,0,
                (byte)OpCodes.INC_REG, (byte)Registers.R5,
                (byte)OpCodes.MOV_LIT_REG, (byte)'\n', 0xFF, (byte)Registers.R5, //write new line
                (byte)OpCodes.MOV_REG_MEM, (byte)Registers.R5, 0xff,0,
                (byte)OpCodes.HLT
             };

            SizeTable.Init();

            MemoryMapper.Map(new RegisterMemoryDevice(), 0, 30, true);
            MemoryMapper.Map(new MemoryBankedDevice(4), 64, 0xff+31, true);
            MemoryMapper.Map(new MemoryDevice(), 0xff+61, 0xffff, true);

            // Map 0xFF bytes of the address space to an "output device" - just stdout
            MemoryMapper.Map(new ScreenDevice(), 0xff, 0x30ff, true);

            

            var cpu = new CPU(program);
            
            MemoryMapper.SetUInt16(0, 1, cpu);

            var testValueBanked = MemoryMapper.GetUInt16(0, cpu);

            cpu.SetRegister(Registers.MB, 1);
            cpu.Run();

            cpu.DumpRegisters();
            Console.WriteLine();
        }
    }
}