namespace PaintProject
{
	public class Token
	{
		public string Type
		{
			set;
			get;
		}

		public string Value
		{
			set;
			get;
		}

		public Token()
		{
			this.Value = "";
		}

		public Token( string value, string type)
		{
			this.Type = type;
			this.Value = value;
		}

		public override string ToString()
		{
			return this.Value;
		}
	}

}