using TaktTusur.Media.BackgroundCrawling.Core.Interfaces;

namespace TaktTusur.Media.BackgroundCrawling.Infrastructure.Services;

public class TextTransformer : ITextTransformer
{
	public string MakeShorter(string text, int maxLength = 0, int maxParagraphs = 0)
	{
		//TODO:
		return text;
	}
}