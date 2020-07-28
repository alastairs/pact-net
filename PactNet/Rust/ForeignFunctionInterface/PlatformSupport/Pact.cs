namespace PactNet.Rust.ForeignFunctionInterface.PlatformSupport
{
    /// <summary>
    /// Wrapper for a <see cref="PactHandle" />.
    /// </summary>
    internal class Pact
    {
        public PactHandle Handle { get; }

        internal Pact() => Handle = new PactHandle();
    }

    /// <summary>
    /// Wrapper for an <see cref="InteractionHandle" />.
    /// </summary>
    internal class Interaction
    {
        public InteractionHandle Handle { get; }

        internal Interaction(Pact pact)
        {
            Handle = new InteractionHandle
            {
                pact = pact.Handle.pact
            };
        }
    }
}