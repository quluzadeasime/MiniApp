using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Student
    {
        private byte _age;
        private static int _id;
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Id { get; set; }
        public Role Role { get; set; }
        public byte Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value < 18 || value > 70)
                {
                    Console.WriteLine("\nStudent age must be at least 18!\n");
                }
                else
                {
                    _age = value;
                }
            }
        }

        public Student()
        {
            Id = ++_id;

        }

        public override string ToString()
        {
            return $"| Id: {Id} | Name: {Name} | Surname: {Surname} | Age: {Age} | Role: {Role} |";

        }

    }
}
