using System;

namespace PaintProject
{
    public class NumberHandler : Tokenizable
    {
        public override bool tokenizable(Input input)
        {
            char currentCharacter = input.peek();
            return Char.IsDigit(currentCharacter);
        }

        static bool isNumber(Input input)
        {
            char currentCharacter = input.peek();
            return Char.IsDigit(currentCharacter); //TODO check if after number a comma of whitespace 
        }

        public override Token tokenize(Input input)
        {
            string val = "";
            char currentCharacter = input.peek();
            while (input.hasMore() && Char.IsDigit(input.peek()))
            {
                val += input.step().Character;
            }
            if ((input.peek() != ',' && !Char.IsWhiteSpace(input.peek())))
            {
                System.Windows.Forms.MessageBox.Show("Unexpected Token"); // TODO mbox and break
                return null;
            }
            return new Token(val, "Number");
        }
    }
}