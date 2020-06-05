using System.Linq;
using BitVm.Lib.Parsing.AST;
using Sprache;

namespace BitVm.Lib.Parsing
{
    public class AsmPrimitiveGrammar
    {
        public virtual Parser<string> UpperOrLowerStr(string c)
        {
            var upper = Parse.String(c.ToUpper()).Text();
            var lower = Parse.String(c.ToLower()).Text();

            return Parse.Or(upper, lower);
        }

        public virtual Parser<T> Choice<T>(params Parser<T>[] list)
        {
            Parser<T> old = list.First();
            foreach (var o in list)
            {
                old = old.Or(o);
            }

            return old;
        }

        public virtual Parser<LiteralNode> Register
        {
            get
            {
                var r1 = UpperOrLowerStr("r1").Return(Registers.R1);
                var r2 = UpperOrLowerStr("r2").Return(Registers.R2);
                var r3 = UpperOrLowerStr("r3").Return(Registers.R3);
                var r4 = UpperOrLowerStr("r4").Return(Registers.R4);
                var r5 = UpperOrLowerStr("r5").Return(Registers.R5);
                var r6 = UpperOrLowerStr("r6").Return(Registers.R6);
                var r7 = UpperOrLowerStr("r7").Return(Registers.R7);
                var r8 = UpperOrLowerStr("r8").Return(Registers.R8);

                var sp = UpperOrLowerStr("sp").Return(Registers.SP);
                var fp = UpperOrLowerStr("fp").Return(Registers.FP);
                var ip = UpperOrLowerStr("ip").Return(Registers.IP);
                var acc = UpperOrLowerStr("acc").Return(Registers.Acc);

                var value = r1.Or(r2).Or(r3).Or(r4).Or(r5).Or(r6).Or(r7).Or(r8).Or(sp).Or(fp).Or(ip).Or(acc);

                return (from v in value
                        select new LiteralNode(v));
            }
        }

        public virtual Parser<string> HexDigit => Parse.Regex("[0-9A-Fa-f]");


        public virtual Parser<ISyntaxNode> HexLiteral =>
        from i in Parse.Char('$')
        from v in Parse.Many(HexDigit)
        select new HexLiteralNode(string.Join("", v));


        public virtual Parser<IdNode> ValidIdentifier =>
            from id in Parse.Identifier(Parse.Letter, Parse.LetterOrDigit)
            select new IdNode(id);


        public virtual Parser<ISyntaxNode> Variable => from c in Parse.Char('!')
                                                      from name in ValidIdentifier
                                                      select name;

        public virtual Parser<Operators> Operator
        {
            get
            {
                var plus = Parse.Char('+').Return(Operators.Plus);
                var minus = Parse.Char('-').Return(Operators.Minus);
                var mul = Parse.Char('*').Return(Operators.Multiply);

                return plus.Or(minus).Or(mul);
            }
        }
        

        public virtual Parser<ISyntaxNode> SquareBracketExpression =>
             from ob in Parse.Char('[')
            from os in Parse.WhiteSpace.Optional()
            from inner in InnerExpression
                  from oss in Parse.WhiteSpace.Optional()
                  from cb in Parse.Char(']')
            select new SquareBracketExpressionNode(inner);


        protected virtual Parser<ISyntaxNode> InnerExpression => Variable.Or(HexLiteral).Or(GroupedExpression);
        

        public virtual Parser<ISyntaxNode> GroupedExpression => from l in Parse.Char('(')
                   from os in Parse.WhiteSpace.Optional()
                   from expr in Parse.Ref(() => InnerExpression)
                   from oss in Parse.WhiteSpace.Optional()
                   from r in Parse.Char(')')
                   select expr;

    }
}