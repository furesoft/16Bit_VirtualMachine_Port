using System;
using BitVm.Lib;
using BitVm.Lib.MemoryImplementations;

namespace VmRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpu = new CPU(new ArrayMemory());

        }
    }
}