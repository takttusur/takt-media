using System.ComponentModel.DataAnnotations;

namespace TaktTusur.Media.BackgroundCrawling.Core.Settings;

public class TextRestrictions
{
	[Required]
	public int ShortArticleMaxSymbolsCount { get; set; }
	
	[Required]
	public int ShortArticleMaxParagraphs { get; set; }
}