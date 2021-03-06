﻿using System;
namespace BitVm.Lib.Parsing.AST
{
    public class SquareBracketExpressionNode: ISyntaxNode
    {
        public SquareBracketExpressionNode(ISyntaxNode inner)
        {
            Inner = inner;
        }

        public ISyntaxNode Inner { get; set; }

        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}