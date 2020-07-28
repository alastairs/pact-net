using System;

namespace PactNet.Rust.ForeignFunctionInterface.PlatformSupport
{
    /// <summary>
    /// Enables control of the lifecycle of a mock provider for a given Pact
    /// definition.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface must be implemented by internal types calling the Rust
    /// reference library, and the implementation of this interface should only
    /// delegate to the native method of the same name. The port number used by
    /// the mock provider is the handle for the server itself, and should be
    /// encapsulated by the implementing class to pass to the external methods
    /// exposed by the Rust native library.
    /// </para>
    /// <para>
    /// Interface implementations should call the <c>init</c> native method from
    /// their constructors, following
    /// <a href="https://docs.rs/pact_mock_server_ffi/0.0.7/pact_mock_server_ffi/fn.init.html">
    /// the function's documentation</a>.
    /// </para>
    /// <para>
    /// <strong>Note:</strong> the implementation must follow the entire pattern
    /// for <see cref="IDisposable" /> implementations, in order to avoid
    /// leaking memory through handles to the native library. As well as the
    /// implementation of <see cref="IDisposable.Dispose" />, the implementing
    /// class must also define a destructor to call
    /// <see cref="IDisposable.Dispose" />.
    /// </para>
    /// </remarks>
    internal interface IMockProvider : IDisposable
    {
        /// <summary>
        /// Creates an instance of the Mock Server to run the supplied Pact
        /// definition.
        /// </summary>
        /// <param name="pactJson"></param>
        /// <param name="serverAddress"></param>
        /// <param name="enableTls"></param>
        /// <returns></returns>
        void CreateMockServer(string pactJson, string serverAddress, bool enableTls);

        /// <summary>
        /// Creates an instance of the Mock Server to run the supplied Pact
        /// definition.
        /// </summary>
        /// <param name="pact"></param>
        /// <param name="serverAddress"></param>
        /// <param name="enableTls"></param>
        /// <returns></returns>
        void CreateMockServer(Pact pact, string serverAddress, bool enableTls);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool MockServerMatched();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string MockServerMismatches();

        /// <summary>
        ///
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        int WritePactFile(string directory);
    }
}