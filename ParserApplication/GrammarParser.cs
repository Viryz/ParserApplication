using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sprache;

namespace ParserApplication
{
    public class GrammarParser
    {
        private static readonly Parser<char> SeparatorChar =
        Parse.Chars("()<>@,;:\\\"/[]?={} \t");

        private static readonly Parser<char> ControlChar =
            Parse.Char(Char.IsControl, "Control character");

        private static readonly Parser<char> TokenChar =
            Parse.AnyChar
                .Except(SeparatorChar)
                .Except(ControlChar);

        private static readonly Parser<string> Token =
            TokenChar.AtLeastOnce().Text();

        private static readonly Parser<char> DoubleQuote = Parse.Char('"');
        private static readonly Parser<char> Backslash = Parse.Char('\\');

        private static readonly Parser<char> QdText =
            Parse.AnyChar.Except(DoubleQuote);

        private static readonly Parser<char> QuotedPair =
            from _ in Backslash
            from c in Parse.AnyChar
            select c;

        private static readonly Parser<string> QuotedString =
            from open in DoubleQuote
            from text in QuotedPair.Or(QdText).Many().Text()
            from close in DoubleQuote
            select text;

        private static readonly Parser<string> ParameterValue =
            Token.Or(QuotedString);

        public static Parser<IWrite> ExecutebleClass =
            from nameSpace in Parse.Letter.AtLeastOnce().Text().Token()
            from dot in Parse.Char('.')
            from name in Parse.Letter.AtLeastOnce().Text().Token()
            select GetInstance($"{nameSpace}.{name}");

        public static Parser<IEnumerable<string>> Parameters =
            from l in Parse.Char('(')
            from parameters in Parse.Ref(() => ParameterValue).DelimitedBy(Parse.Char(',').Token())
            from r in Parse.Char(')')
            select parameters;

        private static IWrite GetInstance(string name)
        {
            Type type = Type.GetType(name);
            if (type == null)
                throw new Exception("Type error");

            return Activator.CreateInstance(type) as IWrite;
        }

    }
}
