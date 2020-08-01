using System;
namespace BitVm.Lib.Parsing.AST
{
    public interface ISyntaxNode
    {
        void Accept(SyntaxNodeVisitor visitor);
    }
}