using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PNachSharp.Annotations;

namespace PNachSharp
{
    internal class FFI
    {
        // PNachFile factory
        [DllImport(@"ffi.dll", EntryPoint = "PNachFile_New")]
        internal static extern PNachFileHandle PNachFile_New(string game_title, string game_crc);
        // PNachFile Destructor
        [DllImport("ffi.dll", EntryPoint = "PNachFile_Free")]
        internal static extern void PNachFile_Free(IntPtr handle);
        // PNachFile ToString() formatter
        [DllImport("ffi.dll", EntryPoint = "PNachFile_ToString")]
        internal static extern RustFFI.RustStringHandle PNachFile_ToString(PNachFileHandle pnachfile);
        // PNachFile Title accessor
        [DllImport("ffi.dll", EntryPoint = "PNachFile_GetTitle")]
        internal static extern RustFFI.RustStringHandle PNachFile_GetTitle(PNachFileHandle pnachfile);
        // PNachFile Title mutator
        [DllImport("ffi.dll", EntryPoint = "PNachFile_SetTitle")]
        internal static extern void PNachFile_SetTitle(PNachFileHandle pnachfile, string title);
        // PNachFile CRC accessor
        [DllImport("ffi.dll", EntryPoint = "PNachFile_GetCRC")]
        internal static extern RustFFI.RustStringHandle PNachFile_GetCRC(PNachFileHandle pnachfile);
        // PNachFile CRC mutator
        [DllImport("ffi.dll", EntryPoint = "PNachFile_SetCRC")]
        internal static extern void PNachFile_SetCRC(PNachFileHandle pnachfile, string crc);
    }
    public class PNachFile : DependencyObject, IDisposable, INotifyPropertyChanged
    {
        // Internal SafeHandle
        private readonly PNachFileHandle _handle;

        // Property Changed event
        public event PropertyChangedEventHandler PropertyChanged;

        // Indicates if current PNachFile has been changed since the last save
        public static readonly DependencyProperty FileChangedProperty =
            DependencyProperty.Register("FileChanged", typeof(bool), typeof(PNachFile));
        public bool FileChanged
        {
            get => (bool) GetValue(FileChangedProperty);
            set
            {
                if (FileChanged == value) return;
                SetValue(FileChangedProperty, value);
                OnPropertyChanged();
            }
        }

        // PNachFile game title
        public string Title
        {
            get => FFI.PNachFile_GetTitle(_handle).ToString();
            set
            {
                if (Title == value) return;
                FFI.PNachFile_SetTitle(_handle, value);
                OnPropertyChanged();
            }
        }

        // PNachFile game CRC code
        public string CRC
        {
            get => FFI.PNachFile_GetCRC(_handle).ToString();
            set
            {
                if (CRC == value) return;
                FFI.PNachFile_SetCRC(_handle, value);
                OnPropertyChanged();
            }
        }

        // PNachFile code list
        public static readonly DependencyProperty CodesProperty =
            DependencyProperty.Register("Codes", typeof(List<PNachCode>), typeof(PNachFile));

        public List<PNachCode> Codes
        {
            get => (List<PNachCode>) GetValue(CodesProperty);
            set
            {
                if (Codes == value) return;
                SetValue(CodesProperty, value);
                OnPropertyChanged();
            }
        }

        public PNachFile(string title, string crc)
        {
            _handle = FFI.PNachFile_New(title, crc);
        }

        // Print using Rust PNachFile.to_string() formatting
        public override string ToString() => FFI.PNachFile_ToString(_handle).ToString();

        // Free the PNachFile resource
        public void Dispose() => _handle.Dispose();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class PNachFileHandle : SafeHandle
    {
        // Generic constructor
        public PNachFileHandle() : base(IntPtr.Zero, true) { }

        // Give Rust ownership to free the memory when disposing
        protected override bool ReleaseHandle()
        {
            // Give ownership back to Rust to free the memory if it's valid
            if (!IsInvalid)
                FFI.PNachFile_Free(this.handle);
            return true;
        }

        public override bool IsInvalid => this.handle == IntPtr.Zero;
    }
}
