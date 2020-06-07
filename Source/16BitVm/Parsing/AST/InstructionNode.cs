using System;
using System.Text;

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

        public InstructionType GetTypeInfo()
        {
            string[] spl = Name.Split('_');
            StringBuilder nameBuilder = new StringBuilder();

            if (spl.Length == 3)
            {
                nameBuilder.Append(spl[1] + spl[2]);
            }

            return (InstructionType)Enum.Parse(typeof(InstructionType), nameBuilder.ToString(), true);
        }

        public T GetArg<T>(int index)
        {
            return (T)Args[index];
        }
    }
}