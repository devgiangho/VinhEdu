using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VinhEdu.Models;

namespace VinhEdu.Migrations
{
    public class SeedData
    {
        public void Seed(VinhEdu.Models.EduVinhContext context)
        {
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 6,
                SchoolID = 1,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 7,
                SchoolID = 1,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 8,
                SchoolID = 1,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 9,
                SchoolID = 1,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 10,
                SchoolID = 1,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 11,
                SchoolID = 1,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 12,
                SchoolID = 1,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 13,
                SchoolID = 1,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 14,
                SchoolID = 1,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 15,
                SchoolID = 1,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 16,
                SchoolID = 1,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 17,
                SchoolID = 2,
                //HomeRoomTeacherID = 4,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 18,
                SchoolID = 2,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 19,
                SchoolID = 2,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 20,
                SchoolID = 2,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 21,
                SchoolID = 2,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 22,
                SchoolID = 2,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 23,
                SchoolID = 2,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 24,
                SchoolID = 2,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 25,
                SchoolID = 2,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 26,
                SchoolID = 2,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 27,
                SchoolID = 2,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 28,
                SchoolID = 2,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 29,
                SchoolID = 2,
                Grade = Grade.G9,ClassName = "9A",
            });

            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 30,
                SchoolID = 2,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 31,
                SchoolID = 2,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 32,
                SchoolID = 2,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 33,
                SchoolID = 3,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 34,
                SchoolID = 3,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 35,
                SchoolID = 3,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 36,
                SchoolID = 3,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 37,
                SchoolID = 3,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 38,
                SchoolID = 3,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 39,
                SchoolID = 3,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 40,
                SchoolID = 3,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 41,
                SchoolID = 3,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 42,
                SchoolID = 3,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 43,
                SchoolID = 3,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 44,
                SchoolID = 3,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 45,
                SchoolID = 3,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 46,
                SchoolID = 3,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 47,
                SchoolID = 3,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 48,
                SchoolID = 3,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 49,
                SchoolID = 4,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 50,
                SchoolID = 4,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 51,
                SchoolID = 4,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 52,
                SchoolID = 4,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 53,
                SchoolID = 4,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 54,
                SchoolID = 4,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 55,
                SchoolID = 4,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 56,
                SchoolID = 4,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 57,
                SchoolID = 4,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 58,
                SchoolID = 4,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 59,
                SchoolID = 4,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 60,
                SchoolID = 4,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 61,
                SchoolID = 4,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 62,
                SchoolID = 4,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 63,
                SchoolID = 4,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 64,
                SchoolID = 4,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 65,
                SchoolID = 5,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 66,
                SchoolID = 5,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 67,
                SchoolID = 5,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 68,
                SchoolID = 5,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 69,
                SchoolID = 5,
                Grade = Grade.G7,ClassName = "7A",
            });

            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 70,
                SchoolID = 5,
                Grade = Grade.G7,ClassName = "7B",
            });

            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 71,
                SchoolID = 5,
                Grade = Grade.G7,ClassName = "7C",
            });

            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 72,
                SchoolID = 5,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 73,
                SchoolID = 5,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 74,
                SchoolID = 5,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 75,
                SchoolID = 5,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 76,
                SchoolID = 5,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 77,
                SchoolID = 5,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 78,
                SchoolID = 5,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 79,
                SchoolID = 5,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 80,
                SchoolID = 5,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 80,
                SchoolID = 5,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 81,
                SchoolID = 6,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 82,
                SchoolID = 6,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 83,
                SchoolID = 6,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 84,
                SchoolID = 6,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 85,
                SchoolID = 6,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 86,
                SchoolID = 6,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 87,
                SchoolID = 6,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 88,
                SchoolID = 6,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 89,
                SchoolID = 6,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 90,
                SchoolID = 6,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 91,
                SchoolID = 6,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 92,
                SchoolID = 6,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 93,
                SchoolID = 6,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 94,
                SchoolID = 6,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 95,
                SchoolID = 6,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 96,
                SchoolID = 6,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 97,
                SchoolID = 7,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 98,
                SchoolID = 7,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 99,
                SchoolID = 7,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 100,
                SchoolID = 7,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 101,
                SchoolID = 7,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 102,
                SchoolID = 7,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 103,
                SchoolID = 7,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 104,
                SchoolID = 7,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 105,
                SchoolID = 7,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 106,
                SchoolID = 7,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 107,
                SchoolID = 7,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 108,
                SchoolID = 7,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 109,
                SchoolID = 7,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 110,
                SchoolID = 7,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 111,
                SchoolID = 7,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 112,
                SchoolID = 7,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 113,
                SchoolID = 8,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 114,
                SchoolID = 8,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 115,
                SchoolID = 8,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 116,
                SchoolID = 8,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 117,
                SchoolID = 8,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 118,
                SchoolID = 8,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 119,
                SchoolID = 8,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 120,
                SchoolID = 8,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 121,
                SchoolID = 8,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 122,
                SchoolID = 8,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 123,
                SchoolID = 8,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 124,
                SchoolID = 8,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 125,
                SchoolID = 8,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 126,
                SchoolID = 8,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 127,
                SchoolID = 8,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 128,
                SchoolID = 8,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 129,
                SchoolID = 9,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 130,
                SchoolID = 9,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 131,
                SchoolID = 9,
                Grade = Grade.G6,ClassName = "6C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 132,
                SchoolID = 9,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 133,
                SchoolID = 9,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 134,
                SchoolID = 9,
                Grade = Grade.G7,ClassName = "7B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 135,
                SchoolID = 9,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 136,
                SchoolID = 9,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 137,
                SchoolID = 9,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 138,
                SchoolID = 9,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 139,
                SchoolID = 9,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 140,
                SchoolID = 9,
                Grade = Grade.G8,ClassName = "8D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 141,
                SchoolID = 9,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 142,
                SchoolID = 9,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 143,
                SchoolID = 9,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 144,
                SchoolID = 9,
                Grade = Grade.G9,ClassName = "9D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 145,
                SchoolID = 10,
                Grade = Grade.G6,ClassName = "6A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 146,
                SchoolID = 10,
                Grade = Grade.G6,ClassName = "6B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 147,
                SchoolID = 10,
                Grade = Grade.G6,ClassName = "6C",
            });

            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 148,
                SchoolID = 10,
                Grade = Grade.G6,ClassName = "6D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 149,
                SchoolID = 10,
                Grade = Grade.G7,ClassName = "7A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 150,
                SchoolID = 10,
                Grade = Grade.G7,ClassName = "7B",
            }); context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 151,
                SchoolID = 10,
                Grade = Grade.G7,ClassName = "7C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 152,
                SchoolID = 10,
                Grade = Grade.G7,ClassName = "7D",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 153,
                SchoolID = 10,
                Grade = Grade.G8,ClassName = "8A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 154,
                SchoolID = 10,
                Grade = Grade.G8,ClassName = "8B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 155,
                SchoolID = 10,
                Grade = Grade.G8,ClassName = "8C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 156,
                SchoolID = 10,
                Grade = Grade.G8,ClassName = "8D",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 157,
                SchoolID = 10,
                Grade = Grade.G9,ClassName = "9A",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 158,
                SchoolID = 10,
                Grade = Grade.G9,ClassName = "9B",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 159,
                SchoolID = 10,
                Grade = Grade.G9,ClassName = "9C",
            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 160,
                SchoolID = 10,
                Grade = Grade.G9,ClassName = "9D",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 161,
                SchoolID = 11,
                Grade = Grade.G6,ClassName = "6A",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 162,
                SchoolID = 11,
                Grade = Grade.G6,ClassName = "6B",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 163,
                SchoolID = 11,
                Grade = Grade.G6,ClassName = "6C",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 164,
                SchoolID = 11,
                Grade = Grade.G6,ClassName = "6D",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 165,
                SchoolID = 11,
                Grade = Grade.G7,ClassName = "7A",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 166,
                SchoolID = 11,
                Grade = Grade.G7,ClassName = "7B",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 167,
                SchoolID = 11,
                Grade = Grade.G7,ClassName = "7C",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 168,
                SchoolID = 11,
                Grade = Grade.G7,ClassName = "7D",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 169,
                SchoolID = 11,
                Grade = Grade.G8,ClassName = "8A",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 170,
                SchoolID = 11,
                Grade = Grade.G8,ClassName = "8B",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 171,
                SchoolID = 11,
                Grade = Grade.G8,ClassName = "8C",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 172,
                SchoolID = 11,
                Grade = Grade.G8,ClassName = "8D",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 173,
                SchoolID = 11,
                Grade = Grade.G9,ClassName = "9A",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 174,
                SchoolID = 11,
                Grade = Grade.G9,ClassName = "9B",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 175,
                SchoolID = 11,
                Grade = Grade.G9,ClassName = "9C",

            });
            context.Classes.AddOrUpdate(e => e.ClassID, new Class
            {
                ClassID = 176,
                SchoolID = 11,
                Grade = Grade.G9,ClassName = "9D",

            });
        }
    }
}