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
                (byte)OpCodes.Mov_Lit_Reg, 42,0, (byte)Registers.R5,
                (byte)OpCodes.Mov_Reg_Reg, (byte)Registers.R5, (byte)Registers.R6,
                (byte)OpCodes.Add_Reg_Reg, (byte)Registers.R5, (byte)Registers.R6
             };

            var cpu = new CPU(new ArrayMemory(), program);
            cpu.Step();

            cpu.DumpRegisters();
            Console.WriteLine();

            cpu.Step();

            cpu.DumpRegisters();
            Console.WriteLine();

            cpu.Step();

            cpu.DumpRegisters();
            Console.WriteLine();
        }


    }
}