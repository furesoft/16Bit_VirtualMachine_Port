using System;
namespace BitVm.Lib
{
    public enum Instructions : byte
    {
        Mov_Lit_Reg = 0x10,
        Mov_Reg_Reg = 0x11,
        Add_Reg_Reg = 0x12,

    }
}