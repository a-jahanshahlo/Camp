using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comps.DomainLayer
{
    public class Binary : DelEntity
    {

        public string Name { get; set; }
        public string Extention { get; set; }
        public long? Size { get; set; }
        public DateTime  UploadDate { get; set; }
        public byte[] FileBinary { get; set; }
        public Guid Guid { get; set; }
        public string Desctiption { get; set; }

        public virtual  IList<Gallery> Galleries { get; set; }
    }
}