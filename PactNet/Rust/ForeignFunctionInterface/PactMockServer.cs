// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

using System.Runtime.InteropServices;

namespace PactNet.Rust.ForeignFunctionInterface
{
    internal class PactMockServer
    {
        private const string PactMockServerDllName = @"Rust\ForeignFunctionInterface\libpact_mock_server_ffi-windows-x86_64.dll";

#if NETSTANDARD2_1
        private const UnmanagedType StringType = UnmanagedType.LPUTF8Str;
#else
        private const UnmanagedType StringType = UnmanagedType.LPStr; // AnsiBStr also works
#endif

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void init(string log_env_var);

        // ReSharper disable once IdentifierTypo
        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern int create_mock_server(
            [MarshalAs(StringType)] string pact_str,
            [MarshalAs(StringType)] string addr_str,
            bool tls);

        // ReSharper disable once IdentifierTypo
        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern int create_mock_server_for_pact(
            PactHandle pact,
            [MarshalAs(StringType)] string addr_str,
            bool tls);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern bool mock_server_matched(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        [return:MarshalAs(StringType)]
        internal static extern string mock_server_mismatches(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern bool cleanup_mock_server(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern int write_pact_file(
            int mock_server_port,
            [MarshalAs(StringType)] string directory);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern PactHandle new_pact(
            [MarshalAs(StringType)] string consumer_name,
            [MarshalAs(StringType)] string provider_name);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern InteractionHandle new_interaction(
            PactHandle pact,
            [MarshalAs(StringType)] string description);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void upon_receiving(
            InteractionHandle interaction,
            [MarshalAs(StringType)] string description);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void given(
            InteractionHandle interaction,
            [MarshalAs(StringType)] string description);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void given_with_param(
            InteractionHandle interaction,
            [MarshalAs(StringType)] string description,
            [MarshalAs(StringType)] string name,
            [MarshalAs(StringType)] string value);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_request(
            InteractionHandle interaction,
            [MarshalAs(StringType)] string method,
            [MarshalAs(StringType)] string path);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_query_parameter(
            InteractionHandle interaction,
            [MarshalAs(StringType)] string name,
            [MarshalAs(StringType)] int index,
            [MarshalAs(StringType)] string value);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_header(
            InteractionHandle interaction,
            InteractionPart part,
            [MarshalAs(StringType)] string name,
            int index,
            [MarshalAs(StringType)] string value);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void response_status(InteractionHandle interaction, ushort status);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_body(
            InteractionHandle interaction,
            InteractionPart part,
            [MarshalAs(StringType)] string content_type,
            [MarshalAs(StringType)] string body);

        // generate_datetime_string -> StringResult

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern bool check_regex(
            [MarshalAs(StringType)] string regex,
            [MarshalAs(StringType)] string example);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_binary_file(
            InteractionHandle interaction,
            InteractionPart part,
            [MarshalAs(StringType)] string content_type,
            [MarshalAs(StringType)] string body,
            int size);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_multipart_file(
            InteractionHandle interaction,
            InteractionPart part,
            [MarshalAs(StringType)] string content_type,
            [MarshalAs(StringType)] string file,
            [MarshalAs(StringType)] string part_name);
    }
}
