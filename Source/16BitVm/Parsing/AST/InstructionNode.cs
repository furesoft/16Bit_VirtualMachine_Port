using System;
namespace BitVm.Lib.Parsing.AST
{
    public class InstructionNode : ISyntaxNode
    {
        public InstructionNode(string mnemnonic, string name, params ISyntaxNode[] args)
        {
            Mnemonic = mnemnonic;
            Name = name;
            Args = args;
        }

        public string Name { get; set; }
        public string Mnemonic { get; set; }

        public ISyntaxNode[] Args { get; set; }

        public void Accept(SyntaxNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}