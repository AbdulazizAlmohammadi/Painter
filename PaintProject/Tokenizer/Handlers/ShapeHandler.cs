using System;

namespace PaintProject
{
    public class ShapeHandler : Tokenizable
    {
        public override bool tokenizable(Input input)
        {
            char currentCharacter = input.peek();
            return currentCharacter == 'r' || currentCharacter == 'c' || currentCharacter == 'l' || currentCharacter == 'R' || currentCharacter == 'C' || currentCharacter == 'L';
        }
        public override Token tokenize(Input input)
        {
            string val = "";
            char currentCharacter = input.peek();

            if (currentCharacter == 'r' || currentCharacter == 'c' ||
                currentCharacter == 'l' || currentCharacter == 'R' ||
                currentCharacter == 'C' || currentCharacter == 'L')
            {
                val += input.step().Character;
                val += input.step().Character;
                val += input.step().Character;
                val += input.step().Character;

                if (input.peek() != ',' && !Char.IsWhiteSpace(input.peek()))
                {
                    System.Windows.Forms.MessageBox.Show("Unexpected Token"); // TODO mbox and break;
                    return null;
                }

                if (val.ToLower() == "rect" || val.ToLower() == "circ" || val.ToLower() == "line")
                {
                    return new Token(val, "Shape");
                }
            }
            System.Windows.Forms.MessageBox.Show("Unexpected Token"); // TODO Break
            return null;
        }
    }

}