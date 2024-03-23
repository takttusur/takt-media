using Quartz;
using TaktTusur.Media.BackgroundCrawler.QuartzJobs;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddQuartz((options) =>
        {
            options.AddJob<NewsFetchingQuartzJob>(JobKey.Create(nameof(NewsFetchingQuartzJob)));
        });
        services.AddQuartzHostedService((options) =>
        {
            options.WaitForJobsToComplete = true;
            options.AwaitApplicationStarted = true;
        });
    })
    .Build();

host.Run();
