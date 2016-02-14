using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        public ApplicationContext db { get; set; }

        public FileRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(FileEntity item)
        {
            db.Files.Add(item);
        }

        public void Delete(FileEntity item)
        {
            db.Files.Remove(item);
        }

        public void Update(FileEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public FileEntity GetFileById(int id)
        {
            return db.Files.Find(id);
        }

        public IEnumerable<FileEntity> GetAllFiles()
        {
            return db.Files;
        }

        public FileEntity GetFileByName(string name)
        {
            FileEntity fileEntity = db.Files.FirstOrDefault(file => file.FileName == name);
            return fileEntity;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
