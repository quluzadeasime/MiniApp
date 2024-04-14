using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace MiniConsoleapp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Classroom classroom = new Classroom();
            bool running = true;

            Console.WriteLine("========== Welcome Classroom App ==========\n");
            while (running)
            {
                if (classroom.Name == null)
                {
                    Console.WriteLine("\n\tCreate Classroom Section (Back - b)\n");
                    Console.WriteLine("1. Give a name for classroom");
                    Console.WriteLine("0. Exit");
                    Console.Write("\nInput: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int number))
                    {
                        switch (number)
                        {
                            case 0:
                                running = false;
                                break;
                            case 1:
                                CreateClassroom(classroom);
                                break;
                            default:
                                Console.WriteLine("\nYou must enter numbers between 0-1!\n");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou must enter numbers!\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\n\tWelcome to {classroom.Name} (Back - b)\n");
                    Console.WriteLine("1. Add Student");
                    Console.WriteLine("2. Get All Students information");
                    Console.WriteLine("3. Get by Id Student information");
                    Console.WriteLine("4. Delete Student");
                    Console.WriteLine("0. Exit");
                    Console.Write("\nInput: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int number))
                    {
                        switch (number)
                        {
                            case 0:
                                running = false;
                                break;
                            case 1:
                                AddStudent(classroom);
                                break;
                            case 2:
                                GetAllStudents(classroom);
                                break;
                            case 3:
                                GetStudentById(classroom);
                                break;
                            case 4:
                                DeleteStudent(classroom);
                                break;
                            default:
                                Console.WriteLine("\nYou must enter numbers between 0-4!\n");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou must enter numbers!\n");
                    }
                }
            }
            Console.WriteLine("\t Program has been stopped");

        }

        public static void CreateClassroom(Classroom classroom)
        {
            Console.WriteLine("\n\t Give A Name For Classroom");

        PATH1:
            Console.Write("\nClassroom Name: ");
            string name = Console.ReadLine().Trim();

            if (name == "b")
            {
                if (classroom.Name != null) classroom.Name = null;
                return;
            }

            try
            {
                classroom.Name = name;
            }
            catch (ClassroomException ex)
            {
                Console.WriteLine("\n" + ex.Message);
                goto PATH1;
            }

        PATH2:
            Console.Write("Frontend student limit: ");
            string fLimit = Console.ReadLine().Trim();

            if (fLimit == "b") goto PATH1;

            Console.Write("Backend student limit: ");
            string bLimit = Console.ReadLine().Trim();

            if (bLimit == "b") goto PATH1;


            if (int.TryParse(bLimit, out int bnumber) && int.TryParse(fLimit, out int fnumber))
            {
                classroom.StudenLimitFront = fnumber;
                classroom.StudenLimitBack = bnumber;
            }
            else
            {
                Console.WriteLine("\nPlease, enter number for front and back groups!\n");
                goto PATH2;
            }

            Console.WriteLine("\nClassroom has been created!");
        }

        public static void AddStudent(Classroom classroom)
        {
            Console.WriteLine("\n\tAdd Student Section\n");

            Student student = new Student();

        PATH1:
            Console.Write("Student First Name: ");
            string inputFirtName = Console.ReadLine().Trim();

            if (inputFirtName == "b") return;

            student.Name = inputFirtName;

        PATH2:
            Console.Write("Student Last Name: ");
            string inputLastName = Console.ReadLine().Trim();

            if (inputLastName == "b") goto PATH1;

            student.Surname = inputLastName;

        PATH3:
            Console.Write("Student Age: ");
            string inputAge = Console.ReadLine().Trim();

            if (inputAge == "b") goto PATH2;

            if (byte.TryParse(inputAge, out byte age))
            {
                student.Age = age;
                if (age < 18 || age > 70)
                {
                    goto PATH3;
                }
            }
            else
            {
                Console.WriteLine("\nYou must enter number between 18-70\n");
            }


        PATH4:
            Console.WriteLine("\n\tStudent Roles\n");
            Console.WriteLine("1. Frontend");
            Console.WriteLine("2. Backend");
            Console.Write("Student Role Number: ");

            string inputRole = Console.ReadLine().Trim();

            if (inputRole == "b") goto PATH3;

            if (int.TryParse(inputRole, out int role))
            {
                switch (role)
                {
                    case 1:
                        student.Role = Role.Frontend;
                        break;
                    case 2:
                        student.Role = Role.Backend;
                        break;
                    default:
                        Console.WriteLine("\nYou must enter number between 1-4!\n");
                        goto PATH4;
                }
            }
            else
            {
                Console.WriteLine("\nYou must enter numbers!\n");
                goto PATH4;
            }

            try
            {
                classroom.AddStudent(student);
            }
            catch (ClassroomException ex)
            {
                Console.WriteLine("\n" + ex.Message + "\n");
            }
        }

        public static void DeleteStudent(Classroom classroom)
        {
            Console.WriteLine("\n\tDelete Student Section\n");
        PATH1:
            Console.Write("Enter Student ID: ");
            string id = Console.ReadLine().Trim();

            if (id == "b") return;

            if (int.TryParse(id, out int StudentId))
            {
                try
                {
                    classroom.RemoveStudent(StudentId);
                }
                catch (StudentException ex)
                {
                    Console.WriteLine("\n" + ex.Message);
                    goto PATH1;
                }
            }
            else
            {
                Console.WriteLine("\nPlease, enter a number for Student id!\n");
                goto PATH1;
            }
        }

        public static void GetStudentById(Classroom oldClassroom)
        {
            Console.WriteLine("\n\tGet By Id Student Section\n");

        PATH1:
            Console.Write("Enter Student ID: ");
            string id = Console.ReadLine().Trim();

            if (id == "b") return;

            Console.WriteLine();
            if (int.TryParse(id, out int StudentId))
            {
                Student emp = oldClassroom.GetStudentById(StudentId);

                try
                {
                    Console.WriteLine(emp.ToString());
                }
                catch (StudentException ex)
                {
                    Console.WriteLine("\n" + ex.Message);
                    goto PATH1;
                }

            }
            else
            {
                Console.WriteLine("Please, enter a number for Student id!\n");
                goto PATH1;
            }

        }

        public static void GetAllStudents(Classroom classroom)
        {
            Console.WriteLine("\n\tGet All Student Section\n");

        PATH1:
            Console.WriteLine("Student Roles\n");
            Console.WriteLine("1. All students");
            Console.WriteLine("2. Frontend students");
            Console.WriteLine("3. Backend students");
            Console.Write("\nYour choice: ");

            string choice = Console.ReadLine().Trim();

            if (choice == "b") return;

            Console.WriteLine();
            if (int.TryParse(choice, out int input))
            {
                switch (input)
                {
                    case 1:
                        foreach (Student student in classroom.GetAllStudents())
                        {
                            Console.WriteLine(student.ToString());
                        }
                        break;
                    case 2:
                        foreach (Student student in classroom.GetAllStudents())
                        {
                            if (student.Role == Role.Frontend)
                            {
                                Console.WriteLine(student.ToString());
                            }
                        }
                        break;
                    case 3:
                        foreach (Student student in classroom.GetAllStudents())
                        {
                            if (student.Role == Role.Backend)
                            {
                                Console.WriteLine(student.ToString());
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("\nYou must enter number between 1-3!\n");
                        goto PATH1;
                }
            }
            else
            {
                Console.WriteLine("\nYou must enter numbers!\n");
                goto PATH1;
            }
        }
    }
}
