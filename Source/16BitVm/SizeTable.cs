using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BitVm.Lib
{
    public static class SizeTable
    {
        private static Dictionary<OpCodes, InstructionTypeSizes> _entries = new Dictionary<OpCodes, InstructionTypeSizes>();

        public static void Init()
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where(_ => _.GetInterfaces().Contains(typeof(IInstruction)));
            foreach (var t in types)
            {
                var instance = (IInstruction)Activator.CreateInstance(t);

                _entries.Add(instance.Instruction, instance.Size);
            }
        }

        public static InstructionTypeSizes GetEntry(OpCodes op)
        {
            return _entries[op];
        }
    }
}