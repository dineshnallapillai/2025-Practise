using MockLunitAnalysis;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<FlowDispatcherService>();
        services.AddSingleton<DemoModeFlowHandler>();
        services.AddSingleton<WebApiHost>();
    })
    .UseWindowsService() 
    .Build()
    .Run();