using System;
using System.Collections.Generic;
using System.Linq;
using BitVm.Lib.Instructions.Arithmetik.Add;
using BitVm.Lib.Instructions.Calls;
using BitVm.Lib.Instructions.Jumps;
using BitVm.Lib.Instructions.Move;
using BitVm.Lib.Instructions.Stack;

namespace BitVm.Lib
{
    public class CPU
    {
        public byte[] Registers;
        public Dictionary<Registers, int> RegisterMap;
        public byte[] Program;
        public byte[] Memory;
        public Dictionary<OpCodes, IInstruction> Instructions;
        public int StackFrameSize = 0;

        public CPU(IMemory memory, byte[] program)
        {
            this.Registers = memory.Create(Enum.GetNames(typeof(Registers)).Length * 2);
            Memory = memory.Create(256 * 256);

            RegisterMap = new Dictionary<Registers, int>();
            Instructions = new Dictionary<OpCodes, IInstruction>();

            initRegisterMap();

            this.Program = program;

            initInstructions();
        }

        private void initInstructions()
        {
            //move instructions
            Instructions.Add(OpCodes.MOV_LIT_REG, new MovLitRegInstruction());
            Instructions.Add(OpCodes.MOV_REG_REG, new MovRegRegInstruction());
            Instructions.Add(OpCodes.MOV_MEM_REG, new MovMemRegInstruction());
            Instructions.Add(OpCodes.MOV_REG_MEM, new MovRegMemInstruction());

            //add instructions
            Instructions.Add(OpCodes.ADD_REG_REG, new AddRegRegInstruction());

            //jump instructions
            Instructions.Add(OpCodes.JMP_NOT_EQ, new JmpNotEqualInstruction());

            //stack instructions
            Instructions.Add(OpCodes.POP, new PopInstruction());
            Instructions.Add(OpCodes.PSH_LIT, new PushLitInstruction());
            Instructions.Add(OpCodes.PSH_REG, new PushRegInstruction());

            //call instructions
            Instructions.Add(OpCodes.CAL_LIT, new CallLitInstruction());
            Instructions.Add(OpCodes.CAL_REG, new CallRegInstruction());
            Instructions.Add(OpCodes.RET, new RetInstruction());
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

        public void ViewMemoryAt(int address)
        {
            // 0x0f01: 0x04 0x05 0xA3 0xFE 0x13 0x0D 0x44 0x0F
            var bytes = Memory.Skip(address).Take(8);
            var hexBytes = bytes.Select(_ => "0x" + _.ToString("x"));
            var joined = string.Join(" ", hexBytes);

            Console.WriteLine(address.ToString("x") + ": " + joined);
        }

        public void Push(short value)
        {
            var spAddress = GetRegister(Lib.Registers.SP);
            Memory.SetInt16(spAddress, value);
            SetRegister(Lib.Registers.SP, (short)(spAddress - 2));
            StackFrameSize += 2;
        }

        public short Pop()
        {
            var nextSpAddress = GetRegister(Lib.Registers.SP) + 2;
            SetRegister(Lib.Registers.SP, (short)nextSpAddress);
            StackFrameSize -= 2;

            return Memory.GetInt16((short)nextSpAddress);
        }

        public void PushState()
        {
            Push(this.GetRegister(Lib.Registers.R1));
            Push(this.GetRegister(Lib.Registers.R2));
            Push(this.GetRegister(Lib.Registers.R3));
            Push(this.GetRegister(Lib.Registers.R4));
            Push(this.GetRegister(Lib.Registers.R5));
            Push(this.GetRegister(Lib.Registers.R6));
            Push(this.GetRegister(Lib.Registers.R7));
            Push(this.GetRegister(Lib.Registers.R8));
            Push(this.GetRegister(Lib.Registers.IP));
            Push((short)(StackFrameSize + 2));

            SetRegister(Lib.Registers.FP, GetRegister(Lib.Registers.SP));
            StackFrameSize = 0;
        }

        public void PopState()
        {
            var framePointerAddress = GetRegister(Lib.Registers.FP);
            this.SetRegister(Lib.Registers.SP, framePointerAddress);

            StackFrameSize = Pop();
            var stackFrameSize = StackFrameSize;

            SetRegister(Lib.Registers.IP, Pop());
            SetRegister(Lib.Registers.R8, Pop());
            SetRegister(Lib.Registers.R7, Pop());
            SetRegister(Lib.Registers.R6, Pop());
            SetRegister(Lib.Registers.R5, Pop());
            SetRegister(Lib.Registers.R4, Pop());
            SetRegister(Lib.Registers.R3, Pop());
            SetRegister(Lib.Registers.R2, Pop());
            SetRegister(Lib.Registers.R1, Pop());

            var nArgs = Pop();
            for (var i = 0; i < nArgs; i++)
            {
                Pop();
            }

            SetRegister(Lib.Registers.FP, (short)(framePointerAddress + stackFrameSize));
        }
    }
}