using System;
using BitVm.Lib;
using BitVm.Lib.Devices;
using BitVm.Lib.Parsing;
using BitVm.Lib.Parsing.AST;
using Sprache;

namespace VmRunner
{
    class Program
    { 
        static void Main(string[] args)
        {
            string[] example = {
                "my_label:",
            "mov $4200, &my_label",
            "mov r1, &0060",
            "mov $1300, r1",
            "mov &0060, r2",
            "add r1, r2",
              };

            var iss = Grammar.Parse(string.Join('\n', example)); 

            var program = new byte[] {
                (byte)OpCodes.MOV_LIT_REG, 65, 0xFF, (byte)Registers.R5, // write A
                (byte)OpCodes.MOV_REG_MEM, (byte)Registers.R5, 0xff,0,
                (byte)OpCodes.INC_REG, (byte)Registers.R5,
                (byte)OpCodes.MOV_LIT_REG, (byte)'\n', 0xFF, (byte)Registers.R5, //write new line
                (byte)OpCodes.MOV_REG_MEM, (byte)Registers.R5, 0xff,0,
                (byte)OpCodes.HLT
             };

            SizeTable.Init();
            EmitVisitor visitor = new EmitVisitor();

            Emitter em = new Emitter(visitor);
            visitor.Visit(iss);

            var output = em.ToArray();
            var strOutp = string.Join(' ', output);
            //var vvv = sg.SquareBracketExpression.Parse("[eax - 4]");

             
            MemoryMapper.Map(new MemoryDevice(), 0, 100);

            // Map 0xFF bytes of the address space to an "output device" - just stdout
            MemoryMapper.Map(new ScreenDevice(), 0xff, 0x30ff, true);

            var cpu = new CPU(program);
            cpu.Run();

            cpu.DumpRegisters();
            Console.WriteLine();
        }
    }
}