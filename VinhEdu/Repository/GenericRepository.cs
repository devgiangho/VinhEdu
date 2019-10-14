using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VinhEdu.Models;

namespace VinhEdu.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T FindByID(int id);
        void Add(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EduVinhContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new EduVinhContext();
            table = _context.Set<T>();
        }
        public GenericRepository(EduVinhContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return table.AsQueryable();
        }
        public T FindByID(int id)
        {
            return table.Find(id);
        }
        public void Add(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}