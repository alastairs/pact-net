// ReSharper disable InconsistentNaming

using System.Runtime.InteropServices;

namespace PactNet.Rust.ForeignFunctionInterface
{
    internal class PactMockServer
    {
        private const string PactMockServerDllName = "pact_mock_server_ffi-windows-x86_64.dll";

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void init(string log_env_var);

        // ReSharper disable once IdentifierTypo
        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern int create_mock_server(string pact_str, string addr_str, bool tls);

        // ReSharper disable once IdentifierTypo
        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern int create_mock_server(PactHandle pact, string addr_str, bool tls);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern bool mock_server_matched(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern string mock_server_mismatches(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern bool cleanup_mock_server(int mock_server_port);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern int write_pact_file(int mock_server_port, string directory);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern PactHandle new_pact(string consumer_name, string provider_name);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern InteractionHandle new_interaction(PactHandle pact, string description);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void upon_receiving(InteractionHandle interaction, string description);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void given(InteractionHandle interaction, string description);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void given_with_param(InteractionHandle interaction, string description, string name,
            string value);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_request(InteractionHandle interaction, string method, string path);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_query_parameter(InteractionHandle interaction, string name, int index,
            string value);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_header(InteractionHandle interaction, InteractionPart part, string name,
            int index, string value);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void response_status(InteractionHandle interaction, ushort status);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_body(InteractionHandle interaction, InteractionPart part, string content_type,
            string body);

        // generate_datetime_string -> StringResult

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern bool check_regex(string regex, string example);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_binary_file(InteractionHandle interaction, InteractionPart part,
            string content_type, string body, int size);

        [DllImport(PactMockServerDllName, CharSet = CharSet.Unicode)]
        internal static extern void with_multipart_file(InteractionHandle interaction, InteractionPart part,
            string content_type, string file, string part_name);
    }
}
