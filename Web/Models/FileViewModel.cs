using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileBytes { get; set; }
        public bool IsPrivate { get; set; }
        public int DownloadsCount { get; set; }
        public string ApplicationUserId { get; set; }
        public string OwnerName { get; set; }
    }
}