namespace TaktTusur.Media.Core.Interfaces;

/// <summary>
/// Transforms text.
/// </summary>
public interface ITextTransformer
{
	/// <summary>
	/// Cut the text to make it shorter
	/// </summary>
	/// <param name="text">Original text</param>
	/// <param name="maxLength">Max length of short version, 0 - unlimited</param>
	/// <param name="maxParagraphs">Max amount of paragraphs for short version, 0 - unlimited</param>
	/// <returns>Short text</returns>
	string MakeShorter(string text, int maxLength = 0, int maxParagraphs = 0);
}