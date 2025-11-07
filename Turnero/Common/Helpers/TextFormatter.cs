namespace Turnero.Common.Helpers
{
	public static class TextFormatter
	{
		public static string Capitalize(string text)
		{
			if (string.IsNullOrEmpty(text))
				return text;

			return char.ToUpper(text[0]) + text.Substring(1).ToLower();
		}

	}
}
