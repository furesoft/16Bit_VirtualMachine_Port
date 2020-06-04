using System;
namespace BitVm.Lib.Parsing.AST
{
    public class BinaryOperationNode : ISyntaxNode
    {
        public BinaryOperationNode(ISyntaxNode left, Operators @operator, ISyntaxNode right)
        {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        public ISyntaxNode Left { get; set; }
        public Operators Operator { get; set; }
        public ISyntaxNode Right { get; set; }

        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}