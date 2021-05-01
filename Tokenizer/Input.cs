using System;

namespace PaintProject
{
    public delegate bool InputCondition(Input input);
    public class Input
    {
        private readonly string input;
        private int position;
        private readonly int length;

        public int Length
        {
            get
            {
                return this.length;
            }
        }

        public int Position
        {
            get
            {
                return this.position;
            }
        }

        public char Character
        {
            get
            {
                if (this.position > -1)
                    return this.input[this.position];
                else
                    return '\0';
            }
        }

        public Input(string input)
        {
            if (input.Length == 0)
                throw new Exception("empty input is not allowed");

            this.input = input;
            this.length = input.Length;
            this.position = -1;
        }

        public bool hasMore()
        {
            int t = (this.position + 1);
            return 0 <= t && t < this.length;
        }

        public Input step()
        {
            if (this.hasMore())
                this.position++;
            else
            {
                throw new Exception("There is no more step");
            }
            return this;
        }

        public char peek()
        {
            if (this.hasMore())
                return this.input[this.Position + 1];
            return '\0';
        }
        public string loop(InputCondition condition)
        {
            string buffer = "";
            while (this.hasMore() && condition(this))
            {
                buffer += this.step().Character;
            }
            return buffer;
        }
    }
}