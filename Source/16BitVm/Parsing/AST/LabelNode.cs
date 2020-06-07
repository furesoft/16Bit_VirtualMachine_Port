using System;
namespace BitVm.Lib.Parsing.AST
{
    public class LabelNode : ISyntaxNode
    {
        public LabelNode(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        
        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}