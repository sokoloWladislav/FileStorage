using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface IFileService : IDisposable
    {
        OperationDetails CreateFile(FileDTO file);
        OperationDetails DeleteFile(FileDTO file);
        void IncrementDownloadCount(int id);
        FileDTO GetFileById(int id);
        FileDTO GetFileByName(string name);
        IEnumerable<FileDTO> GetAllFiles();
        IEnumerable<FileDTO> GetAllUserFiles(string userId);
        IEnumerable<FileDTO> GetAllPublicFiles();
    }
}
