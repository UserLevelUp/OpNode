using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pWordLib.dat.ellipsis
{
    public struct EllipsisFile
    {
        public string FileName { get; set; }
        public Type FileType { get; set; }
        public byte[] FileBuf { get; set; }
        public DateTime Created { get; set; }
    }
}
