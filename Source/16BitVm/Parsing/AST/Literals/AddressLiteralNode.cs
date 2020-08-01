using System;
namespace BitVm.Lib.Parsing.AST.Literals
{
    public class AddressLiteralNode : ISyntaxNode
    {
        public short Address { get; set; }

        public AddressLiteralNode(short addr)
        {
            Address = addr;
        }

        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}