namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VinhEdu.Models;
    using static VinhEdu.Models.AdditionalDefinition;

    internal sealed class Configuration : DbMigrationsConfiguration<VinhEdu.Models.EduVinhContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VinhEdu.Models.EduVinhContext context)
        {
            //ADMIN 
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 1,
                Email = "thaondm@gmail.com",
                StudentID = "ABC",
                FullName = "Admin",
                Type = UserType.Admin,
                Role = "admin",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2019-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,
            }); ;
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 2,
                Email = "ngocnguyen123@gmail.com",
                StudentID = "ngocnguyen123@gmail.com",
                FullName = "Nguyễn Ngọc",
                Type = UserType.HeadMaster,
                Role = "headmaster",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2019-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,
                
            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 3,
                Email = "ngocnguyenabc@gmail.com",
                StudentID = "ngocnguyenabc@gmail.com",
                FullName = "Hiệu Trưởng 2",
                Type = UserType.HeadMaster,
                Role = "headmaster",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2019-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Male,

            });
            this.SeedData(context);
        }
        private void SeedData(VinhEdu.Models.EduVinhContext context)
        {
            //Base class
            context.BaseClassLists.AddOrUpdate(e => e.ID, new BaseClassList
            {
                ID = 1,
                ClassName = "6A"
            });
            context.BaseClassLists.AddOrUpdate(e => e.ID, new BaseClassList
            {
                ID = 2,
                ClassName = "6B"
            });
            //Thêm môn học
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 1,
                SubjectName = "Toán"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 2,
                SubjectName = "Ngữ Văn"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 3,
                SubjectName = "Anh Văn"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 4,
                SubjectName = "Sinh Học"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 5,
                SubjectName = "Lịch Sử"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 6,
                SubjectName = "Hóa Học"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 7,
                SubjectName = "Vật Lý"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 8,
                SubjectName = "GDCD"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 9,
                SubjectName = "Thể Dục"
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 10,
                SubjectName = "Địa Lý"
            });
            // Thêm config
            context.Configures.AddOrUpdate(e => e.ID, new Configure
            {
                ID = 1,
                IsActive = true,
                SchoolYear = "2019 - 2020"
            });
            context.Configures.AddOrUpdate(e => e.ID, new Configure
            {
                ID = 2,
                IsActive = false,
                SchoolYear = "2020 - 2021"
            });
            context.Configures.AddOrUpdate(e => e.ID, new Configure
            {
                ID = 3,
                IsActive = false,
                SchoolYear = "2021 - 2022"
            });
            context.Configures.AddOrUpdate(e => e.ID, new Configure
            {
                ID = 4,
                IsActive = false,
                SchoolYear = "2019 - 2020"
            });
            context.Configures.AddOrUpdate(e => e.ID, new Configure
            {
                ID = 5,
                IsActive = true,
                SchoolYear = "2019 - 2020"
            });
            // Thêm Trường
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 1,
                SchoolName = "THCS Đặng Thai Mai",
                HeadMasterID = 2,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 2,
                SchoolName = "THCS Quang Trung",
                HeadMasterID = 3,
            });
            // Thêm Gíao viên 4 - 7
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 4,
                Email = "giaovien1@gmail.com",
                StudentID = "giaovien1@gmail.com",
                SubjectID = 1,
                FullName = "Giáo Viên Toán",
                Type = UserType.Teacher,
                Role = "teacher",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("1980-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,
            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 5,
                Email = "giaovien2@gmail.com",
                StudentID = "giaovien2@gmail.com",
                FullName = "Giáo Viên Văn",
                SubjectID = 2,
                Type = UserType.Teacher,
                Role = "teacher",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("1980-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,
            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 6,
                Email = "giaovien3@gmail.com",
                StudentID = "giaovien3@gmail.com",
                FullName = "Giáo Viên Anh",
                Type = UserType.Teacher,
                SubjectID = 3,
                Role = "teacher",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("1980-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,
            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 7,
                Email = "giaovien4@gmail.com",
                StudentID = "giaovien4@gmail.com",
                FullName = "Nguyễn Ngọc",
                Type = UserType.Teacher,
                SubjectID = 4,
                Role = "teacher",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2019-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,

            });
            // Thêm Lớp 1 -2
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 1,
                SchoolID = 1,
                ConfigureID = 1,
                //HomeRoomTeacherID = 4,
                ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 2,
                SchoolID = 1,
                ConfigureID = 1,
                //HomeRoomTeacherID = 4,
                ClassName = "6B",
            });
            // Thêm học sinh 8 - 12
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 8,
                StudentID = "STD008",
                Email = "STD008",
                FullName = "Đặng Văn Quang",
                Type = UserType.Student,
                Role = "student",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2008-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Male,

            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 9,
                StudentID = "STD009",
                Email = "STD009",
                FullName = "Đặng Văn Quý",
                Type = UserType.Student,
                Role = "student",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2008-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Male,

            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 10,
                StudentID = "STD010",
                Email = "STD010",
                FullName = "Đặng Văn Quế",
                Type = UserType.Student,
                Role = "student",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2008-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Male,

            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 11,
                StudentID = "STD011",
                Email = "STD011",
                FullName = "Lâm Tâm Như",
                Type = UserType.Student,
                Role = "student",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2008-10-09"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,

            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 12,
                StudentID = "STD012",
                Email = "STD012",
                FullName = "Lăng Thanh Trúc",
                Type = UserType.Student,
                Role = "student",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2008-01-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,

            });
            //Thêm thành viên Lớp
            //Chủ nhiệm
            context.ClassMembers.AddOrUpdate(e =>e.ClassID,new ClassMember{
                ClassID = 1,
                UserID = 4,
                IsHomeTeacher = true,
                IsCurrent = true,
            });
            // Giao viên
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 5,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 6,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 7,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
            // Học sinh của lớp
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 8,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 9,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 10,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 11,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 12,
                IsHomeTeacher = false,
                IsCurrent = true,
            });
        }
        
    }
}
