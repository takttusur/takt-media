using Quartz;
using TaktTusur.Media.BackgroundCrawler.QuartzJobs;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var newsJobKey = JobKey.Create(nameof(NewsFetchingQuartzJob));
        services.AddQuartz((options) =>
        {
            options.AddJob<NewsFetchingQuartzJob>(newsJobKey, configurator =>
            {
                configurator.DisallowConcurrentExecution();
            }).AddTrigger(c =>
            {
                c.StartNow().ForJob(newsJobKey);
            });
        });
        services.AddQuartzHostedService((options) =>
        {
            options.WaitForJobsToComplete = true;
            options.AwaitApplicationStarted = true;
        });
    })
    .Build();

host.Run();
