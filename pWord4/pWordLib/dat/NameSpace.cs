using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pWordLib.dat
{
    // explain what ICloneabl does
    // https://docs.microsoft.com/en-us/dotnet/api/system.icloneable?view=netframework-4.8
    [Serializable()]
    public class NameSpace : ICloneable
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public string URI_PREFIX { get; set; }
        public string URI_SUFFIX { get; set; }


                #region ICloneable Members
        /// <summary>
        /// Shallow copy
        /// </summary>
        /// <returns></returns>#region ICloneable Members

        public object Clone()
        {
            NameSpace ns = new NameSpace();
            ns.Prefix = (ns.Prefix != null) ? (String)this.Prefix.Clone() : null;
            ns.Suffix = (ns.Suffix != null) ? (String)this.Suffix.Clone() : null;
            ns.URI_PREFIX = (ns.URI_PREFIX != null) ? (String)this.URI_PREFIX.Clone() : null;
            ns.URI_SUFFIX = (ns.URI_SUFFIX != null) ? (String)this.URI_SUFFIX.Clone() : null;
            return ns;
        }

        #endregion
    }
}
