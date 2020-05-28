using System;
using BitVm.Lib;
using BitVm.Lib.Devices;
using BitVm.Lib.Parsing;
using Sprache;

namespace VmRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new byte[] {
                (byte)OpCodes.MOV_LIT_REG, 65, 0xFF, (byte)Registers.R5, // write A
                (byte)OpCodes.MOV_REG_MEM, (byte)Registers.R5, 0xff,0,
                (byte)OpCodes.MOV_LIT_REG, (byte)'\n', 0xFF, (byte)Registers.R5, //write new line
                (byte)OpCodes.MOV_REG_MEM, (byte)Registers.R5, 0xff,0,
                (byte)OpCodes.HLT
             };
             
            MemoryMapper.Map(new MemoryDevice(), 0, 100);

            // Map 0xFF bytes of the address space to an "output device" - just stdout
            MemoryMapper.Map(new ScreenDevice(), 0xff, 0x30ff, true);

            var cpu = new CPU(program);
            cpu.Run();

            cpu.DumpRegisters();
            Console.WriteLine();
            

            var asm = "mov $42, r4";
            var value = InstructionsGrammar.Parse(asm);
        }
    }
}