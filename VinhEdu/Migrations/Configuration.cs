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
                Identifier = "thaondm@gmail.com",
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
                Identifier = "ngocnguyen123@gmail.com",
                SchoolID = 1,
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
                Identifier = "ngocnguyenabc@gmail.com",
                FullName = "Hiệu Trưởng 2",
                SchoolID = 2,
                Type = UserType.HeadMaster,
                Role = "headmaster",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2019-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Male,

            });
            //Thông tin sở
            context.Settings.AddOrUpdate(e => e.ID, new Setting
            {
                ID = 1,
                OrganizationName = "Phòng GD & ĐT Thành Phố Vinh",
                Semester = Semester.HK1
            });
            this.SeedData(context);
            SeedData Seeder = new SeedData();
            Seeder.Seed(context);
        }
        private void SeedData(VinhEdu.Models.EduVinhContext context)
        {
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
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 3,
                SchoolName = "THCS Hà Huy Tập",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 4,
                SchoolName = "THCS Trung Đô",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 5,
                SchoolName = "THCS Đội Cung",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 6,
                SchoolName = "THCS Lê lợi",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 7,
                SchoolName = "THCS Bến Thủy",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 8,
                SchoolName = "THCS Hưng Dũng",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 9,
                SchoolName = "THCS Hưng Lộc",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 10,
                SchoolName = "THCS Nghi Ân",
                HeadMasterID = 3,
            });
            context.Schools.AddOrUpdate(e => e.SchoolID, new School
            {
                SchoolID = 11,
                SchoolName = "THCS Nghi Kim",
                HeadMasterID = 3,
            });
            // Thêm Gíao viên 4 - 7
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 4,
                Identifier = "giaovien1@gmail.com",
                SubjectID = 1,
                SchoolID = 1,
                FullName = "Nguyễn Văn Toán",
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
                SchoolID = 1,
                Identifier = "giaovien2@gmail.com",
                FullName = "Hoàng Thị Văn",
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
                SchoolID = 1,
                Identifier = "giaovien3@gmail.com",
                FullName = "Đặng Ngọc Ngữ",
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
                Identifier = "giaovien4@gmail.com",
                FullName = "Nguyễn Ngọc",
                SchoolID = 2,
                Type = UserType.Teacher,
                SubjectID = 4,
                Role = "teacher",
                Status = UserStatus.Activated,
                CreateDate = DateTime.Parse("2019-09-09"),
                DateOfBirth = DateTime.Parse("2019-12-12"),
                Password = "0192023A7BBD73250516F069DF18B500", // = admin123
                Gender = Gender.Female,

            });
            // Thêm Lớp 1 - 4
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 1,
                SchoolID = 1,
                //HomeRoomTeacherID = 4,
                ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 2,
                SchoolID = 1,
                ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 3,
                SchoolID = 1,
                ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 4,
                SchoolID = 1,
                ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 5,
                SchoolID = 1,
                ClassName = "7A",
            });
 

            // Thêm học sinh 8 - 12
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 8,
                Identifier = "STD008",
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
                Identifier = "STD009",
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
                Identifier = "STD010",
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
                Identifier = "STD011",
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
                Identifier = "STD012",
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
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 4,
                IsHomeTeacher = true,
                ConfigureID = 1,
            });
            // Giao viên
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 5,
                IsHomeTeacher = false,
                ConfigureID = 1,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 6,
                IsHomeTeacher = false,
                ConfigureID = 1,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 7,
                IsHomeTeacher = false,
                ConfigureID = 1,
            });
            // Học sinh của lớp
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 8,
                IsHomeTeacher = false,
                ConfigureID = 1,
                LearnStatus = LearnStatus.Learning,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 9,
                IsHomeTeacher = false,
                ConfigureID = 1,
                LearnStatus = LearnStatus.Learning,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 10,
                IsHomeTeacher = false,
                ConfigureID = 1,
                LearnStatus = LearnStatus.Learning,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 11,
                IsHomeTeacher = false,
                LearnStatus = LearnStatus.Learning,
                ConfigureID = 1,
            });
            context.ClassMembers.AddOrUpdate(e => e.ClassID, new ClassMember
            {
                ClassID = 1,
                UserID = 12,
                IsHomeTeacher = false,
                ConfigureID = 1,
                LearnStatus = LearnStatus.Learning,
            });
        }
        

    }
}
