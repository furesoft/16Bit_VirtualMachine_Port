using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BitVm.Lib
{
    public class CPU
    {
        public Dictionary<Registers, int> RegisterMap;
        public byte[] Program;
        public Dictionary<OpCodes, IInstruction> Instructions;
        public bool IsInInterruptHandler;

        public int InterruptVectorAddress;

        public int StackFrameSize = 0;


        public CPU(byte[] program, int interruptVectorAddress = 0x1000)
        {
            RegisterMap = new Dictionary<Registers, int>();
            Instructions = new Dictionary<OpCodes, IInstruction>();
            InterruptVectorAddress = interruptVectorAddress;


            initRegisterMap();
            SetRegister(Registers.SP, 0xffff - 1);
            SetRegister(Registers.FP, 0xffff - 1);
            SetRegister(Registers.IM, 0xffff);

            Program = program;

            initInstructions();
        }

        public void HandleInterrupt(ushort value)
        {
            var vectorIndex = value & 0xf;
            var isUnmasked = ((1 << vectorIndex) & GetRegister(Registers.IM)) == 1;
            if(!isUnmasked)
            {
                var addrPointer = InterruptVectorAddress + (vectorIndex * 2);
                var address = MemoryMapper.GetUInt16((ushort)addrPointer, this);

                if(!IsInInterruptHandler)
                {
                    Push(0);
                    PushState();
                    IsInInterruptHandler = true;
                    SetRegister(Registers.IP, address);
                }
            }
        }

        private void initInstructions()
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where(_ => _.GetInterfaces().Contains(typeof(IInstruction)));
            foreach (var t in types)
            {
                var instance = (IInstruction)Activator.CreateInstance(t);

                Instructions.Add(instance.Instruction, instance);
            }
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
            return MemoryMapper.GetUInt16((ushort)RegisterMap[reg], this);
        }

        public ushort GetRegister(byte reg)
        {
            return GetRegister((Registers)reg);
        }

        public void SetRegister(Registers reg, ushort value)
        {
            MemoryMapper.SetUInt16((ushort)RegisterMap[reg], value, this);
        }

        public void SetRegister(byte reg, ushort value)
        {
            SetRegister((Registers)reg, value);
        }

        public byte Fetch()
        {
            var nextInstuctionAddress = GetRegister(Registers.IP);
            var instruction = Program[nextInstuctionAddress];
            SetRegister(Registers.IP, (ushort)(nextInstuctionAddress + 1));

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
            var spAddress = GetRegister(Registers.SP);
            MemoryMapper.SetUInt16(spAddress, value, this);
            SetRegister(Registers.SP, (ushort)(spAddress - 2));
            StackFrameSize += 2;
        }

        public ushort Pop()
        {
            var nextSpAddress = GetRegister(Registers.SP) + 2;
            SetRegister(Registers.SP, (ushort)nextSpAddress);
            StackFrameSize -= 2;

            return MemoryMapper.GetUInt16((ushort)nextSpAddress, this);
        }

        public void PushState()
        {
            Push(GetRegister(Registers.R1));
            Push(GetRegister(Registers.R2));
            Push(GetRegister(Registers.R3));
            Push(GetRegister(Registers.R4));
            Push(GetRegister(Registers.R5));
            Push(GetRegister(Registers.R6));
            Push(GetRegister(Registers.R7));
            Push(GetRegister(Registers.R8));
            Push(GetRegister(Registers.IP));
            Push((ushort)(StackFrameSize + 2));

            SetRegister(Registers.FP, GetRegister(Registers.SP));
            StackFrameSize = 0;
        }

        public void PopState()
        {
            var framePointerAddress = GetRegister(Registers.FP);
            SetRegister(Registers.SP, framePointerAddress);

            StackFrameSize = Pop();
            var stackFrameSize = StackFrameSize;

            SetRegister(Registers.IP, Pop());
            SetRegister(Registers.R8, Pop());
            SetRegister(Registers.R7, Pop());
            SetRegister(Registers.R6, Pop());
            SetRegister(Registers.R5, Pop());
            SetRegister(Registers.R4, Pop());
            SetRegister(Registers.R3, Pop());
            SetRegister(Registers.R2, Pop());
            SetRegister(Registers.R1, Pop());

            var nArgs = Pop();
            for (var i = 0; i < nArgs; i++)
            {
                Pop();
            }

            SetRegister(Registers.FP, (ushort)(framePointerAddress + stackFrameSize));
        }
    }
}