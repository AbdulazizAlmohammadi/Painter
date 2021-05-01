namespace PaintProject
{
	public abstract class Tokenizable
	{
		public abstract bool tokenizable(Input input);
		public abstract Token tokenize(Input input);
	}
}