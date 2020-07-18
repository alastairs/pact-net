using System;
using PactNet.Core;

namespace PactNet.Rust
{
    using static ForeignFunctionInterface.PactMockServer;

    public class PactMockServer : IPactCoreHost, IDisposable
    {
        public int Port { get; private set; } = 0;

        public void Start()
        {
            init("PACT_LOG_LEVEL");
            Port = create_mock_server("path to pact", $"{Port}", false);
        }

        public void Stop()
        {
            Dispose();
        }

        private void ReleaseUnmanagedResources()
        {
            cleanup_mock_server(Port);
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~PactMockServer()
        {
            ReleaseUnmanagedResources();
        }
    }
}
