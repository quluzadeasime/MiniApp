using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;

namespace Core.Entities

{
    public class Classroom : Check
    {
        private static int _id;
        private string _name;
        public Student[] Students;
        public int StudenLimitBack;
        public int StudenLimitFront;
        public int Count
        {
            get
            {
                return Students.Length;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (!ClassroomName(value))
                {
                    throw new ClassroomException("Enter correct name!(AA100)");
                }
                _name = value;
            }
        }


        public int Id { get; set; }
        public Classroom()
        {
            
        }
        public Classroom(string name, int frontCount, int backCount)
        {
            Name = name;
            StudenLimitBack = backCount;
            StudenLimitFront = frontCount;
            Students = new Student[0];
            Id = ++_id;
        }

        public void AddStudent(Student student)
        {
            StudentLimit(Students, StudenLimitFront, StudenLimitBack);
            Array.Resize(ref Students, Students.Length + 1);
            Students[Students.Length - 1] = student;
        }

        public Student[] GetAllStudents()
        {
            return Students; 
        }

        public Student[] RemoveStudent(int id)
        {
            Student[] newStudents = { };
            foreach (Student student in Students)
            {
                if(student.Id == id)
                {
                    continue;
                }
                else
                {
                    Array.Resize(ref newStudents, newStudents.Length + 1);
                    newStudents[newStudents.Length - 1] = student;
                }
            }
            if(Students.Length == newStudents.Length)
            {
                throw new StudentException("There is no student id in database!");
            }
            return newStudents;
        }

        public Student GetStudentById(int id)
        {
            foreach (Student student in Students)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }
            throw new StudentException("Student Not Found!!");
        }

    }
}
