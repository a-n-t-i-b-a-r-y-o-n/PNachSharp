using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PNachSharp
{
    class RustFFI
    {
        [DllImport(@"ffi.dll")]
        private static extern uint sum_of_even(int[] n, UIntPtr len);

        public static uint SumOfEven(int[] n)
        {
            return sum_of_even(n, (UIntPtr)n.Length);
        }
    }
}
