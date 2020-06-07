using System;
using BitVm.Lib.Parsing.AST.Literals;

namespace BitVm.Lib.Parsing.AST
{
    public abstract class SyntaxNodeVisitor
    {
        public virtual void Visit(IdNode id)
        {

        }
        public virtual void Visit(InstructionNode instr)
        {

        }
        public virtual void Visit(AddressLiteralNode addr)
        {

        }

        public virtual void Visit(HexLiteralNode hex)
        {

        }
        public virtual void Visit(RegisterLiteralNode reg)
        {

        }
        public virtual void Visit(SquareBracketExpressionNode square)
        {

        }

        public virtual void Visit(LiteralNode lit)
        {

        }

        public virtual void Visit(BinaryOperationNode binary)
        {

        }

        public virtual void Visit(CompilationUnit cu)
        {

        }
    }
}