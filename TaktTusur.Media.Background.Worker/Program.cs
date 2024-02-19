using System.Configuration;
using Microsoft.AspNetCore;
using Quartz;
using TaktTusur.Media.BackgroundCrawler.QuartzJobs;
using TaktTusur.Media.BackgroundCrawling.Core.Settings;
using TaktTusur.Media.BackgroundCrawling.Infrastructure;
using TaktTusur.Media.BackgroundCrawling.Infrastructure.FakeImplementations;
using TaktTusur.Media.BackgroundCrawling.Infrastructure.Jobs;
using TaktTusur.Media.Core.News;
using TaktTusur.Media.Infrastructure;

var host = Host.CreateApplicationBuilder(args);
host.AddWorkerInfrastructureLayer();

var services = host.Services;
services.AddFakeRemoteSources();
services.AddFakeRepositories();

services.AddOptionsWithValidateOnStart<TextRestrictions>()
	.BindConfiguration(nameof(TextRestrictions));

services.AddOptionsWithValidateOnStart<ReplicationJobSettings>(nameof(ArticlesReplicationJob))
	.BindConfiguration(nameof(ArticlesReplicationJob));

services.AddInfrastructureLayer(host.Configuration);
services.Configure<QuartzOptions>(host.Configuration.GetSection("Quartz"));

services.AddScoped<ArticlesReplicationJob>();
        
services.AddQuartz((options) =>
{
	options
		.AddJob<QuartzJobDecorator<ArticlesReplicationJob>>(configurator =>
		{
			configurator.WithIdentity("ArticlesReplication")
				.DisallowConcurrentExecution();
		});
            
	options.AddTrigger(opts => opts
		.ForJob("ArticlesReplication") // link to the HelloWorldJob
		.WithIdentity("ArticlesReplication-trigger") // give the trigger a unique name
		.WithCronSchedule("0/30 * * * * ?")); // run every 30 seconds
});
services.AddQuartzHostedService((options) =>
{
	options.WaitForJobsToComplete = true;
	options.AwaitApplicationStarted = true;
});

host.Build().Run();
