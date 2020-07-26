using System;
using System.Security;
using Newtonsoft.Json;
using PactNet.Configuration.Json;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;

namespace PactNet.Rust
{
    using static ForeignFunctionInterface.PactMockServer;

    /// <remarks>
    /// The rust library lifecycle is different from the Ruby implementation.
    /// The lifecycle is:
    /// 1. init()
    /// 2. create_mock_server expects you to pass the already generated Pact
    ///    (using the standardised format) to indicate the interactions that the
    ///    current test will support, or you could use
    ///    create_mock_server_for_pact to get a PactHandle and build it up that
    ///    way
    /// 3. You would then have the test code execute the test against the server
    ///    you just started
    /// 4. Call mock_server_matched to see if the test matched everything
    /// 5. Call mock_server_mismatches if there was mismatches and display to
    ///    the user
    /// 6. write_pact_file (only if the test is successful
    /// 7. Call cleanup_mock_server to shut things down
    /// </remarks>
    public class PactBuilder : IPactBuilder, IDisposable
    {
        public string ConsumerName { get; private set; }

        public string ProviderName { get; private set; }

        public int Port { get; private set; }

        public PactBuilder() => init("PACT_LOG_LEVEL");

        public IPactBuilder ServiceConsumer(string consumerName)
        {
            ConsumerName = string.IsNullOrWhiteSpace(consumerName)
                ? throw new ArgumentException(nameof(consumerName))
                : consumerName;

            return this;
        }

        public IPactBuilder HasPactWith(string providerName)
        {
            ProviderName = string.IsNullOrWhiteSpace(providerName)
                ? throw new ArgumentException(nameof(providerName))
                : providerName;

            return this;
        }

        public IMockProviderService MockService(int port, bool enableSsl = false, IPAddress host = IPAddress.Loopback,
            string sslCert = null, string sslKey = null)
        {
            return MockService(port, JsonConfig.ApiSerializerSettings, enableSsl, host, sslCert, sslKey);
        }

        public IMockProviderService MockService(int port, JsonSerializerSettings jsonSerializerSettings, bool enableSsl = false,
            IPAddress host = IPAddress.Loopback, string sslCert = null, string sslKey = null)
        {
            var address = host switch
            {
                IPAddress.Loopback => $"127.0.0.1:{port}",
                IPAddress.Any => $"0.0.0.0:{port}",
                _ => throw new NotSupportedException($"Enum value {host} is not supported")
            };

            var portInUse = create_mock_server("path to pact", address, false);
            Port = portInUse switch
            {
                -1 => throw new ArgumentException(message: "A null pointer was received"),
                -2 => throw new JsonException("The pact JSON could not be parsed"),
                -3 => throw new InvalidOperationException("The mock server could not be started"),
                -4 => throw new Exception("The method panicked"),
                -5 => throw new ArgumentException("The address is not valid"),
                -6 => throw new SecurityException(
                    "Could not create the TLS configuration with the self-signed certificate"),
                _ => port
            };

            return null;
        }

        public void Build()
        {
            new_pact(ConsumerName, ProviderName);
            write_pact_file(Port, Constants.PactPath);
            Dispose();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            cleanup_mock_server(Port);
        }

        ~PactBuilder()
        {
            ReleaseUnmanagedResources();
        }
    }
}
