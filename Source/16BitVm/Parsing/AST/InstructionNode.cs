using System;
namespace BitVm.Lib.Parsing.AST
{
    public class InstructionNode : ISyntaxNode
    {
        public InstructionNode(string name, params ISyntaxNode[] args)
        {
            Name = name;
            Args = args;
        }

        public string Name { get; set; }

        public ISyntaxNode[] Args { get; set; }
    }
}