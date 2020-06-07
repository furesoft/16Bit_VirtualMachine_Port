using System;
using BitVm.Lib.Parsing.AST;
using BitVm.Lib.Parsing.AST.Literals;

namespace BitVm.Lib
{
    public class Emitter
    {
        private ByteArrayBuilder _builder = new ByteArrayBuilder();

        public void EmitLit(HexLiteralNode lit)
        {
            short val = (short)lit.Value;
            byte highByte = (byte)((val & 0xff00) >> 8);
            byte lowByte = (byte)(val & 0x00ff);

            _builder.Append(highByte);
            _builder.Append(lowByte);
        }

        public void EmitMem(AddressLiteralNode lit)
        {
            short val = lit.Address;
            byte highByte = (byte)((val & 0xff00) >> 8);
            byte lowByte = (byte)(val & 0x00ff);

            _builder.Append(highByte);
            _builder.Append(lowByte);
        }

        public void EmitLit8(AddressLiteralNode lit)
        {
            short val = lit.Address;
            byte lowByte = (byte)(val & 0x00ff);

            _builder.Append(lowByte);
        }

        public void EmitReg(RegisterLiteralNode lit)
        {
            byte val = (byte)lit.Value;

            _builder.Append(val);
        }

        public void EmitOpCode(OpCodes op)
        {
            _builder.Append((byte)op);
        }

        public void EmitInstruction(InstructionNode node)
        {
            EmitOpCode((OpCodes)Enum.Parse(typeof(OpCodes), node.Name, true));
        }
    }
}