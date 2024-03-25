
using TaktTusur.Media.Core.Settings;

namespace TaktTusur.Media.Worker.Configuration;

/// <summary>
/// Settings for <see cref="NewsReplicationJobConfiguration"/>
/// </summary>
public class NewsReplicationJobConfiguration : JobConfigurationBase
{
	/// <summary>
	/// The VK group identifier to import news
	/// </summary>
	public string SourceIdentifier { get; set; }
	
	/// <summary>
	/// The maximum symbols count to import article text
	/// 0 - not limited
	/// </summary>
	public int MaxSymbolsCount { get; set; }
	
	/// <summary>
	/// The maximum of paragraphs count to import article text
	/// </summary>
	public int MaxParagraphCount { get; set; }
	
	/// <summary>
	/// Max articles which can be processed in one execution
	/// </summary>
	public int MaxArticlesCount { get; set; }
	
	/// <summary>
	/// The amount of articles which will be commited in one transaction
	/// </summary>
	public int CommitBuffer { get; set; }
}