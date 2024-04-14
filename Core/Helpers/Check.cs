using Core.Entities;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Helpers
{
    public class Check
    {
        public void StudentLimit(Student[] students, int frontCount, int backCount)
        {
            if (students.Length > (frontCount + backCount))
            {
                throw new ClassroomException("Frontend and backend groups are full!");
            }
            else
            {
                int fCount = 0, bCount = 0;
                foreach (Student student in students)
                {
                    if(student.Role == Enums.Role.Frontend)
                    {
                        fCount++;
                        if(fCount == frontCount)
                        {
                            throw new ClassroomException("Frontend group is full!");
                        }
                    }
                    if (student.Role == Enums.Role.Backend)
                    {
                        bCount++;
                        if (bCount == backCount)
                        {
                            throw new ClassroomException("Backend group is full!");
                        }
                    }

                }
            }
        }
        public bool Name(string name)
        {
            if (name == null || name == " ")
            {
                return false;
            }
            if (!char.IsUpper(name[0]) && name.Length < 3)
            {
                return false;
            }
            if (name.Split(' ').Length > 1) return false;
            return true;
        }

        public bool Surname(string surname)
        {
            if (surname == null || surname == " ")
            {
                return false;
            }
            if (!char.IsUpper(surname[0]) && surname.Length < 3)
            {
                return false;
            }
            if (surname.Split(' ').Length > 1) return false;
            return true;
        }

        public bool ClassroomName(string name)
        {
            if (name.Length != 5)
            {
                return false;
            }
            if (!char.IsUpper(name[0]) || !char.IsUpper(name[1]))
            {
                return false;
            }
            for (int i = 2; i < 5; i++)
            {
                if (!char.IsDigit(name[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
