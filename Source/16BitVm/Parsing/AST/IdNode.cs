using System;
namespace BitVm.Lib.Parsing.AST
{
    public class IdNode : ISyntaxNode
    {
        public IdNode(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}