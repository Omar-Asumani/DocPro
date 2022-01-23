using System;

namespace DocPro.DTO
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string Name { get; set; }
        public byte[] Binary { get; set; }
        public int? KindID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
