namespace MockLunitAnalysis
{
    public class DemoModeFlowHandler
    {
        private readonly WebApiHost _apiHost;
        private bool _hasStarted;

        public DemoModeFlowHandler(WebApiHost apiHost)
        {
            _apiHost = apiHost;
        }

        public void OnDemoModeFlowReceived()
        {
            if (!_hasStarted)
            {
                _hasStarted = true;
                _apiHost.RunWebApiAsync(CancellationToken.None).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("Failed to start Web API: " + task.Exception?.GetBaseException().Message);
                    }
                    else
                    {
                        Console.WriteLine("Web API started successfully.");
                    }
                });
            }
        }
    }

}
