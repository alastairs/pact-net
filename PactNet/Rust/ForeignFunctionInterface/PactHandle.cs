using System;
using System.Runtime.InteropServices;

namespace PactNet.Rust.ForeignFunctionInterface
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PactHandle
    {
        public UIntPtr pact;
    }
}