using ETL.Database.DataMigrating.Extractors;
using ETL.Database.DataMigrating.Loaders;
using ETL.Database.DataMigrating.Transformers;
using ETL.Database.Loaders;
using ETL.Database.Pipelines;
using ETL.Database.Source;
using ETL.Database.Target;
using ETL.Database.Transformers;
using ETL.Database.Util.Source;
using ETL.Database.Util.Target;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();

var serviceProvider = new ServiceCollection()
    .AddDbContext<SourceDbContext>()
    .AddDbContext<TargetDbContext>()
    .AddTransient<IDataExtractor<SourceDbContext>, DatabaseExtractor<SourceDbContext>>()
    .AddTransient<IDataTransformer, BaseDataTransformer>()
    .AddTransient<IDataLoader<TargetDbContext>, DatabaseLoader<TargetDbContext>>()
    .AddTransient<IETLDatabaseToDatabasePipelineProvider<SourceDbContext, TargetDbContext>, ETLDatabaseToDatabasePipelineProvider<SourceDbContext, TargetDbContext>>()
    .BuildServiceProvider();

var pipelineProvider = serviceProvider.GetRequiredService<IETLDatabaseToDatabasePipelineProvider<SourceDbContext, TargetDbContext>>();

CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(1)).Token;

await pipelineProvider
    .Extract(async (context, token) =>
    {
        await Task.Delay(1000);
        //return context.SourceData.FirstAsync(cancellationToken);
        return Task.FromResult(new SourceData() { Id = 1 });
    })
    .Transform(async  (data, token) =>
    {
        await Task.Delay(2000);
        return Task.FromResult(new TargetData
        {
            Id = data.Id + 1,
        });
    })
    .Load(async (context, data, token) =>
    {
        await Task.Delay(3000);
        //context.Add(data);
        //context.SaveChangesAsync();
    })
    .StartAsync(cancellationToken);