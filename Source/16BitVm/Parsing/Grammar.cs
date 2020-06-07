using System;
using BitVm.Lib.Parsing.AST;
using Sprache;

namespace BitVm.Lib.Parsing
{
    public class Grammar : InstructionsGrammar
    {
        protected virtual Parser<ISyntaxNode> LabelOrInstruction => Label.Or(Instruction());


        public static CompilationUnit Parse(string source)
        {
            return new CompilationUnit(new Grammar().LabelOrInstruction.Many().Parse(source));
        }
    }
}