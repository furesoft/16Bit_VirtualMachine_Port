using System;
using System.Collections.Generic;
using BitVm.Lib.Parsing.AST;

namespace BitVm.Lib
{
    public class EmitVisitor : SyntaxNodeVisitor
    {
        public Dictionary<string, int> Labels = new Dictionary<string, int>();
        private int _currentAddress;
        private Emitter _emitter = new Emitter();

        public override void Visit(CompilationUnit cu)
        {
            foreach (var c in cu.Children)
            {
                if(c is LabelNode ln)
                {
                    Labels.Add(ln.Name, _currentAddress);
                }
                else if(c is InstructionNode node)
                {
                    _emitter.EmitInstruction(node);
                    _currentAddress += (byte)SizeTable.GetEntry(node.Name);
                }
            }
        }
    }
}