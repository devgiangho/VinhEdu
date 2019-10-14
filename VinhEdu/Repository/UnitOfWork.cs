using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinhEdu.Models;

namespace VinhEdu.Repository
{
    public class UnitOfWork
    {
        private EduVinhContext context = new EduVinhContext();
        private UserRepository userRepository;
        private SchoolRepository schoolRepository;
        private GenericRepository<Class> classRepository;
        //private UnitOfWork Instance;
        //public UnitOfWork GetInstance()
        //{
        //    if (Instance == null)
        //    {
        //        Instance = new UnitOfWork();
        //    }
        //    return Instance;
        //}
        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }
        public SchoolRepository SchoolRepository
        {
            get
            {
                if (schoolRepository == null)
                {
                    schoolRepository = new SchoolRepository(context);
                }
                return schoolRepository;
            }
        }
        public GenericRepository<Class> ClassRepository
        {
            get
            {
                if (classRepository == null)
                {
                    classRepository = new GenericRepository<Class>(context);
                }
                return classRepository;
            }
        }
        internal void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}