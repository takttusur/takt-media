using Quartz;
using TaktTusur.Media.BackgroundCrawler.QuartzJobs;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddQuartz((options) =>
        {
            options.AddJob<ArticlesFetchingQuartzJob>(JobKey.Create(nameof(ArticlesFetchingQuartzJob)));
        });
        services.AddQuartzHostedService((options) =>
        {
            options.WaitForJobsToComplete = true;
            options.AwaitApplicationStarted = true;
        });
    })
    .Build();

host.Run();
