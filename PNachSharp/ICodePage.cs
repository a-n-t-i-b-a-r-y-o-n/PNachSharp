using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNachSharp
{
    public interface ICodePage
    {
        public enum PageType { Raw, Encoded }
        public abstract PageType GetPageType();
        public abstract string GetRawCode();
    }
}
