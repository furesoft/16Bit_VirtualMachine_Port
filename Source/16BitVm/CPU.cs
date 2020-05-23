using System;
using System.Collections.Generic;
using System.Linq;

namespace BitVm.Lib
{
    public class CPU
    {
        public byte[] Registers;
        public Dictionary<Registers, int> RegisterMap;
        public byte[] Program;

        public CPU(IMemory registerMemory, byte[] program)
        {
            this.Registers = registerMemory.Create(Enum.GetNames(typeof(Registers)).Length * 2);
            RegisterMap = new Dictionary<Registers, int>();

            initRegisterMap();

            this.Program = program;
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
            Step((Instructions)instr);
        }

        public void Step(Instructions instruction)
        {
            switch (instruction)
            {
                case Instructions.Mov_Lit_Reg:
                    var literal = Fetch16();
                    var reg = Fetch();

                    SetRegister(reg, literal);
                    break;
                case Instructions.Mov_Reg_Reg:
                    var fromReg = Fetch();
                    var toReg = Fetch();

                    var fromValue = GetRegister(fromReg);

                    SetRegister(toReg, fromValue);

                    break;
                case Instructions.Add_Reg_Reg:
                    var reg1 = FetchRegister();
                    var reg2 = FetchRegister();

                    SetRegister(Lib.Registers.Acc, (short)(GetRegister(reg1) + GetRegister(reg2)));

                    break;
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