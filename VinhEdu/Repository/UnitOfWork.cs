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
        private GenericRepository<Configure> configRepository;
        private GenericRepository<ClassMember> memberRepository;
        private GenericRepository<Subject> subjectRepository;
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
        public GenericRepository<Configure> ConfigRepository
        {
            get
            {
                if (configRepository == null)
                {
                    configRepository = new GenericRepository<Configure>(context);
                }
                return configRepository;
            }
        }
        public GenericRepository<ClassMember> MemberRepository
        {
            get
            {
                if (memberRepository == null)
                {
                    memberRepository = new GenericRepository<ClassMember>(context);
                }
                return memberRepository;
            }
        }
        public GenericRepository<Subject> SubjectRepository
        {
            get
            {
                if (subjectRepository == null)
                {
                    subjectRepository = new GenericRepository<Subject>(context);
                }
                return subjectRepository;
            }
        }
        internal void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}