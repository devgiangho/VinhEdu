using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VinhEdu.Models;

namespace VinhEdu.Repository
{
    public class UserRepository
    {
        EduVinhContext context;
        public UserRepository(EduVinhContext context)
        {
            this.context = context;
        }

        public User FindByStudentID(string id)
        {
            User u = context.Users.Where(e => e.StudentID.ToLower() == id.ToLower()).FirstOrDefault();
            return u;
        }
        public bool CheckExistByStudentID(string id)
        {
            return context.Users.Any(e => e.StudentID.ToLower() == id.ToLower());
        }
        public User FindByEmail(string email)
        {
            User u = context.Users.Where(e => e.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return u;
        }
        public bool CheckExistByEmail(string email)
        {
            return context.Users.Any(e => e.Email.ToLower() == email.ToLower());
        }
        public void AddUser(User model)
        {
            context.Users.Add(model);
        }
        public IQueryable<User> AllUser()
        {
            IQueryable<User> query = context.Users;
            return query.AsQueryable();
        }
        public void DeleteUser(User model)
        {
            context.Users.Remove(model);

        }
        public void UpdateUser(User model)
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