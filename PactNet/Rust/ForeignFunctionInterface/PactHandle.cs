using System;
using System.Runtime.InteropServices;

namespace PactNet.Rust.ForeignFunctionInterface
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PactHandle
    {
        public UIntPtr pact;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct InteractionHandle
    {
        public UIntPtr pact;
        public UIntPtr interaction;
    }

    // TODO: how does one import an enum from a native library in C#?
    internal enum InteractionPart
    {
        Request,
        Response
    }
}
