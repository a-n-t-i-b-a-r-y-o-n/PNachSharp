using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PNachSharp
{
    public static class RustFFI
    {
        internal class RustStringHandle : SafeHandle
        {
            // FFI
            [DllImport("ffi.dll", EntryPoint = "RustString_Free")]
            private static extern void RustString_Free(IntPtr handle);

            // Generic constructor, set Invalid Value to IntPtr.Zero
            public RustStringHandle() : base(IntPtr.Zero, true) {}

            protected override bool ReleaseHandle()
            {
                if (!IsInvalid)
                {
                    RustString_Free(handle);
                }

                return true;
            }

            public override bool IsInvalid => handle == IntPtr.Zero;

            public override string ToString()
            {
                // Calculate input length
                int length = 0;
                while (Marshal.ReadByte(handle, length) != 0) { ++length; }
                // Create buffer of calculated length
                byte[] buffer = new byte[length];
                // Copy data into buffer up until *buffer* length
                Marshal.Copy(handle, buffer, 0, buffer.Length);
                // Return UTF-8 string from buffer
                return Encoding.UTF8.GetString(buffer);
            }
        }

        public class RustString : IDisposable
        {
            // FFI
            [DllImport("ffi.dll", EntryPoint = "RustString_New")]
            private static extern RustStringHandle RustString_New(string input);

            // Internal SafeHandle
            private readonly RustStringHandle _handle;

            RustString(string input)
            {
                _handle = RustString_New(input);
            }

            public void Dispose() => _handle.Dispose();

            public override string ToString() => _handle.ToString();
        }

    }
}
