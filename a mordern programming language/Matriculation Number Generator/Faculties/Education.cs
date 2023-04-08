using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator.Faculties
{
    public class Education
    {
        public enum Department
        {
            Adult_Education,
            Arts_and_Social_Science_Education,
            Human_Kinetics_and_Health_Education,
            Education_Administration,
            Educational_Foundations,
            Science_and_Technology_Education
        }

        public static Dictionary<Department, int> studentCount = new Dictionary<Department, int>()
        {
            { Department.Adult_Education, 0 },
            { Department.Arts_and_Social_Science_Education, 0 },
            { Department.Human_Kinetics_and_Health_Education, 0 },
            { Department.Education_Administration, 0 },
            { Department.Educational_Foundations, 0 },
            { Department.Science_and_Technology_Education, 0 },
        };

        public static void updateDepartmentStudentCount(Department department)
        {
            int currentCount = 0;
            studentCount.TryGetValue(department, out currentCount);
            studentCount[department] = currentCount + 1;
        }
    }
}
