namespace BitVm.Lib
{
    public enum OpCodes : byte
    {
        MOV_LIT_REG = 0x10,
        MOV_REG_REG = 0x11,
        MOV_REG_MEM = 0x12,
        MOV_MEM_REG = 0x13,
        MOV_LIT_MEM = 0x1B,
        MOV_REG_PTR_REG = 0x1C,
        MOV_LIT_OFF_REG = 0x1D,

        ADD_REG_REG = 0x14,
        ADD_LIT_REG = 0x3F,

        SUB_LIT_REG = 0x16,
        SUB_REG_LIT = 0x1E,
        SUB_REG_REG = 0x1F,

        MUL_LIT_REG = 0x20,
        MUL_REG_REG = 0x21,

         LSF_REG_LIT = 0x26,
 LSF_REG_REG = 0x27,
 RSF_REG_LIT = 0x2A,
 RSF_REG_REG = 0x2B,

        INC_REG = 0x35,
        DEC_REG = 0x36,

        JMP_NOT_EQ = 0x15,

        PSH_LIT = 0x17,
        PSH_REG = 0x18,
        POP = 0x1A,

        CAL_LIT = 0x5E,
        CAL_REG = 0x5F,
        RET = 0x60,

        HLT = 0xFF,
    }
}