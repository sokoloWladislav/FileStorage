using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private ApplicationUserManager userManager;
        private FileRepository fileRepository;

        public UnitOfWork(string conectionString)
        {
            db = new ApplicationContext(conectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            fileRepository = new FileRepository(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IFileRepository FileRepository
        {
            get { return fileRepository; }
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    fileRepository.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
