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
        private string _name;
        public Student[] Students;
        public int StudenLimitBack;
        public int StudenLimitFront;
        public int Id { get; set; }
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

        public Classroom()
        {
            Students = new Student[0];
        }

        public void AddStudent(Student student)
        {
            StudentLimit(StudenLimitBack, StudenLimitFront, Students);
            Array.Resize(ref Students, Students.Length + 1);
            Students[Students.Length - 1] = student;

            if (student.Role == Role.Frontend) Console.WriteLine("New frontend student has been created!\n");
            else Console.WriteLine("New backend student hass been created!\n");
        }

        public Student[] GetAllStudents()
        {
            return Students;
        }

        public void RemoveStudent(int id)
        {
            Student[] newStudents = new Student[0];
            bool check = false;


            for (int i = 0; i < Students.Length; i++)
            {
                if (Students[i].Id == id)
                {
                    check = true;
                    continue;
                }
                else
                {
                    Array.Resize(ref newStudents, newStudents.Length + 1);
                    newStudents[newStudents.Length - 1] = Students[i];
                }
            }

            Students = newStudents;

            if (!check)
            {
                throw new StudentException("There is no student id in database!");
            }
            else
            {
                Console.WriteLine("Student hass been deleted!");
            }
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
