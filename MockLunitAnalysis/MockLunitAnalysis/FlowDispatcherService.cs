using Philips.PmsGxr.CxaInterfaces.Flows;
using Philips.PmsGxr.GeneralFramework.MessageHandler;
using Philips.PmsGxr.Logging.LoggingToolkit;

namespace MockLunitAnalysis
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class FlowDispatcherService : BackgroundService
    {
        private readonly ILogger<FlowDispatcherService> _logger;
        private readonly DemoModeFlowHandler _handler;

        public FlowDispatcherService(ILogger<FlowDispatcherService> logger, DemoModeFlowHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting Flow Dispatcher...");

            const int originatorId = 501328;
            IGFLoggingFactory loggerFactory = Philips.PmsGxr.GlueRegistry.Instance.GetService<IGFLoggingFactory>();
            IGFLogger glueLogger = loggerFactory.GetLogger(originatorId);
            var dispatcher = new QueueingFlowDispatcher(glueLogger, FlowDispatcherConfiguration.FieldService);

            dispatcher.Add<GfintIndDemoMode>(_ =>
            {
                _logger.LogInformation("Received DemoMode flow.");
                _handler.OnDemoModeFlowReceived();
            });

            dispatcher.StartDispatchingOfQueuedFlows();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

}
