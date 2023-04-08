using System;
using System.Collections.Generic;
using MatriculationNumberSimulator.Faculties;


namespace MatriculationNumberSimulator
{
    public class SchoolPortal
    {
        static int numberOfStudents = 0;
        static List<Student> students = new List<Student>();
        static string matricNumber;


        public void CreateStudent()
        {
            int selectedFaculty = 0;
            Console.Clear();
            Console.Write("Enter fullname: ");
            string fullname = Console.ReadLine();
            if(fullname.Length == 0 || fullname == null)
            {
                DisplayMessage("Name cannot be empty");
                return;
            }
            selectedFaculty = SelectFaculty();
            if (selectedFaculty == -1)
            {
                DisplayMessage("Invalid Response...");

            } else
            {
                string selectedDepartment = SelectDepartment(selectedFaculty);
                if(selectedDepartment.Length == 0)
                {
                    DisplayMessage("Invalid Response...");
                } else
                {
                    students.Add(new Student(matricNumber, fullname, (Faculty)selectedFaculty, selectedDepartment));
                    numberOfStudents++;
                }
            }
        }

        private static void GenerateMatricNumber(Faculty faculty, int departmentCode, int departmentCount)
        {
            string year = DateTime.Now.Year.ToString().Substring(2);
            matricNumber = String.Format("{0:00}{1:00}{2:00}{3:000}", year, ((int)faculty) + 1, departmentCode + 1, departmentCount);
        }

