using System;

namespace PaintProject
{
	public class WhitespaceHandler : Tokenizable
	{
		public override bool tokenizable(Input input)
		{
			return Char.IsWhiteSpace(input.peek());
		}

		static bool isWhiteSpace(Input input)
		{
			char currentCharacter = input.peek();
			return Char.IsWhiteSpace(currentCharacter) || isNewLine(currentCharacter) || isTab(currentCharacter);
		}

		static bool isNewLine(char c)
		{
			return c == '\n';
		}

		static bool isTab(char c)
		{
			return c == '\t';
		}

		public override Token tokenize(Input input)
		{
			return new Token(input.loop(isWhiteSpace), "Whitespace");
		}
	}
}