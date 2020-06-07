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

        public void EmitLit8(LiteralNode lit)
        {
            short val = (byte)lit.Value;

            _builder.Append(val);
        }

        public void EmitReg(RegisterLiteralNode lit)
        {
            byte val = (byte)(Registers)lit.Value;

            _builder.Append(val);
        }

        public void EmitOpCode(OpCodes op)
        {
            _builder.Append((byte)op);
        }

        public void EmitInstruction(InstructionNode node)
        {
            EmitOpCode((OpCodes)Enum.Parse(typeof(OpCodes), node.Name, true));

            var type = node.GetTypeInfo();
            switch(type) {
                case InstructionType.LitReg:
                    EmitLit(node.GetArg<HexLiteralNode>(0));
                    EmitReg(node.GetArg<RegisterLiteralNode>(1));
                    break;
                case InstructionType.RegLit:
                    EmitReg(node.GetArg<RegisterLiteralNode>(0));
                    EmitLit(node.GetArg<HexLiteralNode>(1));
                    break;
                case InstructionType.RegLit8:
                    EmitReg(node.GetArg<RegisterLiteralNode>(0));
                    EmitLit8(node.GetArg<LiteralNode>(1));
                    break;
                case InstructionType.Reg:
                    EmitReg(node.GetArg<RegisterLiteralNode>(0));
                    break;
                case InstructionType.RegReg:
                    EmitReg(node.GetArg<RegisterLiteralNode>(0));
                    EmitReg(node.GetArg<RegisterLiteralNode>(1));
                    break;
                case InstructionType.RegPtrReg:
                    EmitReg(node.GetArg<RegisterLiteralNode>(0));
                    EmitReg(node.GetArg<RegisterLiteralNode>(1));
                    break;
                case InstructionType.Lit:
                    EmitLit(node.GetArg<HexLiteralNode>(0));
                    break;
                case InstructionType.LitMem:
                    EmitLit(node.GetArg<HexLiteralNode>(0));
                    EmitMem(node.GetArg<AddressLiteralNode>(1));
                    break;
                case InstructionType.RegMem:
                    EmitReg(node.GetArg<RegisterLiteralNode>(0));
                    EmitMem(node.GetArg<AddressLiteralNode>(1));
                    break;
                case InstructionType.MemReg:
                    EmitMem(node.GetArg<AddressLiteralNode>(0));
                    EmitReg(node.GetArg<RegisterLiteralNode>(1));
                    break;
                case InstructionType.LitOffReg:
                    EmitLit(node.GetArg<HexLiteralNode>(0));
                    EmitReg(node.GetArg<RegisterLiteralNode>(1));
                    EmitReg(node.GetArg<RegisterLiteralNode>(2));
                    break;
            }
        }

        public byte[] ToArray() {
            return _builder.ToArray();
        }
    }
}