        public void ListStudents()
        {
            Console.Clear();
            Console.WriteLine($"Total number of students: {numberOfStudents}");
            Console.WriteLine("1. List all students\n2. Filter by faculty and department\n3. Back to main menu\n");
            Console.Write("> : ");
            int choice = 0, index = 1; ;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        foreach(Student student in students)
                        {
                            Console.WriteLine($"{index}. {student.matricNumber} {student.fullname}, Faculty of {student.faculty.ToString().Replace('_', ' ')}, Department of {student.department.Replace('_', ' ')}");
                            index++;
                        }
                        DisplayMessage("Press any key to continue...");
                        break;
                    case 2:
                        int selectedFaculty = 0;
                        selectedFaculty = SelectFaculty();
                        string selectedDepartment = SelectDepartment(selectedFaculty);
                        foreach (Student student in students)
                        {
                            if(student.faculty == (Faculty)selectedFaculty && student.department == selectedDepartment)
                            {
                                Console.WriteLine($"{index}. {student.matricNumber} {student.fullname}, Faculty of {student.faculty.ToString().Replace('_', ' ')}, Department of {student.department.Replace('_', ' ')}");
                                index++;
                            }
                        }
                        DisplayMessage("Press any key to continue...");
                        break;
                    case 3:
                        return;
                    default:
                        DisplayMessage("Invalid Response...");
                        break;
                }
            }
        }

        private static int SelectFaculty()
        {
            Console.WriteLine("Select faculty: ");
            int index = 1;
            foreach (string faculty in Enum.GetNames(typeof(Faculty)))
            {
                Console.WriteLine($"[{index}] {faculty}");
                index++;
            }
            Console.Write("> : ");
            int selectedFaculty = 0;
            return (int.TryParse(Console.ReadLine(), out selectedFaculty) && selectedFaculty >= 1 && selectedFaculty <= 12) ? selectedFaculty - 1 : -1;


        }

        private static string SelectDepartment(int faculty)
        {
            Console.WriteLine("Select department: ");
            int index = 1;
            switch (faculty)
            {
                case 0:
                    foreach(string department in Enum.GetNames(typeof(Arts.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    int selectedDepartment = 0;
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        Arts.updateDepartmentStudentCount((Arts.Department) selectedDepartment - 1);
                        GenerateMatricNumber((Faculty) faculty, selectedDepartment - 1, Arts.studentCount[(Arts.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(Arts.Department), selectedDepartment - 1);
                    }
                    break;
                case 1:
                    foreach (string department in Enum.GetNames(typeof(BasicMedicalSciences.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        BasicMedicalSciences.updateDepartmentStudentCount((BasicMedicalSciences.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, BasicMedicalSciences.studentCount[(BasicMedicalSciences.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(BasicMedicalSciences.Department), selectedDepartment - 1);
                    }
                    break;
                case 2:
                    foreach (string department in Enum.GetNames(typeof(ClinicalServices.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        ClinicalServices.updateDepartmentStudentCount((ClinicalServices.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, ClinicalServices.studentCount[(ClinicalServices.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(ClinicalServices.Department), selectedDepartment - 1);
                    }
                    break;
                case 3:
                    foreach (string department in Enum.GetNames(typeof(DentalSciences.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        DentalSciences.updateDepartmentStudentCount((DentalSciences.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, DentalSciences.studentCount[(DentalSciences.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(DentalSciences.Department), selectedDepartment - 1);
                    }
                    break;
                case 4:
                    foreach (string department in Enum.GetNames(typeof(Education.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        Education.updateDepartmentStudentCount((Education.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, Education.studentCount[(Education.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(Education.Department), selectedDepartment - 1);
                    }
                    break;
                case 5:
                    foreach (string department in Enum.GetNames(typeof(Engineering.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        Engineering.updateDepartmentStudentCount((Engineering.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, Engineering.studentCount[(Engineering.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(Engineering.Department), selectedDepartment - 1);
                    }
                    break;
                case 6:
                    foreach (string department in Enum.GetNames(typeof(EnvironmentalSciences.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        EnvironmentalSciences.updateDepartmentStudentCount((EnvironmentalSciences.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, EnvironmentalSciences.studentCount[(EnvironmentalSciences.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(EnvironmentalSciences.Department), selectedDepartment - 1);
                    }
                    break;
                case 7:
                    foreach (string department in Enum.GetNames(typeof(Law.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        Law.updateDepartmentStudentCount((Law.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, Law.studentCount[(Law.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(Law.Department), selectedDepartment - 1);
                    }
                    break;
                case 8:
                    foreach (string department in Enum.GetNames(typeof(ManagementSciences.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        ManagementSciences.updateDepartmentStudentCount((ManagementSciences.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, ManagementSciences.studentCount[(ManagementSciences.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(ManagementSciences.Department), selectedDepartment - 1);
                    }
                    break;
                case 9:
                    foreach (string department in Enum.GetNames(typeof(Pharmacy.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        Pharmacy.updateDepartmentStudentCount((Pharmacy.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, Pharmacy.studentCount[(Pharmacy.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(Pharmacy.Department), selectedDepartment - 1);
                    }
                    break;
                case 10:
                    foreach (string department in Enum.GetNames(typeof(Sciences.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if ((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        Sciences.updateDepartmentStudentCount((Sciences.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, Sciences.studentCount[(Sciences.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(Sciences.Department), selectedDepartment - 1);
                    }
                    break;
                case 11:
                    foreach (string department in Enum.GetNames(typeof(SocialSciences.Department)))
                    {
                        Console.WriteLine($"[{index}] {department}");
                        index++;
                    }
                    Console.Write("> : ");
                    if((int.TryParse(Console.ReadLine(), out selectedDepartment) && selectedDepartment >= 1 && selectedDepartment <= index))
                    {
                        SocialSciences.updateDepartmentStudentCount((SocialSciences.Department)selectedDepartment - 1);
                        GenerateMatricNumber((Faculty)faculty, selectedDepartment - 1, SocialSciences.studentCount[(SocialSciences.Department)selectedDepartment - 1]);
                        return Enum.GetName(typeof(SocialSciences.Department), selectedDepartment - 1);
                    }
                    break;  
                default:
                    DisplayMessage("Invalid Response...");
                    break;
            }
            return "";
        }

        private static void DisplayMessage(string message)
        {
            Console.Write($"[!] {message}");
            Console.ReadLine();
        }
    }
}
