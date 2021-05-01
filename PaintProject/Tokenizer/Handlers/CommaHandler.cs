using System;

namespace PaintProject
{

    public class CommaHandler : Tokenizable
    {
        public string buffer;
        public override bool tokenizable(Input input)
        {
            char currentCharacter = input.peek();
			return currentCharacter == ',';
        }
        
        public override Token tokenize(Input input)
        {
            input.step();
			return new Token("," , "Comma");
        }
    }

}