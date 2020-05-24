using System;
using System.Collections.Generic;
using System.Linq;
using BitVm.Lib.Instructions.Move;

namespace BitVm.Lib
{
    public class CPU
    {
        public byte[] Registers;
        public Dictionary<Registers, int> RegisterMap;
        public byte[] Program;
        public byte[] Memory;
        public Dictionary<OpCodes, IInstruction> Instructions;

        public CPU(IMemory memory, byte[] program)
        {
            this.Registers = memory.Create(Enum.GetNames(typeof(Registers)).Length * 2);
            Memory = memory.Create(15);

            RegisterMap = new Dictionary<Registers, int>();
            Instructions = new Dictionary<OpCodes, IInstruction>();

            initRegisterMap();

            this.Program = program;

            initInstructions();
        }

        private void initInstructions()
        {
            Instructions.Add(OpCodes.Mov_Lit_Reg, new MovLitRegInstruction());
            Instructions.Add(OpCodes.Mov_Reg_Reg, new MovRegRegInstruction());
        }

        private void initRegisterMap()
        {
            var names = Enum.GetNames(typeof(Registers));
            for (int i = 0; i < names.Length; i++)
            {
                var reg = (Registers)Enum.Parse(typeof(Registers), names[i]);

                RegisterMap.Add(reg, i * 2);
            }
        }

        public short GetRegister(Registers reg)
        {
            return BitConverter.ToInt16(Registers, RegisterMap[reg]);
        }

        public short GetRegister(byte reg)
        {
            return GetRegister((Registers)reg);
        }

        public void SetRegister(Registers reg, short value)
        {
            var tmp = BitConverter.GetBytes(value);

            Array.Copy(tmp, 0, Registers, RegisterMap[reg], sizeof(short));
        }

        public void SetRegister(byte reg, short value)
        {
            SetRegister((Registers)reg, value);
        }

        public byte Fetch()
        {
            var nextInstuctionAddress = GetRegister(Lib.Registers.IP);
            var instruction = this.Program[nextInstuctionAddress];
            SetRegister(Lib.Registers.IP, (short)(nextInstuctionAddress + 1));

            return instruction;
        }

        public Registers FetchRegister()
        {
            return (Registers)Fetch();
        }

        public short Fetch16()
        {
            var first = Fetch();
            var second = Fetch();

            return BitConverter.ToInt16(new byte[] { first, second }, 0);
        }

        public void Step()
        {
            var instr = Fetch();
            Step((OpCodes)instr);
        }

        public void Step(OpCodes instruction)
        {
            if (Instructions.ContainsKey(instruction))
            {
                Instructions[instruction].Invoke(this);
            }
            else
            {
                throw new Exception("unknown opcode");
            }
        }

        public void DumpRegisters()
        {
            foreach (var reg in RegisterMap)
            {
                Console.WriteLine(reg.Key + ": " + GetRegister(reg.Key));
            }
        }
    }
}