using Microsoft.Extensions.Options;
using TaktTusur.Media.Core.Interfaces;
using TaktTusur.Media.Core.News;
using TaktTusur.Media.Core.Services;
using TaktTusur.Media.Core.Settings;

namespace TaktTusur.Media.Infrastructure.Jobs;

public class ArticlesReplicationJob(
	IRemoteSource<Article> remoteSource,
	IArticlesRepository repository,
	ILogger<ArticlesReplicationJob> logger,
	IOptionsSnapshot<ReplicationJobConfiguration> jobSettings,
	IOptions<TextRestrictions> textRestrictions,
	ITextTransformer textTransformer,
	IEnvironment environment)
	: ReplicationJobBase<Article>(remoteSource, repository, logger, jobSettings.Get(nameof(ArticlesReplicationJob)))
{
	private const string BrokenArticleMessage = "The article {originalId} from {originalSource} is broken.";
	
	protected override bool TryUpdateExistingItem(Article remoteItem)
	{
		var localArticle = repository.GetByOriginalId(remoteItem.OriginalId, remoteItem.OriginalSource);
		if (localArticle == null) return false;
		if (remoteItem.OriginalUpdatedAt == localArticle.OriginalUpdatedAt) return true;
		var settings = textRestrictions.Value;
		
		localArticle.OriginalUpdatedAt = remoteItem.OriginalUpdatedAt;
		localArticle.Text =
			textTransformer.MakeShorter(remoteItem.Text, settings.ShortArticleMaxSymbolsCount, settings.ShortArticleMaxParagraphs);
		localArticle.LastUpdated = environment.GetCurrentDateTime();
		
		repository.Update(localArticle);
		return true;
	}

	protected override void AddNewItem(Article item)
	{
		item.Text = textTransformer.MakeShorter(item.Text);
		item.LastUpdated = environment.GetCurrentDateTime();
		
		repository.Add(item);
	}

	protected override void ProcessBrokenItems(Queue<Article> brokenItemsQueue)
	{
		while (brokenItemsQueue.TryDequeue(out var article))
		{
			logger.LogInformation(BrokenArticleMessage, article.OriginalId, article.OriginalSource);
		}
	}
}