using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BitVm.Lib
{
    public static class SizeTable
    {
        private static Dictionary<string, InstructionTypeSizes> _entries = new Dictionary<string, InstructionTypeSizes>();

        public static void Init()
        {
            var types = Assembly.GetCallingAssembly().GetTypes().Where(_ => _.GetInterfaces().Contains(typeof(IInstruction)));
            foreach (var t in types)
            {
                var instance = (IInstruction)Activator.CreateInstance(t);

                _entries.Add(buildName(instance), instance.Size);
            }
        }

        public static InstructionTypeSizes GetEntry(string op)
        {
            return _entries[op];
        }

        private static string buildName(IInstruction instruction)
        {
            var sb = new StringBuilder();

            sb.Append(instruction.Mnemonic.ToLower() + "_");
            //ToDo: implment name

            return sb.ToString();
        }
    }
}