using System;
namespace BitVm.Lib
{
    public enum InstructionType
    {
        LitReg,
        RegLit,
        RegLit8,
        RegReg,
        RegMem,
        MemReg,
        Reg,
        Lit,
        LitMem,
        RegPtrReg,
        LitOffReg,
    }
}