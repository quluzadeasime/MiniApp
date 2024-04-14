using Core.Entities;
using Core.Exceptions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace MiniConsoleapp
{
    internal class Program
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
                    Console.WriteLine("4. Get by Search Student information");
                    Console.WriteLine("5. Delete Student");
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

                            default:
                                Console.WriteLine("\nYou must enter numbers between 0-5!\n");
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

        static void CreateClassroom(Classroom classroom)
        {
            Console.WriteLine("\n\t Give A Name For Classroom");

            PATH1:
            Console.Write("\nClassroom Name: ");
            string name = Console.ReadLine().Trim();

            if (name == "b") return;

            try
            {
                classroom.Name = name;
            }
            catch(ClassroomException ex)
            {
                Console.WriteLine(ex.Message);
                goto PATH1;
            }

            Console.Write("Frontend student limit: ");
            string fLimit = Console.ReadLine().Trim();

            if (fLimit == "b") return;

            if (int.TryParse(fLimit, out int number))
            {
                classroom.StudenLimitFront = number;
            }
            

            classroom.Name = name;
            Console.WriteLine("\nClassroom has been created!");
        }

    }
}
