using System;
using System.Collections.Generic;
using System.Linq;

namespace BitVm.Lib
{
    public class CPU
    {
        public byte[] Registers;
        public Dictionary<Registers, int> RegisterMap; 

        public CPU(IMemory memory)
        {
            this.Registers = memory.Create(Enum.GetNames(typeof(Registers)).Length * 2);
            RegisterMap = new Dictionary<Registers, int>();

            initRegisterMap();
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

        public int GetRegister(Registers reg)
        {
            return BitConverter.ToInt32(Registers, RegisterMap[reg]);
        }

        public void SetRegister(Registers reg, int value)
        {
            var tmp = BitConverter.GetBytes(value);

            Array.Copy(tmp, 0, Registers, RegisterMap[reg], sizeof(int));
        }
    }
}