using System;
using System.Runtime.InteropServices;

namespace PactNet.Rust.ForeignFunctionInterface
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InteractionHandle
    {
        public UIntPtr pact;
        public UIntPtr interaction;
    }
}