using TaktTusur.Media.Core.Interfaces;

namespace TaktTusur.Media.Infrastructure.Services;

public class TextTransformer : ITextTransformer
{
	public string MakeShorter(string text, int maxLength = 0, int maxParagraphs = 0)
	{
		//TODO:
		return text;
	}
}