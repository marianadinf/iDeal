using System;
using System.Runtime.InteropServices;

namespace UIT.iDeal.TestLibrary.ProcessLifecycleManagement
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
        public UInt32 nLength;
        public IntPtr lpSecurityDescriptor;
        public Int32 bInheritHandle;
    }
}