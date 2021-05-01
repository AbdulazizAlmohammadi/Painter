using System;

namespace PaintProject
{
    public class Tokenizer
    {
        public Input input;
        public Tokenizable[] handlers;
        public Tokenizer(Input input, Tokenizable[] handlers)
        {
            this.input = input;
            this.handlers = handlers;
        }

        public Token tokenize()
        {
            Token t = new Token();
            if (!this.input.hasMore())
                return null;
            foreach (var handler in this.handlers)
            {
                if (handler.tokenizable(this.input))
                {
                    t = handler.tokenize(this.input);
                    return t;
                }
            }
            throw new Exception($"Unexpected token {input.peek()} at {(input.Position + 1) }");
        }
    }
}