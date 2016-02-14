using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class FileDTO
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
