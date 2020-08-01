using System;
using System.Collections.Generic;

namespace BitVm.Lib.Parsing.AST
{
    public class CompilationUnit : ISyntaxNode
    {
        public CompilationUnit(IEnumerable<ISyntaxNode> children)
        {
            Children = children;
        }

        public IEnumerable<ISyntaxNode> Children { get; }

        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}