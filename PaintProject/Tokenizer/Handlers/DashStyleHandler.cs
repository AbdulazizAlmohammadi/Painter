using System;
using System.Collections.Generic;

namespace PaintProject
{

    public class DashStyleHandler : Tokenizable
    {
        List<string> styles = new List<string>() { "solid", "dash", "dot", "dashdot", "dashdotdot" };
        public override bool tokenizable(Input input)
        {
            char currentCharacter = input.peek();
            return currentCharacter == 'S' || currentCharacter == 'D' || currentCharacter == 's' || currentCharacter == 'd';
        }

        public override Token tokenize(Input input)
        {
            string val = input.step().Character + "";
            while (input.hasMore() && Char.IsLetter(input.peek()))
            {
                val += input.step().Character;
            }
            if (!styles.Contains(val.ToLower()))
            {
                System.Windows.Forms.MessageBox.Show("Unexpected Token");  // TODO mbox and break
                return null;
            }
            else
            {
                return new Token(val, "DashStyle");
            }
        }
    }

}