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
            MemoryMapper.Map(new MemoryBankedDevice(4, sizeof(short) * 4), 64, 0x11E, true);
            MemoryMapper.Map(new MemoryDevice(), 0x13C, 0xffff, true);

            // Map 0xFF bytes of the address space to an "output device" - just stdout
            MemoryMapper.Map(new ScreenDevice(), 0xff, 0x30ff, true);

            

            var cpu = new CPU(program);
            
            MemoryMapper.SetUInt16(64, 1, cpu);

            var testValueBanked = MemoryMapper.GetUInt16(64, cpu);

            cpu.SetRegister(Registers.MB, 1);

            MemoryMapper.SetUInt16(64, 42, cpu);
            var testValueBanked2 = MemoryMapper.GetUInt16(64, cpu);

            cpu.SetRegister(Registers.MB, 0);
            var testValueBanked3 = MemoryMapper.GetUInt16(64, cpu);

            cpu.Run();

            cpu.DumpRegisters();
            Console.WriteLine();
        }
    }
}