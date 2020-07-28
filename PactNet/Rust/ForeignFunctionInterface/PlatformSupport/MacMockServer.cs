#if NETFRAMEWORK || NETSTANDARD
using System;
using System.Runtime.InteropServices;

namespace PactNet.Rust.ForeignFunctionInterface.PlatformSupport
{
    internal class MacMockServer : IMockProvider
    {
        private const string PactMockServerDllName =
            @"Rust/ForeignFunctionInterface/lib/libpact_mock_server_ffi-osx-x86_64.dylib";

        private int _port;

        internal MacMockServer() => init();

        public void CreateMockServer(string pactJson, string serverAddress, bool enableTls) =>
            _port = create_mock_server(pactJson, serverAddress, enableTls);

        public void CreateMockServer(Pact pact, string serverAddress, bool enableTls) =>
            _port = create_mock_server_for_pact(pact.Handle, serverAddress, enableTls);

        public bool MockServerMatched() =>
            mock_server_matched(_port);

        public string MockServerMismatches() =>
            mock_server_mismatches(_port);

        public int WritePactFile(string directory) =>
            write_pact_file(_port, directory);

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources() => cleanup_mock_server(_port);

        ~MacMockServer()
        {
            ReleaseUnmanagedResources();
        }

        #region externs
        // ReSharper disable IdentifierTypo
        // ReSharper disable InconsistentNaming

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        private static extern void init();

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        private static extern void cleanup_mock_server(int port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        private static extern int create_mock_server(
            [MarshalAs(Constants.StringType)] string pact_str,
            [MarshalAs(Constants.StringType)] string addr_str,
            bool tls);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        private static extern int create_mock_server_for_pact(
            PactHandle pact,
            [MarshalAs(Constants.StringType)] string addr_str,
            bool tls);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        private static extern bool mock_server_matched(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        [return: MarshalAs(Constants.StringType)]
        private static extern string mock_server_mismatches(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        private static extern int write_pact_file(
            int mock_server_port,
            [MarshalAs(Constants.StringType)] string directory);

        // ReSharper restore IdentifierTypo
        // ReSharper restore InconsistentNaming
        #endregion
    }
}
#endif
