using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PNachSharp
{
    public class PNachFile : IDisposable
    {
        // FFI
        [DllImport(@"ffi.dll", EntryPoint = "PNachFile_New")]
        private static extern PNachFileHandle PNachFile_New(string game_title, string game_crc);

        [DllImport("ffi.dll", EntryPoint = "PNachFile_ToString")]
        private static extern RustFFI.RustStringHandle PNachFile_ToString(PNachFileHandle pnachfile);

        // Internal SafeHandle
        private readonly PNachFileHandle _handle;

        // Internal properties
        public String Title { get; set; }
        public String CRC { get; set; }
        public List<PNachCode> Codes { get; set; }

        public PNachFile(string title, string crc)
        {
            _handle = PNachFile_New(title, crc);
        }

        public override string ToString() => PNachFile_ToString(_handle).ToString();


        public void Dispose() => _handle.Dispose();
    }

    internal class PNachFileHandle : SafeHandle
    {
        // FFI
        [DllImport("ffi.dll", EntryPoint = "PNachFile_Free")]
        private static extern void PNachFile_Free(IntPtr handle);

        // Generic constructor
        public PNachFileHandle() : base(IntPtr.Zero, true) { }

        // Give Rust ownership to free the memory
        protected override bool ReleaseHandle()
        {
            if (!this.IsInvalid)
            {
                PNachFile_Free(this.handle);
            }

            return true;
        }

        public override bool IsInvalid => this.handle == IntPtr.Zero;
    }
}
