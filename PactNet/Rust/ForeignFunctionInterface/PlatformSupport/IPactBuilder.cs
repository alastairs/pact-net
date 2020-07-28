namespace PactNet.Rust.ForeignFunctionInterface.PlatformSupport
{
    /// <summary>
    /// Enables control of the lifecycle of a Pact definition.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface must be implemented by internal types calling the Rust
    /// reference library, and the implementation of this interface should only
    /// delegate to the native method of the same name.
    /// </para>
    /// </remarks>
    internal interface IPactBuilder
    {
        Pact NewPact(string consumerName, string providerName);

        Interaction NewInteraction(Pact pact, string description);

        void UponReceiving(Interaction interaction, string description);

        void Given(Interaction interaction, string description);

        void GivenWithParam(Interaction interaction, string description, string name, string value);

        void WithRequest(Interaction interaction, string method, string path);

        void WithQueryParameter(Interaction interaction, string name, int index, string value);

        // TODO: InteractionPart is not part of this namespace - and rightly so.
        void WithHeader(Interaction interaction, InteractionPart part, string name, int index, string value);

        void ResponseStatus(Interaction interaction, int status);

        // TODO: InteractionPart is not part of this namespace - and rightly so.
        void WithBody(Interaction interaction, InteractionPart part, string contentType, string body);

        // TODO: InteractionPart is not part of this namespace - and rightly so.
        void WithBinaryFile(Interaction interaction, InteractionPart part, string contentType, string body, int size);

        // TODO: InteractionPart is not part of this namespace - and rightly so.
        void WithMultipartFile(Interaction interaction, InteractionPart part, string contentType, string file, string partName);

        // TODO: GenerateDateTimeString?
    }

    internal interface IMatchers
    {
        bool CheckRegex(string regex, string example);

        // TODO: GenerateDateTimeString?
    }
}