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
        private UnitOfWork Instance;
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

        internal void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}