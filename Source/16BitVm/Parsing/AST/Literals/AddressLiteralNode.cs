using System;
namespace BitVm.Lib.Parsing.AST.Literals
{
    public class AddressLiteralNode : ISyntaxNode
    {
        public string Address { get; set; }

        public AddressLiteralNode(string addr)
        {
            Address = addr;
        }

        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}