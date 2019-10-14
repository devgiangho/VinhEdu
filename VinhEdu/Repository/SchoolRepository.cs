using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VinhEdu.Models;

namespace VinhEdu.Repository
{
    public class SchoolRepository
    {
        EduVinhContext context;
        public SchoolRepository(EduVinhContext context)
        {
            this.context = context;
        }
        public School FindByID(int id)
        {
            return context.Schools.Find(id);
        }
        public void Add(School model)
        {
            context.Schools.Add(model);
        }
        public IQueryable<School> GetAll()
        {
            IQueryable<School> query = context.Schools;
            return query.AsQueryable();
        }
        public void Delete(School model)
        {
            context.Schools.Remove(model);

        }
        public void Update(School model)
        {
            context.Entry(model).State = EntityState.Modified;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}