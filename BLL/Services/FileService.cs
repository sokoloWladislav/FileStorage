using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork db;
        
        private readonly IMapper mapper;

        public FileService(IUnitOfWork uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<FileEntity, FileDTO>(); });
            mapper = config.CreateMapper();
        }
        public OperationDetails CreateFile(FileDTO file)
        {
            FileEntity fileEntity = db.FileRepository.GetFileByName(file.FileName);
            if (fileEntity == null)
            {
                fileEntity = new FileEntity
                {
                    ApplicationUserId = file.ApplicationUserId,
                    FileBytes = file.FileBytes,
                    FileName = file.FileName,
                    IsPrivate = file.IsPrivate,
                    FileType = MIMEAssistant.GetMIMEType(file.FileName),
                    DownloadsCount = 0
                };
                db.FileRepository.Create(fileEntity);
                db.Commit();
                return new OperationDetails(true, "Файл успешно добавлен", "");
            }
            else return new OperationDetails(false, "Файл с таким именем уже существует", "FileName");
        }

        public OperationDetails DeleteFile(FileDTO file)
        {
            FileEntity fileEntity = db.FileRepository.GetFileById(file.Id);
            if (fileEntity != null)
            {
                db.FileRepository.Delete(fileEntity);
                db.Commit();
                return new OperationDetails(true, "Файл успешно удален", "");
            }
            else
                return new OperationDetails(false, "Файл, который должен быть удален отсутствует", "Id");
        }

        public FileDTO GetFileById(int id)
        {
            FileDTO file = mapper.Map<FileEntity, FileDTO>(db.FileRepository.GetFileById(id));
            SetOwnerName(file);
            return file;
        }

        public FileDTO GetFileByName(string name)
        {
            FileDTO file = mapper.Map<FileEntity, FileDTO>(db.FileRepository.GetFileByName(name));
            SetOwnerName(file);
            return file;
        }

        public IEnumerable<FileDTO> GetAllFiles()
        {
            var files = mapper.Map<IEnumerable<FileEntity>, List<FileDTO>>(db.FileRepository.GetAllFiles());
            SetOwnerName(files);
            return files;
        }

        public IEnumerable<FileDTO> GetAllUserFiles(string userId)
        {
            var files = mapper.Map<IEnumerable<FileEntity>, List<FileDTO>>(db.FileRepository.GetAllFiles());
            SetOwnerName(files.Where(item => item.ApplicationUserId == userId));
            return files.Where(item => item.ApplicationUserId == userId);

        }

        public IEnumerable<FileDTO> GetAllPublicFiles()
        {
            var files = mapper.Map<IEnumerable<FileEntity>, List<FileDTO>>(db.FileRepository.GetAllFiles());
            SetOwnerName(files.Where(item => item.IsPrivate == false));
            return files.Where(item => item.IsPrivate == false);
        }

        public void IncrementDownloadCount(int id)
        {
            FileEntity file = db.FileRepository.GetFileById(id);
            file.DownloadsCount++;
            db.FileRepository.Update(file);
            db.Commit();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private void SetOwnerName(FileDTO file)
        {
            if(file != null)
                file.OwnerName = db.UserManager.FindById(file.ApplicationUserId).UserName;
        }

        private void SetOwnerName(IEnumerable<FileDTO> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    file.OwnerName = db.UserManager.FindById(file.ApplicationUserId).UserName;
                }
            }
        }
    }
}
