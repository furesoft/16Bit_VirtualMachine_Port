using System;
using System.Collections.Generic;
using System.Linq;
using BitVm.Lib.Devices;
using BitVm.Lib.Instructions;
using BitVm.Lib.Instructions.Arithmetik.Add;
using BitVm.Lib.Instructions.Calls;
using BitVm.Lib.Instructions.Jumps;
using BitVm.Lib.Instructions.Move;
using BitVm.Lib.Instructions.Stack;

namespace BitVm.Lib
{
    public class CPU
    {
        public IDevice Registers;
        public Dictionary<Registers, int> RegisterMap;
        public byte[] Program;
        public MemoryDevice Memory;
        public Dictionary<OpCodes, IInstruction> Instructions;
        public int StackFrameSize = 0;

        public CPU(IDevice memory, byte[] program)
        {
            this.Registers = memory.Create(Enum.GetNames(typeof(Registers)).Length * 2);
            Memory = (MemoryDevice)memory.Create(256 * 256);

            RegisterMap = new Dictionary<Registers, int>();
            Instructions = new Dictionary<OpCodes, IInstruction>();

            initRegisterMap();
            SetRegister(Lib.Registers.SP, 0xffff - 1);
            SetRegister(Lib.Registers.FP, 0xffff - 1);

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

            //other instructions
            Instructions.Add(OpCodes.HLT, new HltInstruction());
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

        public ushort GetRegister(Registers reg)
        {
            return Registers.GetUInt16((ushort)RegisterMap[reg]);
        }

        public ushort GetRegister(byte reg)
        {
            return GetRegister((Registers)reg);
        }

        public void SetRegister(Registers reg, ushort value)
        {
            Registers.SetUInt16((ushort)RegisterMap[reg], value);
        }

        public void SetRegister(byte reg, ushort value)
        {
            SetRegister((Registers)reg, value);
        }

        public byte Fetch()
        {
            var nextInstuctionAddress = GetRegister(Lib.Registers.IP);
            var instruction = this.Program[nextInstuctionAddress];
            SetRegister(Lib.Registers.IP, (ushort)(nextInstuctionAddress + 1));

            return instruction;
        }

        public Registers FetchRegister()
        {
            return (Registers)Fetch();
        }

        public ushort Fetch16()
        {
            var first = Fetch();
            var second = Fetch();

            return BitConverter.ToUInt16(new byte[] { first, second }, 0);
        }

        public bool Step()
        {
            var instr = Fetch();
            return Step((OpCodes)instr);
        }

        public bool Step(OpCodes instruction)
        {
            if (Instructions.ContainsKey(instruction))
            {
                return Instructions[instruction].Invoke(this);
            }
            else
            {
                throw new Exception("unknown opcode");
            }
        }

        public void Run()
        {
            var halt = Step();
            if (!halt)
            {
                Run();
            }
        }

        public void DumpRegisters()
        {
            foreach (var reg in RegisterMap)
            {
                Console.WriteLine(reg.Key + ": " + GetRegister(reg.Key));
            }
        }

        public void Push(ushort value)
        {
            var spAddress = GetRegister(Lib.Registers.SP);
            Memory.SetUInt16(spAddress, value);
            SetRegister(Lib.Registers.SP, (ushort)(spAddress - 2));
            StackFrameSize += 2;
        }

        public ushort Pop()
        {
            var nextSpAddress = GetRegister(Lib.Registers.SP) + 2;
            SetRegister(Lib.Registers.SP, (ushort)nextSpAddress);
            StackFrameSize -= 2;

            return Memory.GetUInt16((ushort)nextSpAddress);
        }

        public void PushState()
        {
            Push(GetRegister(Lib.Registers.R1));
            Push(GetRegister(Lib.Registers.R2));
            Push(GetRegister(Lib.Registers.R3));
            Push(GetRegister(Lib.Registers.R4));
            Push(GetRegister(Lib.Registers.R5));
            Push(GetRegister(Lib.Registers.R6));
            Push(GetRegister(Lib.Registers.R7));
            Push(GetRegister(Lib.Registers.R8));
            Push(GetRegister(Lib.Registers.IP));
            Push((ushort)(StackFrameSize + 2));

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

            SetRegister(Lib.Registers.FP, (ushort)(framePointerAddress + stackFrameSize));
        }
    }
}