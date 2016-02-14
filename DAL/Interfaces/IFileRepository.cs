using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IFileRepository : IDisposable
    {
        void Create(FileEntity item);
        void Delete(FileEntity item);
        void Update(FileEntity item);
        FileEntity GetFileByName(string name);
        FileEntity GetFileById(int id);
        IEnumerable<FileEntity> GetAllFiles();
    }
}
