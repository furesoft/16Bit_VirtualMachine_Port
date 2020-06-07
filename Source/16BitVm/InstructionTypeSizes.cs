using System;
namespace BitVm.Lib
{
    public enum InstructionTypeSizes : byte
    {
        NoArg = 1,
        LitReg = 4,
        RegLit = 4,
        RegLit8 = 3,
        RegReg = 3,
        RegMem = 4,
        MemReg = 4,
        LitMem = 5,
        RegPtrReg = 3,
        LitOffReg = 5,
        SingleReg = 2,
        SingleLit = 3,
    }
}