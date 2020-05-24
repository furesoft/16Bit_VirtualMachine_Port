using System;
using BitVm.Lib;
using BitVm.Lib.MemoryImplementations;

namespace VmRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new byte[] {
                (byte)OpCodes.MOV_LIT_REG, 42,0, (byte)Registers.R5,
                (byte)OpCodes.MOV_REG_REG, (byte)Registers.R5, (byte)Registers.R6,
             };

            var cpu = new CPU(new ArrayMemory(), program);
            cpu.Step();

            cpu.DumpRegisters();
            Console.WriteLine();

            cpu.Step();

            cpu.DumpRegisters();
            Console.WriteLine();
        }


    }
}